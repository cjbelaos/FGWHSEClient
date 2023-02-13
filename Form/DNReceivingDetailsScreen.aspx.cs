using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.eppi.utils;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;
using FGWHSEClient.DAL;

namespace FGWHSEClient.Form
{
    public partial class DNReceivingDetailsScreen : System.Web.UI.Page
    {
        public DeliveryReceivingDAL drDal = new DeliveryReceivingDAL();
        public DataTable dtable;
        public string strPageSubsystem = "";
        public string strAccessLevel = "";
        public int iRfidQty = -1;
        public int iRfidTotal = -1;
        public bool isReceive = true;

        Maintenance maint = new Maintenance();
        DeleteDNDAL DeleteDNDAL = new DeleteDNDAL();

        string strDNNew = "";

        

        protected void Page_Load(object sender, EventArgs e)
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
                strPageSubsystem = "FGWHSE_016";
                if (!checkAuthority(strPageSubsystem))
                {
                    //Response.Write("<script>");
                    //Response.Write("alert('You are not authorized to access the page.');");
                    //Response.Write("window.location = 'Default.aspx';");
                    //Response.Write("</script>");


                }

                if (!this.IsPostBack)
                {
                    Fill_DN_HeaderDetails();
                }
                else
                {
                    if (Request.Form["deleteConfirm"] != null)
                    {
                        if (Request.Form["deleteConfirm"].ToString().Equals("1"))
                        {
                            // deleteRowRecord();
                            // Search(txtPartCode.Text);
                            MsgBox1.alert("Deleted" + HiddenField1.Value);

                        }
                    }

                    if (Request.Form["deleteConfirm2"] != null)
                    {
                        if (Request.Form["deleteConfirm2"].ToString().Equals("1"))
                        {
                            DeleteRFIDTag();
                        }
                    }


                    
                }

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
                        string strRole = "";

                        if (iRow >= 0)
                        {
                            isValid = true;
                            strRole = dvSubsystem.Table.Rows[iRow]["ROLE"].ToString();
                        }
                        else
                        {
                            isValid = false;
                        }

