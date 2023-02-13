using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace FGWHSEClient.Classes
{
    /// <summary>
    /// Summary description for SaveShortageList
    /// </summary>
    public class SaveShortageList : IHttpHandler
    {
        CtrlShortageList ctrl;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Form.Count > 0)
            {
                ctrl = new CtrlShortageList();
                string FileName = context.Request.Form["FileName"];
                DataTable ExcelData = ExcelReader.GetData(HttpRuntime.AppDomainAppPath + "/Uploaded/" + FileName);
                //return MyControl.SaveData(dt);
                DataTable Plants = ctrl.MyPlant();
                Dictionary<string, Dictionary<string, string>> dicHeads = new Dictionary<string, Dictionary<string, string>>();
                Dictionary<string, Dictionary<DateTime, string>> dicRows = new Dictionary<string, Dictionary<DateTime, string>>();
                List<string> lstColumns = ctrl.GetDateColumnsFromExcel(ExcelData);
                //context.Response.Flush();
                try
                {
                    Parallel.ForEach(ExcelData.Rows.Cast<DataRow>(), (dr) => {
                        context.Response.Write("");
                        if (dr["Rece#/issue"].ToString() == "issue")
                        {
                            Dictionary<string, string> dicHead = new Dictionary<string, string>();
                            Dictionary<DateTime, string> dicRow = new Dictionary<DateTime, string>();
                            string Plant = dr["Plant"].ToString();
                            string PlantID = ctrl.GetPlantID(Plant, Plants);
                            string MaterialNumber = dr["Material Number"].ToString();
                            dicHead.Add("PlantID", PlantID);
                            dicHead.Add("MaterialNumber", MaterialNumber);
                            dicHead.Add("MaterialDescription", dr["Material Description"].ToString());
                            dicHead.Add("MainVendor", dr["Main Vendor"].ToString());
                            dicHead.Add("OwnStock", dr["Own stock"].ToString());
                            dicHead.Add("Issued", dr["Rece#/issue"].ToString());
                            dicHead.Add("PastDue", dr["Past"].ToString());
                            dicHead.Add("Stock", dr["Stock"].ToString());
                            string DataName = string.Format("P{0}M{1}", PlantID, MaterialNumber);
                            Parallel.ForEach(lstColumns, col => {
                                DateTime Date = DateTime.Parse(col);
                                string Amount = dr[col].ToString();
                                dicRow.Add(Date, Amount);
                            });
                            if (dicHeads.Keys.Contains(DataName))
                            {
                                dicHeads[DataName]["PastDue"] = (int.Parse(dicHeads[DataName]["PastDue"]) + int.Parse(dicHead["PastDue"].ToString())).ToString();
                                dicHeads[DataName]["Stock"] = (int.Parse(dicHeads[DataName]["Stock"]) + int.Parse(dicHead["Stock"].ToString())).ToString();
                                Dictionary<DateTime, string> tmp = new Dictionary<DateTime, string>();
                                Parallel.ForEach(dicRows[DataName], item => {
                                    tmp.Add(item.Key, (int.Parse(dicRows[DataName][item.Key].ToString()) + int.Parse(dicRow[item.Key].ToString())).ToString());
                                });
                                dicRows[DataName] = tmp;
                            }
                            else
                            {
                                dicHeads.Add(DataName, dicHead);
                                dicRows.Add(DataName, dicRow);
                            }
                        }
                    });
                }
                catch (Exception exception)
                {
                    context.Response.Write(exception.Message);
                }

                
                //return SaveData(dicHeads, dicRows);
                Connection MyConnection = new Connection();
                SqlConnection conn = MyConnection.SetConnectionSettings();
                using (conn)
                {
                    conn.Open();


                    SqlTransaction transaction;

                    // Start a local transaction.
                    transaction = conn.BeginTransaction("InsertData");

                    // Must assign both transaction object and connection
                    // to Command object for a pending local transaction

                    try
                    {
                        int ctr = 0;
                        Parallel.ForEach(dicHeads,item=> {
                            ctr++;
                            if (dicRows.Keys.Contains(item.Key))
                            {
                                int ctr1 = 0;
                                Parallel.ForEach(dicRows[item.Key],det=> {
                                    ctr1++;
                                    //msg = string.Format("Saving {0} : [h:{1}/{2}][d:{3}/{4}]", item.Value["MaterialNumber"], ctr, Header[item.Key].Count, ctr1, Detail[item.Key].Count);
                                    //fw.Write(msg);
                                    SqlCommand command = conn.CreateCommand();
                                    command.Connection = conn;
                                    command.Transaction = transaction;
                                    string date = det.Key.ToString("yyyy-MM-dd");
                                    command.CommandText = "PSIShortageListSave";
                                    command.Parameters.AddWithValue("@PlantID", item.Value["PlantID"]);
                                    command.Parameters.AddWithValue("@MaterialNumber", item.Value["MaterialNumber"]);
                                    command.Parameters.AddWithValue("@MaterialDescription", item.Value["MaterialDescription"]);
                                    command.Parameters.AddWithValue("@MainVendor", item.Value["MainVendor"]);
                                    command.Parameters.AddWithValue("@OwnStock", item.Value["OwnStock"]);
                                    command.Parameters.AddWithValue("@Issued", item.Value["Issued"]);
                                    command.Parameters.AddWithValue("@PastDue", item.Value["PastDue"]);
                                    command.Parameters.AddWithValue("@Date", date);
                                    command.Parameters.AddWithValue("@Amount", det.Value);
                                    command.Parameters.AddWithValue("@Stock", item.Value["Stock"]);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.ExecuteNonQuery();
                                });
                            }
                        });

                        // Attempt to commit the transaction.
                        transaction.Commit();
                        //fw.Write("Committed");
                        //return ("Both records are written to database.");
                        context.Response.Write("Both records are written to database.");
                    }
                    catch (Exception ex)
                    {
                        //return string.Format("Commit Exception Type: {0}", ex.GetType());
                        //Console.WriteLine("  Message: {0}", ex.Message);

                        // Attempt to roll back the transaction.
                        context.Response.Write(ex.Message);
                        try
                        {
                            conn.Close();
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            context.Response.Write(ex2.Message);
                            // This catch block will handle any errors that may have occurred
                            // on the server that would cause the rollback to fail, such as
                            // a closed connection.
                            //return string.Format("Rollback Exception Type: {0}", ex2.GetType());
                            //Console.WriteLine("  Message: {0}", ex2.Message);
                        }
                    }
                }
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}