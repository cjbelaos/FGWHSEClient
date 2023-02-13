using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.eppi.utils;

namespace FGWHSEClient.Form
{
    public partial class ContainerLabelPrinting : System.Web.UI.Page
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
                    strPageSubsystem = "FGWHSE_010";
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
                    txtContainerNo.Focus();


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



        //protected void GetContainerDetails()
        //{
        //    try
        //    {
        //        DataView dv = new DataView();

        //        dv = maint.GET_CONTAINER_LABEL_DETAILS(txtContainerNo.Text.ToUpper().Trim());
        //        if (dv.Count > 0)
        //        {
        //            lblODNo.Text = dv[0]["OD_NO"].ToString();
        //            lblContainerNo.Text = dv[0]["CONTAINER_NO"].ToString();


        //            //lblBarcodeContainer.Text = "*Z1" + dv[0]["CONTAINER_NO"].ToString().ToUpper().Trim() + "*";

        //            // Get the Requested code to be created.
        //            string Code = "*Z1" + dv[0]["CONTAINER_NO"].ToString().ToUpper().Trim() + "*";

        //            // Multiply the lenght of the code by 40 (just to have enough width)
        //            int w = Code.Length * 40;

        //            // Create a bitmap object of the width that we calculated and height of 100
        //            Bitmap oBitmap = new Bitmap(w, 100);

        //            // then create a Graphic object for the bitmap we just created.
        //            Graphics oGraphics = Graphics.FromImage(oBitmap);

        //            // Now create a Font object for the Barcode Font
        //            // (in this case the IDAutomationHC39M) of 18 point size
        //            Font oFont = new Font("3 of 9 Barcode", 100);

        //            // Let's create the Point and Brushes for the barcode
        //            PointF oPoint = new PointF(2f, 2f);
        //            SolidBrush oBrushWrite = new SolidBrush(Color.Black);
        //            SolidBrush oBrush = new SolidBrush(Color.White);

        //            // Now lets create the actual barcode image
        //            // with a rectangle filled with white color
        //            oGraphics.FillRectangle(oBrush, 0, 0, w, 100);

        //            // We have to put prefix and sufix of an asterisk (*),
        //            // in order to be a valid barcode
        //            oGraphics.DrawString("*" + Code + "*", oFont, oBrushWrite, oPoint);

        //            // Then we send the Graphics with the actual barcode
        //            Response.ContentType = "image/jpeg";
        //            oBitmap.Save(Response.OutputStream, ImageFormat.Jpeg);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox1.alert("An unexpected error has occured! " + ex.Message);
        //    }
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            DataView dv = new DataView();

            dv = maint.GET_CONTAINER_LABEL_DETAILS(txtContainerNo.Text.ToUpper().Trim());
            if (dv.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(
                this.GetType(), "OpenWindow", "window.open('ContainerLabelBarcode.aspx?ContNo=" + dv[0]["CONTAINER_NO"].ToString().ToUpper().Trim() + "&Container=" + "Z1" + dv[0]["CONTAINER_NO"].ToString().ToUpper().Trim() + " ','_newtab');", true);

            }
            else
            {
                MsgBox1.alert("No Data Found.");
            }
        }




    }
}