                        //string strRole = dvSubsystem.Table.Rows[iRow]["ROLE"].ToString();

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
        private void Fill_DN_HeaderDetails()
        {
            try
            {
                DataView dv = new DataView();
                dv = drDal.DN_GetDN_Header(Request["DNno"].ToString().Trim().ToUpper());

                if (dv.Count > 0)
                {
                    if (dv.Table.Rows.Count  > 0)
                    {
                        lblDNNo.Text = dv.Table.Rows[0]["barcode"].ToString().Trim().ToUpper();
                        lblInvoiceNo.Text = dv.Table.Rows[0]["invoice"].ToString().Trim().ToUpper();
                        lblTotalRFIDCount.Text = dv.Table.Rows[0]["rfidqty"].ToString().Trim().ToUpper() + "/" +
                                                dv.Table.Rows[0]["rfidtotal"].ToString().Trim().ToUpper();

                        iRfidQty = Convert.ToInt32(dv.Table.Rows[0]["rfidqty"].ToString().Trim().ToUpper());
                        iRfidTotal = Convert.ToInt32(dv.Table.Rows[0]["rfidtotal"].ToString().Trim().ToUpper());

                        if (dv.Table.Rows[0]["DNRECEIVESTATUSID"].ToString().Trim().ToUpper() == "2")
                        {
                            lblConfirmedBy.Text = "CONFIRMED BY : " + dv.Table.Rows[0]["CONFIRMEDBY"].ToString().Trim().ToUpper();
                            lblConfirmedBy.Visible = true;
                            lnkbtnConfirmDelivery.Visible = false;
                            lnlbtnChangeDN.Visible = false;
                            lblImageChangeDN.Visible = false;
                            lblImageConfirm.Visible = false;
                        }
                        else if (dv.Table.Rows[0]["DNRECEIVESTATUSID"].ToString().Trim().ToUpper() == "3" ||
                            dv.Table.Rows[0]["DNRECEIVESTATUSID"].ToString().Trim().ToUpper() == "4") // STATUS IS DELETED (3) OR CHANGE DN (4)
                        {
                            lblImageChangeDN.Visible = false;
                            lnlbtnChangeDN.Visible = false;
                            lblImageConfirm.Visible = false;
                            lnkbtnConfirmDelivery.Visible = false;
                        }
                        else
                        {
                            lblConfirmedBy.Visible = false;
                            lnkbtnConfirmDelivery.Visible = true;
                            lnlbtnChangeDN.Visible = true;
                            lblImageChangeDN.Visible = true;
                            lblImageConfirm.Visible = true;
                        }

                        //if (iRfidQty == iRfidTotal)   
                        //{
                        //    lnkbtnConfirmDelivery.Visible = true;
                        //    lnlbtnChangeDN.Visible = false;
                        //    lblImageChangeDN.Visible = false;
                        //    lblImageConfirm.Visible = true;
                        //}
                        //else
                        //{
                        //    lnkbtnConfirmDelivery.Visible = false;
                        //    lnlbtnChangeDN.Visible = true;
                        //    lblImageChangeDN.Visible = true;
                        //    lblImageConfirm.Visible = false;
                        //}

                        Fill_DN();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        private void Fill_DN()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = drDal.DN_GetDNDetails(lblDNNo.Text);

                if (ds.Tables.Count > 0)
                {
                   
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        Label lblHeader = new Label();
                        Label lblFooter = new Label();
                        string strPartCode = "";
                        string strPartName = "";
                        int iRcvCount = 0;
                        int iRcvCountTotal = 0;
                        int iRcvQty = 0;
                        int iRcvQtyTotal = 0;
                     
                        
                        strPartCode = ds.Tables[i].Rows[0][2].ToString();
                        strPartName = ds.Tables[i].Rows[0][10].ToString();

                        foreach (DataRow dr in ds.Tables[i].Rows)
                        {

                            int iQty = Convert.ToInt32(dr["RCVQTY"].ToString().Trim().ToUpper());

                            if (dr["STATUS"].ToString().Trim().ToUpper() == "RECEIVED")
                            {
                                iRcvCount++;
                                iRcvQty += iQty;
                            }


                            iRcvQtyTotal = Convert.ToInt32(dr["DNQTY"].ToString().Trim().ToUpper());
                            iRcvCountTotal++;
                            
                        }



                        lblHeader.Text = "<table style='background-color:#ebeded' width='1160px'><tr><td colspan=7 style='font-size:18px;font-weight:bolder;'>" + strPartCode + " - " + strPartName + "</td></tr>" +
                                         "<tr><td width='55px'style='font-weight:normal; font-size:16px;'  align='center'>" + iRcvCount.ToString() + "/" + iRcvCountTotal.ToString() + "</td>" +
                                         "<td width='300px'></td>" +
                                         "<td width='350px'></td>" +
                                         "<td width='590px'></td>" +
                                         "<td  width='100px' style='font-weight:normal; font-size:16px;''  align='center'>" + iRcvQty.ToString() + "/" + iRcvQtyTotal.ToString() + "</td>" +
                                         "<td width='250px'></td>" +
                                         "<td width='250px'></td>" +
                                         "</tr></table>";

                        lblFooter.Text = "</br>";

                        
                        GridView objGV = new GridView();
                        objGV.ID = "GV" + i;
                        objGV.AutoGenerateColumns = false;
                        objGV.GridLines = GridLines.Both;
                        objGV.Width = 1160;
                        //objGV.DataBound += objGV_DataBound;
                        objGV.RowDataBound += objGV_RowDataBound;
                        objGV.RowDeleting += objGV_RowDeleting;
                        objGV.RowCommand += objGV_RowCommand;
                        objGV.EnableViewState = false;

                        

                        BoundField no = new BoundField();
                        no.HeaderText = "No.";
                        no.DataField = "NO";
                        no.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        no.HeaderStyle.Font.Size = 14;
                        no.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        no.HeaderStyle.ForeColor = Color.White;
                        no.ItemStyle.Font.Size = 11;
                        no.ItemStyle.ForeColor = Color.Black;
                        no.ItemStyle.Width = 50;
                        no.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(no);


                        BoundField rfidtag = new BoundField();
                        rfidtag.HeaderText = "RFID Tag";
                        rfidtag.DataField = "RFIDTAG";
                        rfidtag.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        rfidtag.HeaderStyle.Font.Size = 14;
                        rfidtag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        rfidtag.HeaderStyle.ForeColor = Color.White;
                        rfidtag.ItemStyle.Font.Size = 11;
                        rfidtag.ItemStyle.ForeColor = Color.Black;
                        rfidtag.ItemStyle.Width = 250;
                        rfidtag.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(rfidtag);

                        BoundField refno = new BoundField();
                        refno.HeaderText = "Ref No.";
                        refno.DataField = "REFNO";
                        refno.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        refno.HeaderStyle.Font.Size = 14;
                        refno.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        refno.HeaderStyle.ForeColor = Color.White;
                        refno.ItemStyle.Font.Size = 11;
                        refno.ItemStyle.ForeColor = Color.Black;
                        refno.ItemStyle.Width = 250;
                        refno.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(refno);

                        BoundField lotno = new BoundField();
                        lotno.HeaderText = "Lot No.";
                        lotno.DataField = "LOTNO";
                        lotno.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        lotno.HeaderStyle.Font.Size = 14;
                        lotno.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        lotno.HeaderStyle.ForeColor = Color.White;
                        lotno.ItemStyle.Font.Size = 11;
                        lotno.ItemStyle.ForeColor = Color.Black;
                        lotno.ItemStyle.Width = 250;
                        lotno.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(lotno);


                        BoundField rcvqty = new BoundField();
                        rcvqty.HeaderText = "Qty";
                        rcvqty.DataField = "RCVQTY";
                        rcvqty.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        rcvqty.HeaderStyle.Font.Size = 14;
                        rcvqty.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        rcvqty.HeaderStyle.ForeColor = Color.White;
                        rcvqty.ItemStyle.Font.Size = 11;
                        rcvqty.ItemStyle.ForeColor = Color.Black;
                        rcvqty.ItemStyle.Width = 100;
                        rcvqty.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(rcvqty);

                        BoundField remarks = new BoundField();
                        remarks.HeaderText = "Remarks";
                        remarks.DataField = "REMARKS";
                        remarks.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        remarks.HeaderStyle.Font.Size = 14;
                        remarks.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        remarks.HeaderStyle.ForeColor = Color.White;
                        remarks.ItemStyle.Font.Size = 11;
                        remarks.ItemStyle.ForeColor = Color.Black;
                        remarks.ItemStyle.Width = 200;
                        //remarks.ItemStyle.Wrap = true;
                        remarks.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(remarks);


                        BoundField status = new BoundField();
                        status.HeaderText = "Status";
                        status.DataField = "STATUS";
                        status.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        status.HeaderStyle.Font.Size = 14;
                        status.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        status.HeaderStyle.ForeColor = Color.White;
                        status.ItemStyle.Font.Size = 11;
                        status.ItemStyle.ForeColor = Color.Black;
                        status.ItemStyle.Width = 110;
                        status.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        objGV.Columns.Add(status);

                        //03MAR2021 
                        //CommandField delete2 = new CommandField();
                        //delete2.ButtonType = ButtonType.Button;
                        //delete2.DeleteImageUrl = "../Image/Delete.png";
                        //delete2.ShowDeleteButton = true;
                        //delete2.Visible = true;
                        //delete2.HeaderText = "";
                        //delete2.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        //delete2.HeaderStyle.Font.Size = 14;
                        //delete2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        //delete2.HeaderStyle.ForeColor = Color.White;
                        //delete2.ItemStyle.Font.Bold = false;
                        //delete2.ItemStyle.Width = 70;
                        //objGV.Columns.Add(delete2);

                        //// Create the TemplateField 
                        //TemplateField firstName = new TemplateField();
                        //firstName.HeaderText = "First_Name";
                        //firstName.HeaderStyle.BackColor = Color.FromArgb(83, 141, 213);
                        //firstName.HeaderStyle.Font.Size = 14;
                        //firstName.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        //firstName.HeaderStyle.ForeColor = Color.White;
                        //firstName.ItemTemplate = new TemplateGenerator(ButtonType.Button, "FirstName");
                        //objGV.Columns.Add(firstName);
                        //03MAR2021 


                        objGV.DataSource = ds.Tables[i];
                        objGV.DataBind();


                        pnl.Controls.Add(lblHeader);
                        pnl.Controls.Add(objGV);
                        pnl.Controls.Add(lblFooter);

                       
                    }

                    
                }


            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        void objGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView grid = sender as GridView;

                int rownum = 0;
                rownum++;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int vColumnCnt = -1;
                    foreach (TableCell vCell in e.Row.Cells)
                    {
                        vColumnCnt++;
                        //if (.Cells[vColumnCnt].Text.ToString().ToUpper() == "DN NO.")
                        //{
                        //}
                        if (grid.HeaderRow.Cells[vColumnCnt].Text.ToString().Trim().ToUpper() == "STATUS")
                        {
                            if (e.Row.Cells[vColumnCnt].Text.ToString().Trim().ToUpper() == "RECEIVED")
                            {
                                e.Row.BackColor = Color.FromArgb(204, 255, 204);
                                //delete2.Visible = false;
                            }
                            else
                            {
                                isReceive = false;
                            }
                        }
                    }

                }

                    ////foreach (Button button in e.Row.Cells[7].Controls.OfType<Button>())
                    ////{
                
                        int i = e.Row.RowIndex;
                        Button delbutton = (Button)e.Row.Cells[7].Controls[0];
                       // delbutton.OnClientClick = "if (!confirm('Are you sure you want to delete this record?')) return;";
                        //delbutton.Click += new System.EventHandler(this.btnDisplay_Click);
                        delbutton.CommandName = "Delete"; 
                        delbutton.CommandArgument = "<%# Container.DataItemIndex %>";
                        delbutton.OnClientClick = "return refreshPage();";
                        //delbutton.CommandName = "Delete";
                        //delbutton.CommandArgument = "<%# Container.DataItemIndex %>";
                        HiddenField1.Value = e.Row.Cells[2].Text.ToString();
                        //HiddenField1.Value = objGV.Rows[i].Cells[2].ToString();
                      //  HiddenField1.Value = 
                       // HiddenField1.Value = e.Row.RowIndex.ToString();
                        return;

                       //btnDisplay_Click(null, new EventArgs());  
                        //objGV_RowCommand(null, new GridViewCommandEventArgs());

                       // HiddenField1.Value = row.Cells[2].Text;
                        
                     //   delbutton.OnClientClick += "btnDisplay_Click";
                        //delbutton.OnClientClick += new System.EventHandler(this.btnDisplay_Click);
                       // MsgBox1.confirm("Are you sure you want to delete this record?", "deleteConfirm");

                        ////if (button.CommandName == "Delete")
                        ////{

                        ////    try
                        ////    {
                        ////        MsgBox1.confirm("Are you sure you want to delete this record?", "deleteConfirm");
                        ////        //     HiddenField1.Value = grdItemControLimitMaster.Rows[e.RowIndex].Cells[1].Text;
                        ////    }
                        ////    catch (Exception ex)
                        ////    {
                        ////        Logger.GetInstance().Fatal(ex.StackTrace, ex);
                        ////        MsgBox1.alert(ex.Message);
                        ////    }

                        ////}

                    ////}

                    
               
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void lnlbtnChangeDN_Click(object sender, EventArgs e)
        {
            try
            {
                Fill_DN();

                strPageSubsystem = "FGWHSE_019";
                if (!checkAuthority(strPageSubsystem))
                {
                    //Response.Write("<script>");
                    //Response.Write("alert('You are not authorized to Confirm Delivery.');");
                    //Response.Write("</script>");
                    MsgBox1.alert("You are not authorized to Change DN.");
                }
                else
                {
                    lblModalDNNo.Text = lblDNNo.Text;
                    txtModalNewDNNo.Text = "";
                   // txtModalPersonIC.Text = "";
                    txtModalReason.Text = "";
                    //ModalPopupChangeDN.Show();
                    ModalPopupApprove.Show();
                }


                

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnModalCancel_Click(object sender, EventArgs e)
        {
            lblModalDNNo.Text = "";
            txtModalNewDNNo.Text = "";
           // txtModalPersonIC.Text = "";
            txtModalReason.Text = "";

            Fill_DN_HeaderDetails();

        }

        protected void btnModalSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValid())
                {
                    DataSet ds = new DataSet();
                    ds = DeleteDNDAL.CHECK_DN_IF_EXISTS(txtModalNewDNNo.Text.Trim());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string strchange = "";
                        strchange = DeleteDNDAL.DN_CHANGE_DN_FUNCTION(txtModalNewDNNo.Text.Trim(), lblDNNo.Text.Trim(), Session["UserID"].ToString());

                        string strexec = "";
                        strexec = DeleteDNDAL.DN_UPDATE_DNNO_CHANGELOG_2(lblDNNo.Text.ToString().Trim().ToUpper(),
                                                               txtModalNewDNNo.Text.ToString().Trim().ToUpper(),
                                                                txtModalReason.Text.ToString().Trim().ToUpper(), Session["UserID"].ToString());

                        //MsgBox1.alert("DN No successfully updated!");
                        //Fill_DN_HeaderDetails();
                        HiddenField2.Value = txtModalNewDNNo.Text.ToString().Trim().ToUpper();

                        DataSet ds2 = new DataSet();
                        ds2 = DeleteDNDAL.CHECK_RFID_NOT_RECEIVED(txtModalNewDNNo.Text.Trim());
                        if (ds2.Tables[0].Rows.Count > 0)
                        {                         
                            grdDN.DataSource = ds2;
                            grdDN.DataBind();
                            ModalPopupDelete.Show();
                        }
                        else
                        {
                            Response.Write("<script>");
                            Response.Write("alert('DN No successfully updated!');");
                            Response.Write("</script>");

                            Response.Redirect("~/Form/DNReceivingDetailsScreen.aspx?DNNo=" + HiddenField2.Value);
                            Fill_DN_HeaderDetails();
                        }

                    }
                    else
                    {
                        lblError.Text = "New DN does not exist!";
                        ModalPopupChangeDN.Show();
                    }
                }
                else
                {
                    lblError.Text = "Invalid! Please check fields.";
                    ModalPopupChangeDN.Show();
                    Fill_DN_HeaderDetails();
                }

                //////string strDNNo = "";
                //////string strPartCode = "";
                //////int iQty = 0;
                //////int iResult = 0;
                //////int iResult2 = 0;
                ////////validate fields

                //////string strOldDNNo = lblDNNo.Text.ToUpper().Trim();

                //////if (isValid())
                //////{
                //////    //check if DN No exists in DN Data
                //////    DataSet dsExists = new DataSet();
                //////    dsExists = drDal.DN_GetNewDNNO(txtModalNewDNNo.Text.ToString().Trim().ToUpper());
                //////    if (dsExists.Tables[0].Rows.Count > 0)
                //////    {
                //////        //get partcode and partname
                //////        foreach (DataRow dr in dsExists.Tables[0].Rows)
                //////        {
                //////            strDNNo = dr["BARCODE"].ToString().Trim().ToUpper();
                //////            strPartCode = dr["MATNR"].ToString().Trim().ToUpper();
                //////            iQty = Convert.ToInt32(dr["MENGE"].ToString().Trim().ToUpper());

                //////            iResult = drDal.DN_UpdateDNNo(strDNNo,
                //////                                          strPartCode,
                //////                                          iQty,
                //////                                          strOldDNNo);

                //////            if (iResult == 1)
                //////            {
                //////                iResult++;
                //////            }
                //////        }

                //////        if (iResult > 0)
                //////        {
                //////            string strexec = "";
                //////            strexec = .DN_UPDATE_DNNO_CHANGELOG(lblDNNo.Text.ToString().Trim().ToUpper(),
                //////                                                   txtModalNewDNNo.Text.ToString().Trim().ToUpper(),
                //////                                                    txtModalReason.Text.ToString().Trim().ToUpper(), Session["UserID"].ToString());
                //////            if (iResult2 == 1)
                //////            {
                //////                MsgBox1.alert("DN No successfully updated");

                //////                //update query string value
                //////                Fill_DN_HeaderDetails();
                //////            }
                //////        }
                //////    }
                //////    else
                //////    {
                //////        MsgBox1.alert("DN No. not found!");
                //////        Fill_DN_HeaderDetails();
                //////    }
                //////}
                //////else
                //////{
                //////    MsgBox1.alert("Invalid! Please check fields.");
                //////    Fill_DN_HeaderDetails();
                //////}

            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }
        public bool isValid()
        {
            bool isValid = true;

         

            if (txtModalNewDNNo.Text.ToString().Trim().ToUpper() != "")
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }

            //if (txtModalPersonIC.Text.ToString().Trim().ToUpper() != "")
            //{
            //    isValid = true;
            //}
            //else
            //{
            //    isValid = false;
            //}


            if (txtModalReason.Text.ToString().Trim().ToUpper() != "")
            {
                if (txtModalReason.Text.Length < 15)
                {
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }

        protected void lnkbtnConfirmDelivery_Click(object sender, EventArgs e)
        {
            Fill_DN();

            strPageSubsystem = "FGWHSE_016";
            if (!checkAuthority(strPageSubsystem))
            {
                //Response.Write("<script>");
                //Response.Write("alert('You are not authorized to Confirm Delivery.');");
                //Response.Write("</script>");
                MsgBox1.alert("You are not authorized to Confirm Delivery.");
            }
            else
            {
                if (isReceive)
                {
                    lblModalConfirmDelivery.Text = lblDNNo.Text;
                    ModalPopupConfirmDN.Show();
                }
                else
                {
                    MsgBox1.alert("Delivery not yet complete.");
                }
                
            }
           
        }

        protected void btnModalConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                
                int iResult = 0;


                if (iRfidQty == iRfidTotal)
                {


                    iResult = drDal.DN_UpdateDNNo_Status(lblModalConfirmDelivery.Text.ToString().Trim().ToUpper(), Session["UserName"].ToString().Trim().ToUpper());

                    if (iResult == 1)
                    {

                        MsgBox1.alert("Delivery Confirmation successful!");
                        Fill_DN_HeaderDetails();
                        lnlbtnChangeDN.Visible = false;
                        lnkbtnConfirmDelivery.Visible = false;

                    }
                }
                else
                {
                    MsgBox1.alert("DN not yet complete!");
                    Fill_DN_HeaderDetails();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnModalCancelDelivery_Click(object sender, EventArgs e)
        {

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            bool isAuthorized = ServiceLocator.GetLoginService().IsAuthorized("FGWHSE Monitoring", "FGWHSE_019", txtUsername.Text.Trim());
            string strSubSystemRole = "";
            string strSystemName = "FGWHSE Monitoring";
            DataView dvUser = new DataView();

            if (isAuthorized)
            {
                //validate username and password in AD
                bool isAuthenticated = ServiceLocator.GetLdapService().IsAuthenticated(txtUsername.Text, txtPassword.Text);
                if (isAuthenticated)
                {
                    DsUser ds = ServiceLocator.GetLoginService().GetUser(txtUsername.Text);
                    if (ds.SystemUser.Rows.Count <= 0)
                    {
                        MsgBox1.alert("User profile is incomplete! " +
                            "Please contact the administrator " +
                            "to check if user has all information required. ");
                        return;
                    }


                    dvUser = maint.GetUsersLDAP(txtUsername.Text, strSystemName);
                    if (dvUser.Count > 0)
                    {
                        Session["UserID"] = Convert.ToString(dvUser[0]["UserID"]);
                        Session["UserName"] = Convert.ToString(dvUser[0]["UserName"]);
                        Session["RoleID"] = Convert.ToString(dvUser[0]["Role"]);
                        //GetUserSubsystems();
                        //isLogin = true;

                        GetAccess();
                    }
                    else
                    {
                        //MsgBox1.alert("Invalid username/password!");
                        lblErrorMsg.Text = "Invalid username/password!";
                        ModalPopupApprove.Show();
                        //return;
                    }

                }
                else
                {
                    dvUser = maint.GetUser(strSystemName, txtUsername.Text.Trim(), txtPassword.Text.Trim(), 0);
                    if (dvUser.Count > 0)
                    {
                        Session["UserID"] = Convert.ToString(dvUser[0]["User_ID"]);
                        Session["UserName"] = Convert.ToString(dvUser[0]["User_Name"]);
                        Session["RoleID"] = Convert.ToString(dvUser[0]["Role"]);
                        //GetUserSubsystems();
                        //isLogin = true;
                        GetAccess();
                    }
                    else
                    {
                        //MsgBox1.alert("Invalid username/password!");
                        lblErrorMsg.Text = "Invalid username/password!";
                        ModalPopupApprove.Show();
                        //return;
                    }
                }
            }
            else
            {

                lblErrorMsg.Text = "User not authorized to change DN, please input user with authority.";
                ModalPopupApprove.Show();
            }
        }

        private void GetAccess()
        {
            string strSubSystemRole = "";

            //check user role
            DataSet ds = maint.GetUserSubsystemRole(txtUsername.Text.Trim(), "FGWHSE Monitoring", "FGWHSE_019");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strSubSystemRole = ds.Tables[0].Rows[0]["ROLE"].ToString();
                    Session["ROLEID"] = strSubSystemRole;

                    if (strSubSystemRole == "4")//ROLE IS USER
                    {
                        //btnNewApp.Visible = false;
                        //GetSectionByUser();

                        lblErrorMsg.Text = "User not authorized to change DN, please input user with authority.";
                        ModalPopupApprove.Show();
                    }
                    else if (strSubSystemRole == "3" || strSubSystemRole == "1") //ROLE IS KEY USER  OR ADMIN
                    {
                        //btnNewApp.Visible = true;
                        //GetSectionByUser();
                      //  string strexec = DeleteDNDAL.DN_UPDATE_DELETEDDN(txtDNNo.Text.Trim(), Session["UserID"].ToString());

                      //  lblMessage.Text = "SUCCESSFULLY DELETED";
                      //  lblMessage.ForeColor = Color.Green;

                     //   grdRFID.DataSource = null;
                     //   grdRFID.DataBind();

                        //txtUsername.Text = "";
                        //txtPassword.Text = "";
                        //lblErrorMsg.Text = "";
                    //    txtDNNo.Text = "";
                        //ModalPopupExtender1.Show();
                        ModalPopupChangeDN.Show();
                    }
                    //else //ROLE IS ADMIN - 1
                    //{
                    //    //btnNewApp.Visible = true;
                    //    //GetSection();
                    //}
                }
                else
                {
                    lblErrorMsg.Text = "User not authorized to change DN, please input user with authority.";
                    ModalPopupApprove.Show();
                }

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Fill_DN_HeaderDetails();
        }

        void objGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    MsgBox1.confirm("Are you sure you want to delete this record?", "deleteConfirm");
                    //     HiddenField1.Value = grdItemControLimitMaster.Rows[e.RowIndex].Cells[1].Text;
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().Fatal(ex.StackTrace, ex);
                    MsgBox1.alert(ex.Message);
                }

            }
        }

