<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master"  AutoEventWireup="true" CodeBehind="ASPoutsideWH.aspx.cs" Inherits="FGWHSEClient.Form.ASPoutsideWH" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style =" margin-left:40px;top:110px;">
       <br />
       <font style="font-size:17px; color:Black; font-weight:bold">RFID-Lot Inquiry </font>
       <br />
       <br />
        <div>
            <table>
                <tr>
                    <td>ITEMCODE:</td>
                    <td><asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td>SERIAL NO:</td>
                    <td><asp:TextBox ID="txtSerialNo" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>DESCRIPTION:</td>
                    <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td>FROM DATE:</td>
                    <td><asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td  valign="top"><asp:Button ID="btnSearch" runat="server" Text="SEARCH" Width="92px" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                    </td>
                    <td></td>
                    <td>TO DATE:</td>
                    <td><asp:TextBox ID="txtToDate" runat="server"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td valign="top"><asp:Button ID="btnDownload" runat="server" Text="DOWNLOAD" Width="97px" /></td>
                </tr>
            </table>
        </div>
        <div>
            <br />

            <asp:GridView ID="grdASP" runat="server" AutoGenerateColumns ="false" AllowPaging ="true" PageSize ="12">
            
            <Columns>
               

                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="ITEM NO"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblITEMNO" runat="server" Text='<%#Bind("ITEMNO") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="ITEM CODE"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblITEMCODE" runat="server" Text='<%#Bind("ITEMCODE") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="ITEM CODE"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblITEMCODE" runat="server" Text='<%#Bind("ITEMCODE") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="ITEM DESCRIPTION"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblITEMDESCRIPTION" runat="server" Text='<%#Bind("ITEMDESCRIPTION") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="SERIAL"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblSERIAL" runat="server" Text='<%#Bind("SERIAL") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="PRODUCTIONDATE"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblPRODUCTIONDATE" runat="server" Text='<%#Bind("PRODUCTIONDATE") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="QUANTITY"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblQUANTITY" runat="server" Text='<%#Bind("QUANTITY") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="TRANSFERDATE"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblTRANSFERDATE" runat="server" Text='<%#Bind("TRANSFERDATE") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="DELIVERYSLIP"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDELIVERYSLIP" runat="server" Text='<%#Bind("DELIVERYSLIP") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                

            </Columns>
            <HeaderStyle  Font-Bold="True"  BackColor = "#C5D9F1" BorderStyle ="Solid" BorderColor ="Black" />
            <RowStyle Font-Bold ="false" BorderStyle ="Solid" BorderColor ="Black" BorderWidth = "10px" />
            </asp:GridView>

        </div>

        <br />
    </div>
</asp:Content>