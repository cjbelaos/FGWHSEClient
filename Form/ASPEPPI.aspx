<%@ Page Language="C#"   MasterPageFile="~/Form/MasterPalletMonitoring.Master"  AutoEventWireup="true" CodeBehind="ASPEPPI.aspx.cs" Inherits="FGWHSEClient.Form.ASPEPPI" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <link href="../Style.css" rel="stylesheet" />
    
    <div style =" margin-left:10px;top:110px;">
       <br />
       <font style="font-size:17px; color:Black; font-weight:bold">VP ASP Traceability</font>
       <br />
       <br />
        <div>
            <table>
                <tr>
                    <td>ITEMCODE:</td>
                    <td><asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td>SERIAL NO:</td>
                    <td><asp:TextBox ID="txtSerialNo" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>DESCRIPTION:</td>
                    <td><asp:TextBox ID="txtDesc" runat="server" Enabled="False"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td>FROM DATE:</td>
                    <td><asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td  valign="top"><asp:Button ID="btnSearch" runat="server" Text="SEARCH" Width="92px" OnClick="btnSearch_Click" /></td>
                    <td >
                          &nbsp;&nbsp;&nbsp;&nbsp;
                        DATE TYPE:
                         &nbsp;&nbsp;
                    </td>
                    <td valign="top">
                      
                        <asp:DropDownList ID="ddDateType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>DS REF NO:</td>
                    <td><asp:TextBox ID="txtDS" runat="server"></asp:TextBox> </td>
                    <td></td>
                    <td>TO DATE:</td>
                    <td><asp:TextBox ID="txtToDate" runat="server"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td valign="top"><asp:Button ID="btnDownload" runat="server" Text="DOWNLOAD" Width="97px" OnClick="btnDownload_Click" /></td>
                </tr>
                
            </table>
            
        </div>
        <div>
            <br />
            <div style="width:1150px;height:280px;overflow:auto">
                <table id="tbASP" runat="server">
                    
                </table>
                <%--<asp:GridView ID="grdASP" runat="server" AutoGenerateColumns ="false" Width ="2100px">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="70px" HeaderText="ITEM NO"  ItemStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblITEMNO" runat="server" Text='<%#Bind("ITEMNO") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="STATUS"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDELIVERYSLIP" runat="server" Text='<%#Bind("ASP_STATUS") %>'></asp:Label>
                                    </ItemTemplate>


                     </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="DELIVERY SLIP"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDELIVERYSLIP" runat="server" Text='<%#Bind("DELIVERYSLIP") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="TRANSFER SLIP"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANSFERSLIP" runat="server" Text='<%#Bind("TRANSFERSLIP") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="ITEM CODE"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("ITEMCODE") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="300px" HeaderText="ITEM DESCRIPTION"  ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblITEMDESCRIPTION" runat="server" Text='<%#Bind("ITEMDESCRIPTION") %>'></asp:Label>
                                    </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="SERIAL"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSERIAL" runat="server" Text='<%#Bind("SERIAL") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="QTY"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQUANTITY" runat="server" Text='<%#Bind("QUANTITY") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="RECEIVED DATE"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRECEIVEDDATE" runat="server" Text='<%#Bind("RECEIVEDDATE") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="DELIVERY DATE"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDELIVERYDATE" runat="server" Text='<%#Bind("DELIVERYDATE") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="200px" HeaderText="DELIVERY INTERFACE DATE"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRECEIVEDDATE" runat="server" Text='<%#Bind("INTERFACEDATE") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

           


                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="OD NO"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblODNO" runat="server" Text='<%#Bind("ODNO") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="INVOICE"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINVOICE" runat="server" Text='<%#Bind("INVOICE") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="SHIPMENTDATE"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSHIPMENTDATE" runat="server" Text='<%#Bind("SHIPMENTDATE") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="200px" HeaderText="SHIPMENT INTERFACE DATE"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSHIPMENTIFDATE" runat="server" Text='<%#Bind("SHIPMENTIFDATE") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    

                

                </Columns>
                <HeaderStyle Font-Size="Small" HorizontalAlign="center" VerticalAlign="Middle" Font-Bold="True"  BackColor = "#C5D9F1" BorderStyle ="Solid" BorderColor ="Black" />
                <RowStyle Font-Size="Small" HorizontalAlign="center" VerticalAlign="Middle" Font-Bold ="false" BorderStyle ="Solid" BorderColor ="Black" BorderWidth = "10px" />
                  
                
                </asp:GridView>--%>
            </div>
        </div>

        <br />
    </div>


    <ajaxToolkit:Calendarextender ID="Calendarextender1" runat="server" BehaviorID="calendar1"
    TargetControlID="txtFromDate"
    Format="dd-MMM-yyyy HH:mm tt"
    PopupButtonID="txtFromDate"  >
    </ajaxToolkit:Calendarextender>

    <ajaxToolkit:Calendarextender ID="Calendarextender2" runat="server" BehaviorID="calendar2"
    TargetControlID="txtToDate"
    Format="dd-MMM-yyyy HH:mm tt"
    PopupButtonID="txtToDate"  >
    </ajaxToolkit:Calendarextender>

    <cc1:msgBox ID="msgBox" runat="server" />
</asp:Content>