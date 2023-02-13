<%@ Page Language="C#"  MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="EmptyBox.aspx.cs" Inherits="FGWHSEClient.Form.EmptyBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="text-align:left;width:1200px">
    <div style="margin-left:10px">
        <br />
            <span>RETURNABLES MONITORING</span>
        <br />
        <br />
        <div style="position:absolute;top:100px;left:1120px">
            <asp:Button ID="btnStart" runat="server" Text="START" Width="100px" Height ="50px" OnClick="btnStart_Click" />
        </div>
        <div>
            <table>
                <tr>
                    <td valign ="top">CONTROL NO.:</td>
                    <td><asp:TextBox ID="txtCONTROLNO" runat="server" Enabled="False">[AUTOMATIC]</asp:TextBox></td>
                    <td style="width:600px">&nbsp;</td>
                    <td>
                        <asp:Label ID="lblAntennaCaption" runat="server" Text="Loading Dock"></asp:Label> :</td>
                    <td>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddLoadingDock" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddLoadingDock_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign ="top">Driver's ID :</td>
                    <td><asp:TextBox ID="txtDriverID" runat="server" AutoPostBack="True" OnTextChanged="txtDriverID_TextChanged"></asp:TextBox></td>
                    <td style="display:none"><asp:Label ID="lblAbbreviation" runat="server" Text=""></asp:Label></td>
                </tr>

                <tr>
                    <td valign ="top">Driver's Name :</td>
                    <td><asp:TextBox ID="txtDriverName" runat="server" Enabled="False"></asp:TextBox></td>
                </tr>

                <tr>
                    <td valign ="top">TRACKING NO :</td>
                    <td><asp:TextBox ID="txtTRACKINGNO" runat="server" AutoPostBack="True" OnTextChanged="txtDriverID_TextChanged"></asp:TextBox></td>
                    
                </tr>
                <tr>
                    <td valign ="top">Supplier Name :</td>
                    <td><asp:TextBox ID="txtSupplier" runat="server" Enabled="False"></asp:TextBox></td>
                    <td style="display:none"><asp:Label ID="lblSupplierID" runat="server" Text=""></asp:Label></td>
                </tr>
                

                <tr>
                    <td valign ="top">Plate No :</td>
                    <td><asp:TextBox ID="txtPlateNo" runat="server"></asp:TextBox></td>
                </tr>
            </table>
            <br />
            <table style="font-size:x-small; margin-left:300px">
                <tr id="trLegend" runat="server"></tr>
                </table>

            <br />

            <div>
                <asp:Panel ID="pnlRefresh" runat="server" ForeColor ="Red">
                Last updated: <asp:Label ID="lblLastUpdateDate" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <br />
                <asp:GridView ID="gvLoad" runat="server" Width="1150px" AutoGenerateColumns="False" CellPadding="3" BackColor="White" Font-Size="Small" GridLines="Both" OnRowDataBound="gvLoad_RowDataBound"  >
                 <RowStyle ForeColor="Black" BorderColor="black"  BorderWidth="1pt" />
                 <Columns>
                      <asp:TemplateField HeaderText="TRACKING NO." >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="15px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="15px" /> 
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkTRACKINGNO"  OnClick ="lnk_Click"  runat="server" CommandArgument='<%#Bind("TRACKINGNO") %>' Text='<%# Eval("TRACKINGNO") %>' ForeColor="MediumBlue"></asp:LinkButton>
                              
                             </ItemTemplate>
                      </asp:TemplateField>


                     <asp:TemplateField HeaderText=".." >
                             <HeaderStyle CssClass="NoDisplay"/>
                             <ItemStyle CssClass="NoDisplay"/> 
                             <ItemTemplate>
                                  <asp:Label ID="lblCONTROLNO" runat="server" Text='<%# Eval("CONTROLNO") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>

                     <asp:TemplateField HeaderText="SUPPLIER" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="15px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="15px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SUPPLIERNAME") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>


                     <asp:TemplateField HeaderText="P-CASE COUNT" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="15px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="15px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblPCASECOUNT" runat="server" Text='<%# Eval("PCASECOUNT") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>

                     <asp:TemplateField HeaderText="TIME IN" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="15px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="15px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblTIMEIN" runat="server" Text='<%# Eval("TIMEIN") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>



                     <asp:TemplateField HeaderText="TIME OUT" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="15px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="15px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblTIMEOUT" runat="server" Text='<%# Eval("TIMEOUT") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>


                      <asp:TemplateField HeaderText="STATUS" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="15px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="15px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblEMPTYPCASESTATUS" runat="server" Text='<%# Eval("EMPTYPCASESTATUS") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>


                     <asp:TemplateField HeaderText=".." >
                             <HeaderStyle CssClass="NoDisplay"/>
                             <ItemStyle CssClass="NoDisplay"/> 
                             <ItemTemplate>
                                  <asp:Label ID="lblCOLORDISPLAY" runat="server" Text='<%# Eval("COLORDISPLAY") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
               </asp:GridView>

            </div>

        </div>
    </div>
</div>


<div style="display:none">
    <asp:TextBox ID="txtButtonValue" runat="server"></asp:TextBox>
    <asp:Button ID="btnDisplay" runat="server" Text="Button" OnClick="btnDisplay_Click" />

</div>
 <script type="text/javascript">

     var TIMER_INTERVAL = 10000;
      setTimeout(function() {refreshPage()}, TIMER_INTERVAL);
     function refreshPage() {
         var displayButton = document.getElementById("ctl00_ContentPlaceHolder1_btnDisplay");
         var check = document.getElementById("ctl00_ContentPlaceHolder1_txtButtonValue").value;
         var pnlRefresh = document.getElementById("ctl00_ContentPlaceHolder1_pnlRefresh");
         //var isVisible = pnlRefresh.attributes.add("display","none")
         //pnlRefresh.visible = false;
         if (check == "STOP") {
             //var isVisible = pnlRefresh.attributes.add("display","compact")
             //pnlRefresh.visible = true;
             displayButton.click();
         }
        
       setTimeout(function() {refreshPage()}, TIMER_INTERVAL);
     }


 </script>
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>