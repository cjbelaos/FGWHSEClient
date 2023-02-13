using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace FGWHSEClient.Classes.Handler
{
    /// <summary>
    /// Used for status while uploading/saving on database
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SharedInfo : System.Web.Services.WebService
    {
        //[WebMethod(EnableSession = true)]
        /// <summary>
        /// to get the message
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetMessage()
        {
            return StaticInfo.Message;
        }
        //[WebMethod(EnableSession = true)]
        /// <summary>
        /// to set the message
        /// </summary>
        /// <param name="msg"></param>
        [WebMethod]
        public void SetMessage(string msg) {
            StaticInfo.Message = msg;
        }
    }
}
