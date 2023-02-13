using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace FGWHSEClient.Classes
{
    /// <summary>
    /// Summary description for SharedService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SharedService : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetPlants()
        {
            return new CtrlDOS().GetPlants();
        }
        [WebMethod(EnableSession = true)]
        public string GetVendors()
        {
            string UserID = Session["UserID"].ToString();
            string SupplierCode = new OtherFunctions().GetVendorBasedOnUser(UserID);
            return new CtrlDOS().GetVendors(SupplierCode);
        }
        [WebMethod]
        public string GetMaterial()
        {
            return new CtrlDOS().GetMaterial();
        }
        [WebMethod]
        public string GetBusCategory()
        {
            return new CtrlDOS().GetBusCategory();
        }
        [WebMethod]
        public string GetModel()
        {
            return new CtrlDOS().GetModel();
        }
        [WebMethod]
        public string GetProblemCategory()
        {
            return new CtrlDOS().GetProblemCategory();
        }
        [WebMethod]
        public string GetNoDOSMaterial(string SupplierCode) {
            return new CtrlDOS().GetNoDOSMaterial(SupplierCode);
        }
        [WebMethod]
        public string GetNoDOSMaterialCount(string SupplierCode) {
            return new CtrlDOS().GetNoDOSMaterialCount(SupplierCode);
        }
        [WebMethod]
        public string StopTheProcess() {
            StaticInfo.CanProcess = false;
            return "Trying to stop the process";
        }
        [WebMethod]
        public string StartTheProcess() {
            StaticInfo.CanProcess = true;
            return "Trying to start the process";
        }
        [WebMethod(EnableSession = true)]
        public string GetSession() {
            var d = ((DataView)Session["Subsystem"]);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("UserID", Session["UserID"]);
            dic.Add("UserName", Session["UserName"]);
            dic.Add("RoleID", Session["RoleID"]);
            dic.Add("Subsystem", d.Table);
            return JsonConvert.SerializeObject(dic);
        }
    }
}
