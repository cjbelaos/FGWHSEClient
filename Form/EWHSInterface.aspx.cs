using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using FGWHSEClient.DAL;
using System.IO;
using System.Drawing;

namespace FGWHSEClient.Form
{
    public partial class EWHSInterface : System.Web.UI.Page
    {
        PartsLocationCheckDAL PartsLocationCheckDAL = new PartsLocationCheckDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string str = PartsLocationCheckDAL.GET_EKANBANVP_PARTSMASTER();

                if (str.Length > 0)
                {
                    MsgBox1.alert(str);
                }
                else
                {
                    MsgBox1.alert("Interface Successfully Finished!");
                }
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
        }

        protected void btnRefreshVirtual_Click(object sender, EventArgs e)
        {
            try
            {
                string str = PartsLocationCheckDAL.GET_VIRTUALPARTSMASTER_VPPIS();

                if (str.Length > 0)
                {
                    MsgBox1.alert(str);
                }
                else
                {
                    //MsgBox1.alert("Interface Successfully Finished!");

                    string str2 = PartsLocationCheckDAL.GET_VIRTUALPARTSMASTER_VPEPI();

                    if (str2.Length > 0)
                    {
                        MsgBox1.alert(str2);
                    }
                    else
                    {
                        MsgBox1.alert("Interface Successfully Finished!");
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }


        }
    }
}