        void objGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                try
                {
                    MsgBox1.confirm("Are you sure you want to delete this record?", "deleteConfirm");
               //     HiddenField1.Value = grdItemControLimitMaster.Rows[e.RowIndex].Cells[1].Text;
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().Fatal(ex.StackTrace, ex);
                    MsgBox1.alert(ex.Message);
                }

                ////GridView grid = sender as GridView;

                ////int rownum = 0;
                ////rownum++;
                ////if (e.Row.RowType == DataControlRowType.DataRow)
                ////{
                ////    int vColumnCnt = -1;
                ////    foreach (TableCell vCell in e.Row.Cells)
                ////    {
                ////        vColumnCnt++;
                ////        //if (.Cells[vColumnCnt].Text.ToString().ToUpper() == "DN NO.")
                ////        //{
                ////        //}
                ////        if (grid.HeaderRow.Cells[vColumnCnt].Text.ToString().Trim().ToUpper() == "STATUS")
                ////        {
                ////            if (e.Row.Cells[vColumnCnt].Text.ToString().Trim().ToUpper() == "RECEIVED")
                ////            {
                ////                e.Row.BackColor = Color.FromArgb(204, 255, 204);
                ////                //delete2.Visible = false;
                ////            }
                ////            else
                ////            {
                ////                isReceive = false;
                ////            }
                ////        }
                ////    }
                ////}
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
           // Button delete2 = (sender as Button);
           // GridViewRow row = (delete2.NamingContainer as GridViewRow);
           //// string id = delbutton.CommandArgument;
           // //    string name = row.Cells[2].Text;
           // HiddenField1.Value = row.Cells[2].Text;

