<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PartsLocationCheckStoringPS2.aspx.cs" Inherits="FGWHSEClient.Form.PartsLocationCheckStoringPS2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script type="text/javascript">
        window.history.forward(1);
    </script>

    <link type="text/css" rel="Stylesheet" href="Style.css" />

    <div style="left: 40px; top: 110px; font-weight: 600;">
        <center>
            <asp:Label ID="lblHeader" runat="server" Style="font-size: 18px">PARTS LOCATION CHECK STORING</asp:Label>
        </center>
        <asp:Panel ID="pnlPassed" runat="server" Style="display: block">
            <table align="left" width="470px" height="260px" bordercolordark="#006666" frame="border" cellpadding="0px" cellspacing="0px" style='border-color: black; border-width: thin; border-style: solid;'>
                <tr align="center" style="background-color: Silver">
                    <td align="center" height="15PX">
                        <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="14pt" ForeColor="#2F4F4F"
                            Text="PARTCODE:" Width="400PX"></asp:Label>
                        <br />
                        <div id="divPartcode" style="border: 1; background-color: Silver; border-style: groove; width: 360px; height: 30px" runat="server">
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
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="14pt" ForeColor="DarkSlateGray" Text="LOCATION ID:" ClientIDMode="AutoID"></asp:Label>

                                </td>
                                <td>
                                <asp:TextBox ID="txtLocationID"  Height="35px" Style="text-align: center; text-transform:uppercase" Width="350px"
                                       Font-Size="18pt" runat="server" AutoPostBack="True" OnTextChanged="txtLocationID_TextChanged" ></asp:TextBox>
                                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="14pt" ForeColor="DarkSlateGray" Text="MOTHERLOT QR:" ClientIDMode="AutoID"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMotherLot" Height="35px" Style="text-align: center" Width="350px"
                                        Font-Bold="True" Font-Size="18pt" runat="server" AutoPostBack="True" OnTextChanged="txtMotherLot_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Names="Calibri" Font-Size="14pt" ForeColor="DarkSlateGray" Text="RFID TAG:" ClientIDMode="AutoID"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRFIDTag" runat="server" Height="35px" Width="350px" Font-Names="Calibri" Font-Size="18pt" Font-Bold="True" ForeColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="#CCCCCC"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Font-Names="Calibri" Font-Size="14pt" ForeColor="#2F4F4F" Text="LOT QR:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLotRefNo" Height="35px" Style="text-align: center" Width="350px"
                                        Font-Bold="True" Font-Size="18pt" runat="server" AutoPostBack="True"
                                        BorderStyle="Solid" BorderWidth="1px" OnTextChanged="txtLotRefNo_TextChanged1"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Font-Names="Calibri" Font-Size="14pt" ForeColor="#2F4F4F" Text="LOT LABEL COUNT:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLotLabelCount" runat="server" Height="35px" Width="350px" Font-Names="Calibri" Font-Size="14pt" ForeColor="DarkSlateGray" BorderWidth="1px" BorderStyle="Solid" BackColor="#CCCCCC"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="14pt" ForeColor="#2F4F4F" Text="MESSAGE:"></asp:Label>
                                </td>
                                <td>
                                    <div id="partname" style="border: 1; background-color: InfoBackground; border-style: groove; width: 348px; height: 70px" runat="server">
                                        <asp:Label ID="lblMessage" runat="server" Font-Names="Calibri" Font-Size="12pt" ForeColor="black"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnBypass" runat="server" Text="BYPASS" Width="105px" Height="32px" BackColor="silver" OnClick="btnBypass_Click" />
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="SAVE" Width="105px" Height="32px" BackColor="Silver" OnClick="btnSave_Click" />
                                            </td>
                                            <td align="center" >
                                                  <asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="12pt"  ForeColor="#2F4F4F" Text="SCANNED QTY"></asp:Label>
                                            </td>
                                            <td align="center"  style="width:120px">
                                                        <asp:Label ID="Label10" runat="server" Font-Names="Calibri" Font-Size="12pt" ForeColor="#2F4F4F" Text="TOTAL QTY"></asp:Label>

                                            </td>

                                        </tr>
                                    </table>

                              </td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnBack" runat="server" Text="BACK" Width="105px" Height="32px" BackColor="Silver" />
                                </td>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnClear" runat="server" Text="CLEAR" Width="105px" Height="32px" BackColor="Silver" OnClick="btnClear_Click1" />
                                            </td>
                                            <td align="center" style="width:80px">
                                                <asp:Label ID="lblScanQty" runat="server" Style="text-align: center; border:2px solid"  BorderColor="Gray" Font-Names="Calibri" Font-Size="12pt" ForeColor="Black"  Width="105px"></asp:Label>
                                            </td>
                                            <td>
                                                /
                                            </td>
                                            <td align="center" >
                                                <asp:Label ID="lblTotQty"  runat="server"   Style="text-align: center; border:2px solid" BorderColor="Gray" Font-Names="Calibri" Font-Size="12pt" ForeColor="Black"  Width="105px"></asp:Label>
                                            </td>
                                        </tr>

                                    </table>
                                    

                                </td>


                            </tr>
                        </table>
                    </td>
                </tr>

            </table>
        </asp:Panel>

        <div style="display: none">
            <cc1:msgBox ID="MsgBox1" runat="server"></cc1:msgBox>
            <asp:HiddenField ID="hiddenEKANBAN" runat="server" />
            <asp:HiddenField ID="hiddenLotID" runat="server" />
        </div>

    </div>




    <asp:Panel ID="pnlGetTrans" runat="server" Style="display: none" CssClass="pnlBypass">
        <table style="color: #000000; height:200px; width:480px; font-size: small;">
            <tr>
                <td></td>
                <td width="130px" align="center">BYPASS    </td>
                <td align="left">APPROVAL</td>
                <td></td>
            </tr>
            <tr>
                <td width="70px"></td>
                <td >Username:</td>
                <td width="168px">
                    <asp:TextBox ID="txtApprover" runat="server" type="text"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td width="70px"></td>
                <td width="168px">Password:
                </td>
                <td>
                    <asp:TextBox ID="txtPass" runat="server" type="password" Text=""></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnConfirm" runat="server" Text="CONFIRM" Width="105px" Height="32px" BackColor="silver" OnClick="btnConfirm_Click" />
                </td>
                <td></td>
                
            </tr>
            <tr>
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
                    <%--<asp:TextBox  Enabled="False"></asp:TextBox>--%>
                </td>
                <td></td>
            </tr>

            <tr>
                <td width="70px"></td>
                <td>
                    <asp:CheckBox ID="chkExcess" runat="server" Text="EXCESS" Enabled="False"/>
                </td>
                <td>Excess Qty:
                </td>
                <td>
                    <asp:Label ID="lblExcessQty" runat="server"></asp:Label>
                    <%--<asp:TextBox ID="txtExcessQty" runat="server" Enabled="False"></asp:TextBox>--%>
                </td>
            </tr>

        </table>
        <br />
      <asp:Button ID="btnProceed" runat="server" Text="PROCEED" Width="105px" Height="32px" BackColor="silver" CssClass="pnlBypassbtn" OnClick="btnProceed_Click" Enabled="False"/>
        
    </asp:Panel>


    <asp:Button ID="btnShowPopup" runat="server" Style="display: NONE"
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
        .auto-style1 {
            width: 466px;
        }
    </style>
</asp:Content>

