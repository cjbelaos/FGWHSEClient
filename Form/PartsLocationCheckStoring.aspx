<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PartsLocationCheckStoring.aspx.cs" Inherits="FGWHSEClient.Form.PartsLocationCheckStoring" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
 window.history.forward(1);
 </script> 


    <div style =" left:40px;top:110px; font-weight:600;">
    <center><asp:Label ID="lblHeader" runat="server" style="font-size:18px">PARTS LOCATION CHECK STORING</asp:Label>
      </center>
        <asp:Panel id="pnlPassed" runat="server" style="display:block" >
      <table align="left" width="470px" height="260px" bordercolordark="#006666" frame="border" cellpadding="0px" cellspacing="0px" style='border-color:black;border-width:thin; border-style:solid;'>
            <tr align="center" style="background-color:Silver">
                 <td align="center" height="15PX"> 
                  <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="#2F4F4F"
                        Text="PARTCODE" Width="400PX"></asp:Label>
                   <br />
                    <div id="divPartcode" style=" border:1; background-color:Silver ; border-style:groove; width:360px; height:30px" runat="server" >
                             <asp:Label ID="lblPartCode" runat="server" Font-Names="Calibri" Font-Size="18pt" Font-Bold="true" ForeColor="Black"></asp:Label>
                     </div> 
                   
                   
                  <%-- <asp:TextBox ID="txtPartCode" Height="35px"  style="text-align: center"
                            Width="290px" Font-Bold="True" Font-Size="18pt" runat="server" 
                            MaxLength="15" AutoPostBack="True" ontextchanged="txtPartCode_TextChanged" 
                            BorderStyle="Solid" BorderWidth="1px"  ></asp:TextBox>--%>
                 </td>
            </tr>
            
            <tr>
                <td>
                    <center>
                                     <center>
                                     
                                     <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="#2F4F4F" Text="LOCATION ID" ></asp:Label>
                                     <br />
                                     <asp:TextBox ID="txtLocationID" Height="35px"  style="text-align: center"
                                        Width="360px" Font-Bold="True" Font-Size="18pt" runat="server" 
                                             AutoPostBack="True" BorderStyle="Solid" BorderWidth="1px" 
                                             ontextchanged="txtLocationID_TextChanged"  ></asp:TextBox>
                                        
                                        <br />
                                      <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="#2F4F4F" Text="LOT REF NO" ></asp:Label>
                                     <br />
                                     <asp:TextBox ID="txtLotNo" Height="35px"  style="text-align: center" Width="360px" 
                                             Font-Bold="True" Font-Size="18pt" runat="server"  AutoPostBack="True"  
                                             BorderStyle="Solid" BorderWidth="1px" ontextchanged="txtLotNo_TextChanged"  ></asp:TextBox>

                                     </center>
                        

                                        <table>
                                            <tr>
                                                <td>
                                                  <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="14pt"  ForeColor="#2F4F4F" Text="MESSAGE" ></asp:Label>
                                                </td>
                                            </tr>
                                        </table>   
                                        
                                      <div  id="partname" style=" border:1; background-color:InfoBackground ; border-style:groove; width:360px; height:70px">
                                          <asp:Label ID="lblMessage" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="black" ></asp:Label>
                                      </div>          
                                        
                                        
                        <asp:Button ID="btnBack" runat="server" Text="BACK" Width="172px" Height="50px" 
                                         BackColor="Silver" onclick="btnBack_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="CLEAR" Width="172px" Height="50px" 
                                         BackColor="#ff8080" onclick="btnClear_Click" />

                    </center>
                    
                </td>
            </tr>  
            <tr>
                <td>
                    <cc1:msgBox ID="msgBox" runat="server" /></cc1:msgbox>
                    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
                    <asp:HiddenField ID="hiddenEKANBAN" runat="server" />  
                    <asp:HiddenField ID="hiddenLotID" runat="server" />  
                </td>
            </tr>
        </table>
        </asp:Panel>


    