            //Get the button that raised the event


            MsgBox1.confirm("Are you sure you want to delete this record?", "deleteConfirm");
            //Fill_DN_HeaderDetails();
            //return;

           // objGV_RowCommand(object sender, GridViewCommandEventArgs e)
        }

        protected void chkHeaderItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in grdDN.Rows)
                {

                    CheckBox chkBox = (CheckBox)gvr.Cells[0].FindControl("chkItem");
                    if (chkBox.Enabled)
                    {
                        chkBox.Checked = ((CheckBox)sender).Checked;
                        ModalPopupDelete.Show();
                    }
                    else
                    {
                        chkBox.Checked = false;
                        ModalPopupDelete.Show();
                    }
                }



            }
            catch (Exception ex)
            {
                // Logger.GetInstance().Fatal(ex.Message);
                MsgBox1.alert("Error : An unexpected error has occured! " + " AllCheckedChanged : " + ex.Message);
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
             bool isSelected = false;
            int iQty = 0;

            foreach (GridViewRow row in grdDN.Rows)
            {
                CheckBox chkBox = (CheckBox)row.Cells[0].FindControl("chkItem");
                if (chkBox.Checked == true)
                {
                    iQty++;
                    isSelected = true;
                }
            }

            if (!isSelected)
            {
                MsgBox1.alert("Cannot proceed. Please check RFID tag to delete.");
                ModalPopupDelete.Show();

            }
            else
            {
                MsgBox1.confirm("Are you sure you want to delete this record?", "deleteConfirm2");
 
            }
        }

        private void DeleteRFIDTag()
        {
            foreach (GridViewRow row in grdDN.Rows)
            {
                CheckBox chkBox = (CheckBox)row.Cells[0].FindControl("chkItem");
                if (chkBox.Checked == true)
                {
                    string strDNNew = row.Cells[1].Text;
                    string strDNRFID = row.Cells[2].Text;
                    //UPDATE delete flag of DN
                    DeleteDNDAL.UPDATE_DELETEFLAG(strDNNew, strDNRFID);
                    //MsgBox1.alert("RFID Tag/s successfully deleted!");
                    Response.Write("<script>");
                    Response.Write("alert('RFID Tag/s successfully deleted!');");
                    //Response.Write("window.location = '../Login.aspx';");
                    Response.Write("</script>");
                    //Request["DNno"] = strDNNew;
                    HiddenField2.Value = row.Cells[1].Text;
                }
             
            }

            //string sUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            ////Get your querystring value here
            //string sCurrentValue = Request.QueryString["DNno"];
            ////Modify your querystring value
            //string newvalue = HiddenField2.Value;
            ////Repalce old value with new value
            //sUrl = sUrl.Replace("DNno=" + sCurrentValue, "DNno=" + newvalue);
            
         //   Response.Redirect(sUrl);
            Response.Redirect("~/Form/DNReceivingDetailsScreen.aspx?DNNo=" + HiddenField2.Value);
            Fill_DN_HeaderDetails();
        }

        private void Fill_DN_HeaderDetails2()
        {
            try
            {
                DataView dv = new DataView();
                dv = drDal.DN_GetDN_Header(HiddenField2.Value);

                if (dv.Count > 0)
                {
                    if (dv.Table.Rows.Count > 0)
                    {
                        lblDNNo.Text = dv.Table.Rows[0]["barcode"].ToString().Trim().ToUpper();
                        lblInvoiceNo.Text = dv.Table.Rows[0]["invoice"].ToString().Trim().ToUpper();
                        lblTotalRFIDCount.Text = dv.Table.Rows[0]["rfidqty"].ToString().Trim().ToUpper() + "/" +
                                                dv.Table.Rows[0]["rfidtotal"].ToString().Trim().ToUpper();

                        iRfidQty = Convert.ToInt32(dv.Table.Rows[0]["rfidqty"].ToString().Trim().ToUpper());
                        iRfidTotal = Convert.ToInt32(dv.Table.Rows[0]["rfidtotal"].ToString().Trim().ToUpper());

                        if (dv.Table.Rows[0]["DNRECEIVESTATUSID"].ToString().Trim().ToUpper() == "2")
                        {
                            lblConfirmedBy.Text = "CONFIRMED BY : " + dv.Table.Rows[0]["CONFIRMEDBY"].ToString().Trim().ToUpper();
                            lblConfirmedBy.Visible = true;
                            lnkbtnConfirmDelivery.Visible = false;
                            lnlbtnChangeDN.Visible = false;
                            lblImageChangeDN.Visible = false;
                            lblImageConfirm.Visible = false;
                        }
                        else if (dv.Table.Rows[0]["DNRECEIVESTATUSID"].ToString().Trim().ToUpper() == "3" ||
                            dv.Table.Rows[0]["DNRECEIVESTATUSID"].ToString().Trim().ToUpper() == "4") // STATUS IS DELETED (3) OR CHANGE DN (4)
                        {
                            lblImageChangeDN.Visible = false;
                            lnlbtnChangeDN.Visible = false;
                            lblImageConfirm.Visible = false;
                            lnkbtnConfirmDelivery.Visible = false;
                        }
                        else
                        {
                            lblConfirmedBy.Visible = false;
                            lnkbtnConfirmDelivery.Visible = true;
                            lnlbtnChangeDN.Visible = true;
                            lblImageChangeDN.Visible = true;
                            lblImageConfirm.Visible = true;
                        }

                        //if (iRfidQty == iRfidTotal)   
                        //{
                        //    lnkbtnConfirmDelivery.Visible = true;
                        //    lnlbtnChangeDN.Visible = false;
                        //    lblImageChangeDN.Visible = false;
                        //    lblImageConfirm.Visible = true;
                        //}
                        //else
                        //{
                        //    lnkbtnConfirmDelivery.Visible = false;
                        //    lnlbtnChangeDN.Visible = true;
                        //    lblImageChangeDN.Visible = true;
                        //    lblImageConfirm.Visible = false;
                        //}

                        Fill_DN();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Fatal(ex.StackTrace, ex);
                MsgBox1.alert(ex.Message);
            }
        }

        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/DNReceivingDetailsScreen.aspx?DNNo=" + HiddenField2.Value);
            Fill_DN_HeaderDetails();
        }

        // void objGV_DataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        GridView grid = sender as GridView;
        //        for (int i = 0; i < grid.HeaderRow.Cells.Count;i++ )
        //        {
        //            if (grid.HeaderRow.Cells[i].Text.ToString().Trim().ToUpper() == "STATUS")
        //            {
        //                if (e.Row.Cells[i].Text.ToString().Trim().ToUpper() == "RECEIVED")
        //                {
        //                    e.Row.BackColor = Color.FromArgb(204, 255, 204);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().Fatal(ex.StackTrace, ex);
        //        MsgBox1.alert(ex.Message);
        //    }
        //}
    }
}
