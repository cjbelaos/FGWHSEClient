using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace FGWHSEClient.Form
{
    public partial class ReportsWeeklyShipmentScheduleHistory : System.Web.UI.Page
    {
        public string strAccessLevel = "";
        Maintenance maint = new Maintenance();
        DataView dv = new DataView();
        DataTable dtWSS = new DataTable();
        string strUserID;
        public int rowIndx = -1;




        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UserID"] == null)
                {
                    Response.Write("<script>");
                    Response.Write("alert('Session expired! Please log in again.');");
                    Response.Write("window.location = '../Login.aspx';");
                    Response.Write("</script>");
                }
                else
                {
                    strUserID = Session["UserID"].ToString();
                    getLegend(trLegend);
                    getLegend(trLegendExport);

                    MaintainScrollPositionOnPostBack = true;


                    if (!this.IsPostBack)
                    {


                        lblUserID.Text = Session["UserID"].ToString();
                        GetWeeklyShipmentSchedule();


                    }








                    ////ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdWeeklyShipmentSchedule.ClientID + "', 400, 1150 , 20 ,true); </script>", false);
                }
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }
        }

        public void getLegend(HtmlTableRow trLegendRow)
        {
            DataTable dt = new DataTable();
            dt = maint.GET_WEEKLY_SHIPMENT_MAINTAINED_STATUS().Tables[0];
            dv = dt.DefaultView;


            for (int x = 0; x < dv.Count; x++)
            {


                HtmlTableCell tdStatusColor = new HtmlTableCell();
                tdStatusColor.Attributes.Add("style", "border-style:solid; border-width:thin; border-top-color:black;border-left-color:black;border-right-color:lightgray;border-bottom-color:lightgray;width:13px; height:13px;background-color:" + dv[x]["COLORDISPLAY"].ToString());
                trLegendRow.Cells.Add(tdStatusColor);


                Label lblStatusBlank = new Label();
                tdStatusColor.Controls.Add(lblStatusBlank);


                HtmlTableCell tdStatus = new HtmlTableCell();
                trLegendRow.Cells.Add(tdStatus);


                Label lblStatus = new Label();
                lblStatus.Text = dv[x]["STATUSDESCRIPTION"].ToString();
                tdStatus.Controls.Add(lblStatus);

                //CheckBox chck = new CheckBox();
                //chck.Text = dv[x]["STATUSDESCRIPTION"].ToString();
                //tdStatusFilter.Controls.Add(chck);
            }


        }





        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }


        #region Excel to Dataset






        private void finalizeDT(DataTable dt, DataTable finalDT)
        {

            string strColumnName = "";
            dv = dt.DefaultView;

            DataRow dr;
            finalDT.Clear();

            if (dt.Rows.Count > 2)
            {
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    //strColumnName = (dv[0][x].ToString() + " " + dv[1][x].ToString()).Trim();
                    //finalDT.Columns.Add(strColumnName, typeof(String));

                    finalDT.Columns.Add("F" + (x + 1).ToString(), typeof(String));
                }



                for (int y = 2; y < dt.Rows.Count; y++)
                {
                    dr = finalDT.NewRow();
                    for (int z = 0; z < dt.Columns.Count; z++)
                    {
                        strColumnName = ("F" + (z + 1).ToString()).Trim();

                        dr[strColumnName] = dv[y][z].ToString();
                    }

                    finalDT.Rows.Add(dr);
                    finalDT.AcceptChanges();

                }
            }




        }








        #endregion




        public void GetWeeklyShipmentSchedule()
        {


            if (txtInvoiceNo.Text.Trim() == "" && txtODNo.Text.Trim() == "" && txtShipmentNo.Text.Trim() == "")
            {
                MsgBox1.alert("Please select atleast 1 category!");
                return;
            }
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdWeeklyShipmentSchedule.ClientID + "', 400, 1150 , 20 ,true); </script>", false);
            grdWeeklyShipmentSchedule.DataSource = maint.GET_WEEKLY_SHIPMENT_SCHEDULE_HISTORY(txtInvoiceNo.Text.Trim(), txtODNo.Text.Trim(), txtShipmentNo.Text.Trim()).Tables[0];
            grdWeeklyShipmentSchedule.DataBind();


            //style="overflow:scroll; width:1150px; height:400px; font-size:small; color:black" 
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdWeeklyShipmentSchedule.ClientID + "', 400, 1150 , 20 ,true); </script>", false);

            grdExport.DataSource = grdWeeklyShipmentSchedule.DataSource;
            grdExport.DataBind();

        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetWeeklyShipmentSchedule();
        }

        protected void grdWeeklyShipmentSchedule_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridViewRow row = grdWeeklyShipmentSchedule.Rows[e.NewEditIndex];

            //int rowIndex = grdWeeklyShipmentSchedule.EditIndex;
            //GridViewRow row = grdWeeklyShipmentSchedule.Rows[rowIndex];

            //updateWSS(row);


        }






        public void DisplayLabel(Label lbl, Boolean boolen)
        {
            lbl.Visible = boolen;
        }

        public void DisplayTextBox(TextBox txt, Boolean boolen)
        {
            txt.Visible = boolen;
        }

        protected void grdWeeklyShipmentSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblColorDisplay;
            CheckBox chkSelect;
            ImageButton imgEdit;
            Label lblWSSSTATUSFLAG;
            CheckBox chkCheckAll;

            Label lblUPDATEDCOLUMNS;
            Label lblTEXTCOLORDISPLAY;
            string strRowType = e.Row.RowType.ToString();

            if (strRowType == "Header")
            {

                chkCheckAll = (CheckBox)e.Row.FindControl("chkHeader");


            }

            if (strRowType != "Header" && strRowType != "Footer")
            {
                lblUPDATEDCOLUMNS = (Label)e.Row.FindControl("lblUPDATEDCOLUMNS");
                lblTEXTCOLORDISPLAY = (Label)e.Row.FindControl("lblTEXTCOLORDISPLAY");

                lblColorDisplay = (Label)e.Row.FindControl("lblColorDisplay");
                chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                lblWSSSTATUSFLAG = (Label)e.Row.FindControl("lblWSSSTATUSFLAG");

                e.Row.BackColor = System.Drawing.Color.FromName(lblColorDisplay.Text);



                //if (lblWSSSTATUSFLAG.Text.Trim() == "1" || lblWSSSTATUSFLAG.Text.Trim() == "3")
                if (lblWSSSTATUSFLAG.Text.Trim() == "3")
                {
                    chkSelect.Enabled = false;
                    imgEdit.Enabled = false;
                }


                string strColName = lblUPDATEDCOLUMNS.Text, strCurrentColumn = "";
                int intStrLength = 0, colEditedCount = 0;
                intStrLength = strColName.Length;
                Label lblColumn;

                for (int x = 0; x < intStrLength; x++)
                {
                    colEditedCount = strColName.Split('/').Length - 1;

                    if (strColName.Substring(x, 1) != "/")
                    {
                        strCurrentColumn = strCurrentColumn + strColName.Substring(x, 1);
                    }
                    else
                    {
                        lblColumn = (Label)e.Row.FindControl("lbl" + strCurrentColumn.Trim());
                        lblColumn.ForeColor = System.Drawing.Color.FromName(lblTEXTCOLORDISPLAY.Text);
                        lblColumn.Font.Bold = true;
                        strCurrentColumn = "";
                    }

                }


            }

        }



        public string getIDtoPostOrDelete()
        {
            Label lblID;
            CheckBox chkSelect;
            string SelectedId = "";
            for (int x = 0; x < grdWeeklyShipmentSchedule.Rows.Count; x++)
            {
                lblID = ((Label)grdWeeklyShipmentSchedule.Rows[x].FindControl("lblID"));
                chkSelect = ((CheckBox)grdWeeklyShipmentSchedule.Rows[x].FindControl("chkSelect"));

                if (chkSelect.Checked == true)
                {
                    SelectedId = SelectedId + lblID.Text + "/";
                }

                if (x + 1 == grdWeeklyShipmentSchedule.Rows.Count)
                {
                    if (SelectedId.Trim() != "")
                    {
                        SelectedId = (SelectedId.Substring(0, SelectedId.Length - 1)).Trim();
                    }
                }
            }
            return SelectedId;
        }









        public int getSelectedCount()
        {

            CheckBox chkSelect;
            int SelectedIdCount = 0;
            for (int x = 0; x < grdWeeklyShipmentSchedule.Rows.Count; x++)
            {
                chkSelect = ((CheckBox)grdWeeklyShipmentSchedule.Rows[x].FindControl("chkSelect"));

                if (chkSelect.Checked == true)
                {
                    SelectedIdCount = SelectedIdCount + 1;
                }
            }
            return SelectedIdCount;
        }


        public void updateWSSdb(int rowIDX)
        {
            try
            {

                GridViewRow row = grdWeeklyShipmentSchedule.Rows[rowIDX];

                Label lblID = ((Label)row.FindControl("lblID"));
                TextBox txtGNSINVOICENUMBER = ((TextBox)row.FindControl("txtGNSINVOICENUMBER"));
                TextBox txtPRODUCTIONLOCATION = ((TextBox)row.FindControl("txtPRODUCTIONLOCATION"));
                TextBox txtSHIPPINGLOCATION = ((TextBox)row.FindControl("txtSHIPPINGLOCATION"));
                TextBox txtODNO = ((TextBox)row.FindControl("txtODNO"));
                TextBox txtLOTNO = ((TextBox)row.FindControl("txtLOTNO"));
                TextBox txtSHIPMENTNO = ((TextBox)row.FindControl("txtSHIPMENTNO"));
                TextBox txtPOMONTH = ((TextBox)row.FindControl("txtPOMONTH"));
                TextBox txtPONUMBER = ((TextBox)row.FindControl("txtPONUMBER"));
                TextBox txtCONSIGNEEPONUMBER = ((TextBox)row.FindControl("txtCONSIGNEEPONUMBER"));
                TextBox txtPOWK = ((TextBox)row.FindControl("txtPOWK"));
                TextBox txtITEMCODE = ((TextBox)row.FindControl("txtITEMCODE"));
                TextBox txtMODELDESCRIPTION = ((TextBox)row.FindControl("txtMODELDESCRIPTION"));
                TextBox txtMODELNAME = ((TextBox)row.FindControl("txtMODELNAME"));
                TextBox txtPLANNEDEXFACTDATE = ((TextBox)row.FindControl("txtPLANNEDEXFACTDATE"));
                TextBox txtPLANNEDOBDATE = ((TextBox)row.FindControl("txtPLANNEDOBDATE"));
                //TextBox txtREVISEDEXFACEDATE = ((TextBox)row.FindControl("txtREVISEDEXFACEDATE"));
                //TextBox txtREVISEDOBDATE = ((TextBox)row.FindControl("txtREVISEDOBDATE"));
                TextBox txtQTY = ((TextBox)row.FindControl("txtQTY"));
                TextBox txtCUSTOMER = ((TextBox)row.FindControl("txtCUSTOMER"));
                TextBox txtSHIMPENTMOD = ((TextBox)row.FindControl("txtSHIMPENTMOD"));
                TextBox txtPALLETTYPE = ((TextBox)row.FindControl("txtPALLETTYPE"));
                TextBox txtDESTINATION = ((TextBox)row.FindControl("txtDESTINATION"));
                TextBox txtTRADE = ((TextBox)row.FindControl("txtTRADE"));
                TextBox txtCONTAINERQTYHC = ((TextBox)row.FindControl("txtCONTAINERQTYHC"));
                TextBox txtCONTAINERQTY40FT = ((TextBox)row.FindControl("txtCONTAINERQTY40FT"));
                TextBox txtCONTAINERQTY20FT = ((TextBox)row.FindControl("txtCONTAINERQTY20FT"));
                TextBox txtNOOFPALLET = ((TextBox)row.FindControl("txtNOOFPALLET"));
                TextBox txtSTDCONTLOAD = ((TextBox)row.FindControl("txtSTDCONTLOAD"));
                TextBox txtCONTUSAGE = ((TextBox)row.FindControl("txtCONTUSAGE"));
                TextBox txtCONTUSAGERATIO = ((TextBox)row.FindControl("txtCONTUSAGERATIO"));
                TextBox txtCAS = ((TextBox)row.FindControl("txtCAS"));
                TextBox txtCTRNUMBER = ((TextBox)row.FindControl("txtCTRNUMBER"));
                Label lblCTRNUMBERARRIVALDATE = ((Label)row.FindControl("lblCTRNUMBERARRIVALDATE"));
                TextBox txtCONFIRMEDDATE = ((TextBox)row.FindControl("txtCONFIRMEDDATE"));
                TextBox txtSHIPPINGLIN = ((TextBox)row.FindControl("txtSHIPPINGLIN"));
                TextBox txt1STVESSEL = ((TextBox)row.FindControl("txt1STVESSEL"));
                TextBox txt2NDVESSEL = ((TextBox)row.FindControl("txt2NDVESSEL"));
                TextBox txtVESSELDESTINATION = ((TextBox)row.FindControl("txtVESSELDESTINATION"));
                TextBox txtPORTOFDISCHARGE = ((TextBox)row.FindControl("txtPORTOFDISCHARGE"));
                TextBox txtETADISCHARGEPORT = ((TextBox)row.FindControl("txtETADISCHARGEPORT"));
                TextBox txtCYCUTOFFEXFACTCUTOFF = ((TextBox)row.FindControl("txtCYCUTOFFEXFACTCUTOFF"));
                TextBox txtLOADINGPORT = ((TextBox)row.FindControl("txtLOADINGPORT"));
                TextBox txtREASONOFDELAYEDEXFACT = ((TextBox)row.FindControl("txtREASONOFDELAYEDEXFACT"));
                TextBox txtREASONOFDELAYEDOB = ((TextBox)row.FindControl("txtREASONOFDELAYEDOB"));
                TextBox txtREMARKS = ((TextBox)row.FindControl("txtREMARKS"));

                //DataView dvCheckDup = new DataView();
                //string  dupGNSINVOICENUMBER = "",
                //        dupODNO = "",
                //        dupLOTNO = "",
                //        dupPONUMBER = "",
                //        dupITEMCODE = "",
                //        dupPLANNEDEXFACTDATE = "",
                //        dupPLANNEDOBDATE = "",
                //        dupQTY = "",
                //        dupCUSTOMER = "",
                //        dupSHIMPENTMOD = "",
                //        dupPALLETTYPE = "",
                //        dupDESTINATION = "",
                //dvCheckDup = maint.GET_WEEKLY_SHIPMENT_SCHEDULE_DUPLICATE_VALIDATION(strGNSINVOICENUMBERCHECK).Tables[0].DefaultView;

                //if (dvCheckDup.Count > 0)
                //{
                //    for (int i = 0; i < dvCheckDup.Count; i++)
                //    {
                //        dupGNSINVOICENUMBER = dvCheckDup[0]["GNSINVOICENUMBER"].ToString();
                //        dupODNO = dvCheckDup[0]["ODNO"].ToString();
                //        dupLOTNO = dvCheckDup[0]["LOTNO"].ToString();
                //        dupPONUMBER = dvCheckDup[0]["PONUMBER"].ToString();
                //        dupITEMCODE = dvCheckDup[0]["ITEMCODE"].ToString();
                //        dupPLANNEDEXFACTDATE = dvCheckDup[0]["PLANNEDEXFACTDATE"].ToString();
                //        dupPLANNEDOBDATE = dvCheckDup[0]["PLANNEDOBDATE"].ToString();
                //        dupQTY = dvCheckDup[0]["QTY"].ToString();
                //        dupCUSTOMER = dvCheckDup[0]["CUSTOMER"].ToString();
                //        dupSHIMPENTMOD = dvCheckDup[0]["SHIMPENTMOD"].ToString();
                //        dupPALLETTYPE = dvCheckDup[0]["PALLETTYPE"].ToString();
                //        dupDESTINATION = dvCheckDup[0]["DESTINATION"].ToString();

                //    }


                //    MsgBox1.alert("Saving cancelled! Duplicate data has been detected!");
                //    return;
                //}

                maint.UPDATE_WEEKLY_SHIPMENT_SCHEDULE
                    (
                        lblID.Text.Trim(),
                        txtGNSINVOICENUMBER.Text.Trim(),
                        txtPRODUCTIONLOCATION.Text.Trim(),
                        txtSHIPPINGLOCATION.Text.Trim(),
                        txtODNO.Text.Trim(),
                        txtLOTNO.Text.Trim(),
                        txtSHIPMENTNO.Text.Trim(),
                        txtPOMONTH.Text.Trim(),
                        txtPONUMBER.Text.Trim(),
                        txtCONSIGNEEPONUMBER.Text.Trim(),
                        txtPOWK.Text.Trim(),
                        txtITEMCODE.Text.Trim(),
                        txtMODELDESCRIPTION.Text.Trim(),
                        txtMODELNAME.Text.Trim(),
                        txtPLANNEDEXFACTDATE.Text.Trim(),
                        txtPLANNEDOBDATE.Text.Trim(),
                        //txtREVISEDEXFACEDATE.Text.Trim(),
                        //txtREVISEDOBDATE.Text.Trim(),
                        txtQTY.Text.Trim(),
                        txtCUSTOMER.Text.Trim(),
                        txtSHIMPENTMOD.Text.Trim(),
                        txtPALLETTYPE.Text.Trim(),
                        txtDESTINATION.Text.Trim(),
                        txtTRADE.Text.Trim(),
                        txtCONTAINERQTYHC.Text.Trim(),
                        txtCONTAINERQTY40FT.Text.Trim(),
                        txtCONTAINERQTY20FT.Text.Trim(),
                        txtNOOFPALLET.Text.Trim(),
                        txtSTDCONTLOAD.Text.Trim(),
                        txtCONTUSAGE.Text.Trim(),
                        txtCONTUSAGERATIO.Text.Trim(),
                        txtCAS.Text.Trim(),
                        txtCTRNUMBER.Text.Trim(),
                        lblCTRNUMBERARRIVALDATE.Text.Trim(),
                        txtCONFIRMEDDATE.Text.Trim(),
                        txtSHIPPINGLIN.Text.Trim(),
                        txt1STVESSEL.Text.Trim(),
                        txt2NDVESSEL.Text.Trim(),
                        txtVESSELDESTINATION.Text.Trim(),
                        txtPORTOFDISCHARGE.Text.Trim(),
                        txtETADISCHARGEPORT.Text.Trim(),
                        txtCYCUTOFFEXFACTCUTOFF.Text.Trim(),
                        txtLOADINGPORT.Text.Trim(),
                        txtREASONOFDELAYEDEXFACT.Text.Trim(),
                        txtREASONOFDELAYEDOB.Text.Trim(),
                        txtREMARKS.Text.Trim(),
                        strUserID,
                        Session["UPDATETYPE"].ToString()
                    );



                MsgBox1.alert("Successfully saved!");

                GetWeeklyShipmentSchedule();

                maint.ALERT_NEWLY_POSTED_REVISED();
            }
            catch (Exception ex)
            {
                MsgBox1.alert(ex.Message.ToString());
            }


        }





        private void FixAppDomainRestartWhenTouchingFiles()
        {

            System.Reflection.PropertyInfo p = typeof(HttpRuntime).GetProperty("FileChangesMonitor", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            object o = p.GetValue(null, null);

            System.Reflection.FieldInfo f = o.GetType().GetField("_dirMonSubdirs", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.IgnoreCase);

            object monitor = f.GetValue(o);

            System.Reflection.MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            m.Invoke(monitor, new object[0]);

        }



        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (grdWeeklyShipmentSchedule.Rows.Count < 1)
            {
                MsgBox1.alert("No data to export!");
                return;
            }

            ExportToExcel();


        }



        public void ExportToExcel()
        {

            try
            {
                string filename = "WSS_ " + DateTime.Today.ToString("(MMddyy)");
                //Turn off the view stateV 225 55
                this.EnableViewState = false;
                //Remove the charset from the Content-Type header
                Response.Charset = String.Empty;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename='" + filename + "'.xls");
                Response.Charset = "";
                this.GetWeeklyShipmentSchedule();
                // If you want the option to open the Excel file without saving then
                // comment out the line below
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                dvExport.RenderControl(htmlWrite);

                //Append CSS file

                System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("../App_Themes/Stylesheet/Main.css"));
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.StreamReader sr = fi.OpenText();
                while (sr.Peek() >= 0)
                {
                    sb.Append(sr.ReadLine());
                }
                sr.Close();

                Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style><head>" + stringWrite.ToString() + "</html>");

                stringWrite = null;
                htmlWrite = null;

                // Response.Write(stringWrite.ToString());

                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

                MsgBox1.alert(ex.Message.ToString());
            }
        }

        protected void grdExport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblColorDisplay;
            Label lblWSSSTATUSFLAG;
            string strRowType = e.Row.RowType.ToString();
            if (strRowType != "Header" && strRowType != "Footer")
            {
                lblColorDisplay = (Label)e.Row.FindControl("lblColorDisplay");
                lblWSSSTATUSFLAG = (Label)e.Row.FindControl("lblWSSSTATUSFLAG");

                e.Row.BackColor = System.Drawing.Color.FromName(lblColorDisplay.Text);
            }

        }




        public void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in grdWeeklyShipmentSchedule.Rows)
                {

                    CheckBox chkBox = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                    if (chkBox.Enabled == true)
                    {
                        if (chkBox.Enabled)
                        {
                            if (((CheckBox)sender).Checked == true)
                            {
                                chkBox.Checked = true;
                            }
                            else
                            {
                                chkBox.Checked = false;
                            }
                        }
                        else
                        {
                            chkBox.Checked = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                MsgBox1.alert("Error : An unexpected error has occured! " + ex.Message);

            }


        }
    }
}