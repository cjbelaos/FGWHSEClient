using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.eppi.utils;
using System.Data.OleDb;
using System.IO;
using System.Text;


namespace FGWHSEClient.Form
{
    public partial class MasterRFIDTag : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        Maintenance maint = new Maintenance();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../Login.aspx';");
                    Response.Write("</script>");
                }
                else
                {
                    strPageSubsystem = "FGWHSE_007";
                    if (!checkAuthority(strPageSubsystem))
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'Default.aspx';");
                        Response.Write("</script>");
                    }
                }


                if (!this.IsPostBack)
                {
                    getSupplier();
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }

        }

        private bool checkAuthority(string strPageSubsystem)
        {
            bool isValid = false;
            try
            {
                if (Session["Subsystem"] != null)
                {
                    DataView dvSubsystem = new DataView();
                    dvSubsystem = (DataView)Session["Subsystem"];

                    if (dvSubsystem.Count > 0)
                    {
                        dvSubsystem.Sort = "Subsystem";

                        int iRow = dvSubsystem.Find(strPageSubsystem);

                        if (iRow >= 0)
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }

                        string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                        if (strRole != "")
                        {
                            strAccessLevel = strRole;
                        }

                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);

                isValid = false;
                return isValid;
            }
        }

        private void getSupplier()
        {
            DataView dvRFID = new DataView();

            dvRFID = maint.GETSUPPLIER("");

            DataTable dtnewRFID = new DataTable();


            dtnewRFID.Columns.Add("SUPPLIERLOCATION", typeof(string));
            dtnewRFID.Columns.Add("SUPPLIERID", typeof(string));

            dtnewRFID.Rows.Add("", "");


            foreach (DataRow row in dvRFID.Table.Rows)
            {

                String WHID = (string)row[0];
                String WHNAME = (string)row[1];
                dtnewRFID.Rows.Add(WHID, WHNAME);
            }

            ddlSupplier.DataSource = dtnewRFID;
            ddlSupplier.DataTextField = "SUPPLIERLOCATION";
            ddlSupplier.DataValueField = "SUPPLIERID";
            ddlSupplier.DataBind();


            DataView dvRFIDCount = new DataView();
            dvRFIDCount = maint.GETRFIDCOUNT();
            lbltotalrfidtag.Text = Convert.ToString(dvRFIDCount[0]["RFIDTAG_COUNT"]);

        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FileUpload1.Dispose();


            DataView dv = new DataView();
            dv = maint.GET_RFIDMASTER(txtRFIDTAG.Text.Trim(), ddlSupplier.SelectedItem.ToString());

            if (dv.Table.Rows.Count > 0)
            {

                grdRFIDSearch.DataSource = dv;
                grdRFIDSearch.DataBind();

                divSearch.Visible = true;
                divUpload.Visible = false;
            }
            else
            {
                MsgBox1.alert("NO DATA FOUND");
            }
        }

        protected void lblCheck_Click(object sender, EventArgs e)
        {

            txtRFIDTAG.Text = "";
            ddlSupplier.SelectedIndex = -1;
            divSearch.Visible=false;
            
            int error = 0;

            OleDbConnection conn = new OleDbConnection();


            try
            {

                if ((FileUpload1.HasFile))
                {

                    OleDbCommand cmd = new OleDbCommand();
                    OleDbDataAdapter da = new OleDbDataAdapter();
                    DataSet ds = new DataSet();

                    string query = null;
                    string connString = "";
                    string strFileName = "RFID TAG TEMPLATE"; //"RFID TAG TEMPLATE" + DateTime.Now.ToString("mmDDyyyyHHmmsstt");

                    string strFileType = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();

                    //Check file type
                    if (strFileType == ".xls" || strFileType == ".xlsx")
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                    }
                    else
                    {
                        MsgBox1.alert("Only excel files allowed");
                        return;
                    }

                    string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);

                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        //connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {
                        // connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                    }

                    query = "SELECT * FROM [RFIDMASTERTEMPLATE$]";


                    //Create the connection object
                    conn = new OleDbConnection(connString);
                    //Open connection
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    //Create the command object
                    cmd = new OleDbCommand(query, conn);
                    da = new OleDbDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();

                    DataTable dt = new DataTable();
                    DataView dvSection = new DataView();


                    DataTable dtnew1 = new DataTable();
                    dtnew1.Columns.Add("RFIDTAG", typeof(string));
                    dtnew1.Columns.Add("EPCDATA", typeof(string));
                    dtnew1.Columns.Add("SUPPLIER", typeof(string));
                    dtnew1.Columns.Add("REMARKS", typeof(string));
 
                    dt = ds.Tables[0];


                    // uploading
                    foreach (DataRow row in dt.Rows)
                    {

                        string RFIDTAG = row[0].ToString();
                        string EPCDATA = "";
                        string SUPPLIER = row[1].ToString();
                        string REMARKS = "";
                        string EPCDATA2 = string.Empty, FINREMARKS = String.Empty;

                        if (RFIDTAG.Trim() != "")
                        {
                            if ((RFIDTAG.Trim().Length == 24) || (RFIDTAG.Trim().Length == 28))
                            {
                                ///////////////check if RFID TAG exist
                                DataView dvRFID = new DataView();
                                dvRFID = maint.GET_RFIDMASTER(RFIDTAG.Trim(), "");

                                if (dvRFID.Table.Rows.Count > 0)
                                {
                                    REMARKS = "ALREADY EXISTS";
                                    error = +1;
                                }

                                else
                                {
                                    //////////////checking if supplier exist
                                    DataView dvSupplier = new DataView();
                                    dvSupplier = maint.GETSUPPLIER(SUPPLIER.Trim());

                                    if (!(dvSupplier.Table.Rows.Count > 0))
                                    {
                                        REMARKS = "SUPPLIER NOT FOUND";
                                        error = +1;
                                    }
                                }

                                ////////////////conversion of RFID Tag
                                if (RFIDTAG.Substring(0, 4) == "8003")
                                {
                                    Conversion(RFIDTAG, out EPCDATA2, out FINREMARKS);
                                }
                                else
                                {
                                    EPCDATA2 = RFIDTAG.Trim();
                                }

                              

                            }
                            else
                            {
                                REMARKS = "WRONG FORMAT - LENGTH";
                                error = +1;
                            }

                            if (FINREMARKS.Trim() == "")
                            {
                                FINREMARKS = REMARKS;
                            }


                            dtnew1.Rows.Add(RFIDTAG, EPCDATA2, SUPPLIER, FINREMARKS);
                        }
                    }

                    grdRFIDUpload.DataSource = dtnew1;
                    grdRFIDUpload.DataBind();

                    divUpload.Visible = true;
                    divSearch.Visible = false;

                    if (error != 0)
                    {
                        btnSave.Visible = false;
                    }
                    else
                    {
                        btnSave.Visible = true;
                    }
                   
                }
                else
                {
                    MsgBox1.alert("Please select an excel file first");
                }
            }
            catch (Exception ex)
            {
                MsgBox1.alert("Error while retrieving data from excel: " + ex.Message);
                Logger.GetInstance().Fatal("RFID MASTER CHECKING UPLOAD: " + ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            
        }


        public static void Conversion(string RFIDTAG, out string EPCDATA, out string FINREMARKS)
        {
           
                string strRFIDTag = RFIDTAG.Trim().ToUpper();

                String strLocationCode = "35";
                String strAssetType = "001";
                int nCompanyCode = 4942715;
                string strCompanyCode = "04942715";
                Int64 nCurrentSerialNo = 1000000001;

            //nCurrentSerialNo = Convert.ToInt64("1" + strRFIDTag.Substring(19, 9));
            nCurrentSerialNo = Convert.ToInt64(strRFIDTag.Substring(18, 10));


            int nLACode = 0;

                string remarks = "";


                //checking
                //company code
                if (strRFIDTag.Substring(4, 8) != strCompanyCode)
                {
                    remarks = "WRONG COMPANY CODE";

                }

                //Location Code
                if (strRFIDTag.Substring(12, 2) != strLocationCode)
                {
                    remarks = "WRONG LOCATION CODE";

                }

                // Location Code
                if (strRFIDTag.Substring(14, 3) != strAssetType)
                {
                    remarks = "WRONG ASSET TYPE";

                }


                if (!Int32.TryParse(strLocationCode + strAssetType, out nLACode))
                {
                    //Error
                    // return remarks;
                }

                //Generate RFID EPC data
                byte[] byRFIDData = new byte[12];
                byRFIDData[0] = 0x33;
                byRFIDData[1] = (byte)((5 << 2) + (nCompanyCode >> 22));
                byRFIDData[2] = (byte)((nCompanyCode & 0x3FFFFF) >> 14);
                byRFIDData[3] = (byte)((nCompanyCode & 0x3FFF) >> 6);
                byRFIDData[4] = (byte)(((nCompanyCode & 0x3F) << 2) + (nLACode >> 18));
                byRFIDData[5] = (byte)((nLACode & 0x3FFFF) >> 10);
                byRFIDData[6] = (byte)((nLACode & 0x3FF) >> 2);
                byRFIDData[7] = (byte)(((nLACode & 0x3) << 6) + (nCurrentSerialNo >> 32));
                byRFIDData[8] = (byte)((nCurrentSerialNo & 0xFFFFFFFF) >> 24);
                byRFIDData[9] = (byte)((nCurrentSerialNo & 0xFFFFFF) >> 16);
                byRFIDData[10] = (byte)((nCurrentSerialNo & 0xFFFF) >> 8);
                byRFIDData[11] = (byte)(nCurrentSerialNo & 0xFF);


                //string EPCDATA = "";
                EPCDATA = ByteArrayToHexString(byRFIDData);
                FINREMARKS = remarks;
                //return EPCDATA;
        }


        public static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }

        public static byte[] HexStringToByteArray(string Hex)
        {
            byte[] Bytes = new byte[Hex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 | HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }
            return Bytes;
        }

        protected void grdRFIDUpload_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
             
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblRemarks = (Label)e.Row.Cells[3].Controls[1];

                    if (lblRemarks.Text.Trim()== "")
                    {
                        e.Row.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        e.Row.ForeColor = System.Drawing.Color.Red;
                    }



                }

            }
            catch (Exception ex)
            {

                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
                Logger.GetInstance().Fatal("RFID MASTER ROWDATABOUND: " + ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterRFIDTag.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int totalsave = 0;
            try
            {
                if (grdRFIDUpload.Rows.Count > 0)
                {
                    foreach (GridViewRow gvRow in grdRFIDUpload.Rows)
                    {
                        string RFIDTAG = ((Label)gvRow.FindControl("lblRFIDTAG")).Text.Trim().ToUpper();
                        string EPCDATA = ((Label)gvRow.FindControl("lblEPCDATA")).Text.Trim().ToUpper();
                        string SUPPLIER = ((Label)gvRow.FindControl("lblSupplier")).Text.Trim().ToUpper();

                       int save= maint.ADD_RFIDMASTER(RFIDTAG.Trim(), EPCDATA.Trim(), SUPPLIER.Trim(), Session["UserID"].ToString());

                       if (save == 1)
                       {
                           totalsave += 1;
                       }
                       else
                       {
                           Logger.GetInstance().Fatal("Insert Failed: RFID MASTER SAVING: " + RFIDTAG +":" +EPCDATA +":" +SUPPLIER+ ":"+ Session["UserID"].ToString());
                       }

                     }

                }


                Response.Write("<script>");
                Response.Write("alert('UPLOADING SUCCESSFUL!\\n TOTAL COUNT SAVE: [" + totalsave.ToString() + "]');");
                Response.Write("window.location = 'MasterRFIDTag.aspx';");
                Response.Write("</script>");


            }
            catch (Exception ex)
            {
                MsgBox1.alert("An unexpected error has occured! " + ex.Message);
                Logger.GetInstance().Fatal("RFID MASTER SAVING: " + ex.Message);
            }
        }


    }
}
