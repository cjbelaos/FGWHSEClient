<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="EmptyBoxMonitoringScreen.aspx.cs" Inherits="FGWHSEClient.Form.EmptyBoxMonitoringScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td>
            <asp:ImageButton ID="imgBack" runat="server" ImageUrl ="~/Image/Back.png" />
        </td>
        <td>
            <asp:LinkButton ID="lnkBack" runat="server" Font-Underline="false" ForeColor ="#3399FF" OnClientClick="javascript:window.close();">Back To Monitoring Screen</asp:LinkButton>
        </td>
    </tr>
</table>
    <br />
    <div style="margin-left:15px">
        <table>
            <tr style="display:none">
                <td style="font-size:large">CONTROL NO. :</td>
                <td>
                    <asp:Label ID="lblCONTROLNO" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLOADINGSTATUS" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblSTARTRECEIVEFLAG" runat="server" Text="" Font-Size="Large"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="font-size:large">TRACKING NO. :</td>
                <td>
                    <asp:Label ID="lblTrackingNo" runat="server" Text="" Font-Size="Large"></asp:Label>
                </td>
            </tr>

            <tr style="font-weight:100">
                <td>RFID Tag Count :</td>
                <td>
                    <asp:Label ID="lblRFIDTagCount" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <tr style="font-weight:100; color:red">
                <td>Additional RFID :</td>
                <td>
                    <asp:Label ID="lblAdditional" runat="server" Text=""></asp:Label>
                </td>
            </tr>


            <tr>
                <td>Total Empty Box Count :</td>
                <td>
                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                </td>
            </tr>

        </table>

        <br />

        <div style="height:320px;overflow:auto">
            <asp:GridView ID="gvLoad" runat="server" Width="1150px" AutoGenerateColumns="False" CellPadding="3" BackColor="White" Font-Size="Small" GridLines="Both" OnRowDataBound="gvLoad_RowDataBound"   >
                 <RowStyle ForeColor="Black" BorderColor="black"  BorderWidth="1pt" />
                 <Columns>
                     <asp:TemplateField HeaderText="NO" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="14px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="11px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblNO" runat="server" Text='<%# Eval("NO") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>


                     <asp:TemplateField HeaderText="SUPPLIER" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="14px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="11px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblSUPPLIERNAME" runat="server" Text='<%# Eval("SUPPLIERNAME") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>


                     <asp:TemplateField HeaderText="RFIDTAG" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="14px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="11px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblRFIDTAG" runat="server" Text='<%# Eval("RFIDTAG") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>


                     <asp:TemplateField HeaderText="EPC DATA" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="14px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="11px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblEPC" runat="server" Text='<%# Eval("EPC") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>


                     <asp:TemplateField HeaderText="STATUS" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="14px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="11px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblMixed" runat="server" Text='<%# Eval("MIXEDSTATUS") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>

                     <asp:TemplateField HeaderText="--" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="15px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="15px" /> 
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkDELETE"   runat="server" Text="DELETE" ForeColor="MediumBlue"></asp:LinkButton>
                              
                             </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
               </asp:GridView>

        </div>
        <div>
            <br />
            <asp:Button ID="btnADDITIONAL" runat="server" Text="ADDITIONAL" Width ="123px"/>
            <asp:Button ID="btnPRINT" runat="server" Text="PRINT" Width ="123px"/>

        </div>
    </div>

<div class="modal fade" id="myModaL" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div style="margin-left:80px">
        <span>EMPTY BOX DELETION APPROVAL</span>
        <br />
        <br />
        <table>
            <tr>
                <td>USER ID:</td>
                <td><asp:TextBox ID="txtUserID" onkeyup="onEnterUserID(event)" runat="server"></asp:TextBox></td>
            </tr>

            <tr>
                <td>PASSWORD:</td>
                <td><asp:TextBox ID="txtPassword" onkeyup="onEnterPassword(event)" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnOKlogin" runat="server" Text="OK"  Width="70" OnClick="btnOKlogin_Click"/>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancellogin" runat="server" Text="CANCEL"  Width="70"/>
                </td>
            </tr>

        </table>
  </div>
</div>

<div class="modal fade" id="myModaLAdditional" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div style="margin-left:30px">
        <br />
        <table>
            <tr>
                <td valign="top">QUANTITY :</td>
                <td><asp:TextBox ID="txtQuantity" runat="server" TextMode="Number" onfocus="this.select()"></asp:TextBox></td>
                <td style ="width:20px"></td>
                <td valign="top"><asp:Button ID="btnSave" runat="server" Text="SAVE"  Width ="100px" OnClick="btnSave_Click"/></td>
            </tr>
            <tr>
                <td valign="top">REMARKS :</td>
                <td><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                <td></td>
                <td valign="top"><asp:Button ID="btnCancel" runat="server" Text="CANCEL" Width ="100px" /></td>
            </tr>
        </table>
    </div>
</div>

<div class="modal fade" id="myModaLPrint" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div style="margin-left:30px">
        <asp:Label ID="lblQ" runat="server" Text="Are you sure you want to print?"></asp:Label>
        <table>
            <tr>
                <td><asp:Button ID="btnYes" runat="server" Text="YES" OnClick="btnYes_Click"  Width="70" /></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td><asp:Button ID="btnNo" runat="server" Text="NO"  Width="70" /></td>
            </tr>
        </table>
    </div>
</div>

<div style="display:none">
    <button type="button" class="btn nk-indigo btn-info" data-toggle="modal" data-target="#myModaL" id="btnModal">Indigo</button>
    <button type="button" class="btn nk-indigo btn-info" data-toggle="modal" data-target="#myModaLAdditional" id="btnModalAdditional">Indigo</button>
    <button type="button" class="btn nk-indigo btn-info" data-toggle="modal" data-target="#myModaLPrint" id="btnModalPrint">Indigo</button>
    <button type="button" class="btn btn-default" data-dismiss="modal" id="btnDismissModal">CANCEL</button>
    <asp:TextBox ID="txtEPC" runat="server"></asp:TextBox>
  
    

</div>
 <script type="text/javascript">

     

     function onEnterUserID(e) {
         if (e.keyCode == 13) {
             useridvalue = document.getElementById('ctl00_ContentPlaceHolder1_txtUserID').value;
             if (useridvalue != "") {
                 document.getElementById('ctl00_ContentPlaceHolder1_txtPassword').focus();
             }

         }
     }

     function onEnterPassword(e) {
         if (e.keyCode == 13) {
             passwordvalue = document.getElementById('ctl00_ContentPlaceHolder1_txtPassword').value;
             if (passwordvalue != "") {
                 document.getElementById('ctl00_ContentPlaceHolder1_btnOKlogin').click();
             }

         }
     }

     function showModal(epc) {

         document.getElementById('ctl00_ContentPlaceHolder1_txtEPC').value = epc;
         document.getElementById('btnModal').click();
         document.getElementById('ctl00_ContentPlaceHolder1_txtUserID').focus();
     }

     function showModalPrint() {
         document.getElementById('btnModalPrint').click();
     }


     function showModalAdditional() {
         document.getElementById('btnModalAdditional').click();
         document.getElementById('ctl00_ContentPlaceHolder1_txtQuantity').focus();
     }

     function dismissModal() {
            document.getElementById('btnDismissModal').click();
        }

 </script>
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>