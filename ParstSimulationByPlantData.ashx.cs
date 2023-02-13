using FGWHSEClient.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FGWHSEClient
{
    /// <summary>
    /// Summary description for ParstSimulationByPlantData
    /// </summary>
    public class ParstSimulationByPlantData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // Those parameters are sent by the plugin
            var plant = Convert.ToString(context.Request.QueryString["plant"]);
            var parts = Convert.ToString(context.Request.QueryString["parts"]);
            var vendors = Convert.ToString(context.Request.QueryString["vendors"]);

            // prepare an anonymous object for JSON serialization
            var result = GetPartsSimulationByPlant(plant, parts, vendors);

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(result);
            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }

        public DataTable GetPartsSimulationByPlant(string strPLANT, string strPARTS, string strVENDORS)
        {
            DALPSI_PLANT DPSI = new DALPSI_PLANT();
            DataTable dt = DPSI.PSI_GET_PARTSSIMULATION_BY_PLANT(strPLANT, strPARTS, strVENDORS);
            var StartDate = dt.Rows[0][209].ToString();
            for (var i = 11; i <= 208;)
            {
                for (int j = 0; j <= 197; j++)
                {
                    if (i == 11)
                    {
                        dt.Columns[i].ColumnName = StartDate;
                    }
                    else
                    {
                        DateTime Date = Convert.ToDateTime(StartDate);
                        dt.Columns[i].ColumnName = Date.AddDays(j).ToString("MM/dd/yyyy");
                    }
                    i++;
                }
            }
            dt.Columns.Remove("FIRSTCOLUMNNAME");

            return dt;
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