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
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Drawing;
using FGWHSEClient.DAL;

namespace FGWHSEClient.Form
{
    public partial class PLCMenu : System.Web.UI.Page
    {
        public string strPageSubsystem = "";
        public string strAccessLevel = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UserName"] == null)
            //{
            //    Response.Write("<script>");
            //    Response.Write("alert('Session expired! Please log in again.');");
            //    Response.Write("window.location = '../PLCLogin.aspx';");
            //    Response.Write("</script>");
            //}
            //else
            //{
            //    strPageSubsystem = "FGWHSE_036";
            //    if (!checkAuthority(strPageSubsystem))
            //    {
            //        Response.Write("<script>");
            //        Response.Write("alert('You are not authorized to access the page.');");
            //        Response.Write("window.location = '../PLCLogin.aspx';");
            //        Response.Write("</script>");
            //    }

                //if (!IsPostBack)
                //{
                //    btnPartsStoring.Enabled = false;
                //    //btnPartsStoring.BackColor = System.Drawing.Color.Gray;
                //}

                //txtPartCode.Focus();


            }

   


    //    private bool checkAuthority(string strPageSubsystem)
    //    {
    //        bool isValid = false;
    //        try
    //        {
    //            if (Session["Subsystem"] != null)
    //            {
    //                DataView dvSubsystem = new DataView();
    //                dvSubsystem = (DataView)Session["Subsystem"];

    //                if (dvSubsystem.Count > 0)
    //                {
    //                    dvSubsystem.Sort = "Subsystem";

    //                    int iRow = dvSubsystem.Find(strPageSubsystem);

    //                    if (iRow >= 0)
    //                    {
    //                        isValid = true;
    //                    }
    //                    else
    //                    {
    //                        isValid = false;
    //                    }

    //                    string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

    //                    if (strRole != "")
    //                    {
    //                        strAccessLevel = strRole;
    //                    }

    //                }
    //            }
    //            return isValid;
            
    //}
    //        catch (Exception ex)
    //        {
    //            //MsgBox1.alert("An unexpected error has occured! " + ex.Message);

    //            isValid = false;
    //            return isValid;
    //        }
    //    }

        //protected void txtPartCode_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtPartCode.Text.Trim() == "")
        //        {
        //            txtPartCode.Text = "";
        //            txtPartCode.Focus();
        //        }
        //        else
        //        { 

        //            Session["PartsCodePLC"] = null;

        //            PartsLocationCheckDAL maint = new PartsLocationCheckDAL();
        //            DataView dvGetLocation = new DataView();

        //            //dvGetLocation = maint.GetPartName(txtPartCode.Text.Trim());
        //            dvGetLocation = maint.GetPartName_rev1(txtPartCode.Text.Trim());

        //            if (dvGetLocation.Table.Rows.Count == 0)
        //            {
        //                lblPartname.Text = "PARTCODE NOT FOUND IN WAREHOUSE MASTER";
        //                lblPartname.ForeColor = System.Drawing.Color.Red;
        //                lblPartname.Font.Size = 12;

        //                lblPartCode.Text = txtPartCode.Text;
                        
        //                lblInspection.Text = "";
        //                lblEKANBANFLAG.Text = "";
        //                lblLocationST.Text = "";
        //                lblLocation.Text = "";

        //                btnPartsStoring.Enabled = false;
        //                btnPartsStoring.BackColor = System.Drawing.Color.Silver;

        //                txtPartCode.Text = "";
        //                txtPartCode.Focus();

        //            }
        //            else
        //            {

        //                string LocationFinal = "";
        //                string LocationSTFinal = "";
        //                int drCount = 1;

        //                foreach (DataRow dr in dvGetLocation.Table.Rows)
        //                {
        //                    if (drCount == 1)
        //                    {
        //                        LocationFinal = dr["Location"].ToString().ToUpper().Trim();
        //                        LocationSTFinal = dr["Location_ST"].ToString().ToUpper().Trim();
        //                    }
        //                    else
        //                    {
        //                        LocationFinal = LocationFinal + "/" + dr["Location"].ToString().ToUpper().Trim();
        //                        LocationSTFinal = LocationSTFinal + "/" + dr["Location_ST"].ToString().ToUpper().Trim();
        //                    }

        //                    drCount++;
        //                }


        //                if (LocationFinal.Replace("/", "").Trim() == "")
        //                {
        //                    LocationFinal = "";
        //                }

        //                if (LocationSTFinal.Replace("/","").Trim() == "")
        //                {
        //                    LocationSTFinal = "";
        //                }

        //                btnPartsStoring.BackColor = System.Drawing.Color.LightGreen;
        //                lblPartname.ForeColor = System.Drawing.Color.Black;
        //                lblPartname.Font.Size = 14;
        //                lblPartname.Text = dvGetLocation.Table.Rows[0]["PartDescription"].ToString();
        //                lblLocation.Text = LocationFinal; //dvGetLocation.Table.Rows[0]["Location"].ToString();
        //                lblLocationST.Text = LocationSTFinal; //dvGetLocation.Table.Rows[0]["Location_ST"].ToString();
        //                lblPartCode.Text = dvGetLocation.Table.Rows[0]["PartCode"].ToString();

        //                string IQAFlag = dvGetLocation.Table.Rows[0]["Inspection"].ToString();
        //                string EKanbanRFIDIFFlag = dvGetLocation.Table.Rows[0]["EKanbanRFIDIFFlag"].ToString();
        //                string Capacity = dvGetLocation.Table.Rows[0]["Capacity"].ToString();


        //                lblEKANBANFLAG.Text = EKanbanRFIDIFFlag.Trim().ToUpper();


        //                lblInspection.Text = IQAFlag.Trim();

        //                //if (IQAFlag.Trim().ToUpper() == "TRUE")
        //                //{
        //                //    lblInspection.Text = "FOR IQA";
        //                //}
        //                //else
        //                //{
        //                //    lblInspection.Text = "";
        //                //}


        //                //TextBox1.Text = dvGetLocation.Table.Rows[0]["PartDescription"].ToString();


        //                //if (lblLocation.Text == "2F Common")
        //                //{
        //                //    divlocation.Style["background-color"] = "Green";
        //                //    lblLocation.ForeColor = System.Drawing.Color.White;
        //                //}
        //                //else if (lblLocation.Text == "2F Unique")
        //                //{
        //                //    divlocation.Style["background-color"] = "Yellow";
        //                //    lblLocation.ForeColor = System.Drawing.Color.Black;
        //                //}
        //                //else
        //                //{
        //                //    divlocation.Style["background-color"] = "Silver";
        //                //    lblLocation.ForeColor = System.Drawing.Color.Black;
        //                //}

        //                txtPartCode.Text = "";
        //                txtPartCode.Focus();

        //                btnPartsStoring.Enabled = true;
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        msgBox.alert(ex.Message);
        //    }
        //}


        protected void btnPS1_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("PartsLocationCheckv2.aspx");
            
        }

        protected void btnPS2_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("PartsLocationCheckPS2.aspx");
        }
    }
}
