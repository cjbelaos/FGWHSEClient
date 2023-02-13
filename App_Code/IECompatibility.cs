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

/// <summary>
/// Summary description for IECompatibility
/// </summary>
namespace com.eppi.utils
{
    public class IECompatibility
    {
        public IECompatibility()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string GetIEBrowserMode(HttpRequest Request)
        {
            //http://www.toplinestrategies.com/dotneters/net/detecting-ie-compatibility-view-c/?lang=en

            string mode = "";
            string userAgent = Request.UserAgent;
            //entire UA string     
            string browser = Request.Browser.Type;
            //Browser name and Major Version #      
            if (userAgent.Contains("Trident/5.0")) //IE9 has this token     
            {
                if (browser == "IE7")
                {
                    mode = "Compatibility View";
                }
                else
                {
                    mode = "IE9 Standard";
                }
            }
            else if (userAgent.Contains("Trident/4.0")) //IE8 has this token     
            {
                if (browser == "IE7")
                {
                    mode = "Compatibility View";
                }
                else
                {
                    mode = "IE8 Standard";
                }
            }
            else if (!userAgent.Contains("Trident")) //Earlier versions of IE do not contain the Trident token     
            {
                mode = browser;
            }

            return mode;
        }
    }
}