<%--Kempee Renz--%>

     <asp:Panel ID="pnlGetTrans" runat="server" Style="display: 1" CssClass="pnlLEADERSAPPROVAL">
        <table style="color: #000000; font-size: small; background-color: #FFFFFF; clip: rect(auto, auto, auto, auto);" class="auto-style10">
            <tr>
                
                <td  colspan="2" align="Center" style="word-spacing: normal; letter-spacing: normal">
<asp:Label ID="Label5" runat="server" Text="LEADERS APPROVAL" Width="198px"></asp:Label>

                </td>
                
              


             
                <%--<td align="left">APPROVAL</td>--%>   
              <%--  <td></td>--%>
            </tr>
            <tr>
               
                <td class="auto-style4" >
                    <asp:Label ID="Label6" runat="server" Text="USERNAME:" STYLE="margin-left: 20px"></asp:Label>

                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtApprover" runat="server" type="text" width="150px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                
                <td class="auto-style7">
                    <asp:Label ID="Label7" runat="server" Text="PASSWORD:" STYLE="margin-left: 20px"></asp:Label>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="txtPass" runat="server" type="password" Text=""  width="150px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td align="Center" colspan="2" class="auto-style8">
                    <asp:Label ID="ErrorMessageAccount" runat="server" Font-Names="Calibri" Font-Size="12pt" ForeColor="Red" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                
                <td class="auto-style6"></td>
                <%--<td class="auto-style1">modal</td>--%>
                <td class="auto-style9" >
                    
                    <asp:Button ID="btnConfirm" runat="server" Text="CONFIRM" Width="150px" Height="32px" BackColor="Silver" OnClick="btnConfirm_Click" />
                </td>
                
                
            </tr>
            <%--<tr>
                <td width="70px"></td>
                <td align="right">ISSUE
                </td>
                <td></td>
                <td></td>
                
            </tr>
            <tr>
                <td width="70px"></td>
                <td>
                    <asp:CheckBox ID="chkLacking" runat="server" Text="LACKING" Enabled="False" />
                </td>
                <td>Lacking Qty:
                </td>
                <td>
                    <asp:Label ID="lblLackingQty" runat="server"></asp:Label>
                    <%--<asp:TextBox  Enabled="False"></asp:TextBox>
                </td>
                <td></td>
            </tr>

            <tr>
                <td width="70px"></td>
                <td>
                    <asp:CheckBox ID="chkExcess" runat="server" Text="Excess" Enabled="False"/>
                </td>
                <td>Excess Qty:
                </td>
                <td>
                    <asp:Label ID="lblExcessQty" runat="server"></asp:Label>
                    <%--<asp:TextBox ID="txtExcessQty" runat="server" Enabled="False"></asp:TextBox>--%>
              <%--  </td>
            </tr>--%>

        </table>
        <br />
     <%-- <asp:Button ID="btnProceed" runat="server" Text="PROCEED" Width="105px" Height="32px" BackColor="silver" 
         
        Class="pnlBypassbtn" OnClick="btnProceed_Click" Enabled="False"/>--%>
        
    </asp:Panel>
</div>

  <asp:Button ID="btnShowPopup" runat="server" Style="display: none"
       OnClick="btnShowPopup_Click" />


    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
        TargetControlID="btnShowPopup"
        PopupControlID="pnlGetTrans"
        CancelControlID=""
        BackgroundCssClass="watermarked"
        BehaviorID="ModalPopupExtender2"
        Drag="true">
    </ajaxToolkit:ModalPopupExtender>




</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <link type="text/css" rel="Stylesheet" href="../App_Themes/Stylesheet/Main.css" />
    <style type="text/css">
        .auto-style3 {
            height: 22px;
            width: 56px;
        }
        .auto-style4 {
            height: 22px;
            width: 32px;
        }
        .auto-style6 {
            height: 49px;
            width: 32px;
        }
        .auto-style7 {
            height: 28px;
        }
        .auto-style8 {
            height: 22px;
        }
        .auto-style9 {
            height: 49px;
        }
        .auto-style10 {
            height: 161px;
            width: 300px;
        }
    </style>
    </asp:Content>





<%--Kempee Renz--%>


 