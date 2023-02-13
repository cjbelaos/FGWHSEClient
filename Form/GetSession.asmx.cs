using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace FGWHSEClient
{
    /// <summary>
    /// Summary description for GetSession
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GetSession : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public string GetSessions()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string userid = "";
            string username = "";
            string excelfile = "";
            string errormessage = "";

            if (Session["UserID"] == null)
            {
                userid = "";
            }
            else
            {
                userid = Session["UserID"].ToString();
            }

            if (Session["UserName"] == null)
            {
                username = "";
            }
            else
            {
                username = Session["UserName"].ToString();
            }

            if (Session["ExcelFile"] == null)
            {
                excelfile = "";
            }
            else
            {
                excelfile = Session["ExcelFile"].ToString();
            }

            if (Session["ErrorMessage"] == null)
            {
                errormessage = "";
            }
            else
            {
                errormessage = Session["ErrorMessage"].ToString();
            }

            dic.Add("UserID", userid);
            dic.Add("UserName", username);
            dic.Add("ExcelFile", excelfile);
            dic.Add("ErrorMessage", errormessage);

            return JsonConvert.SerializeObject(dic);
        }
    }
}
