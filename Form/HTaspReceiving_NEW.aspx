<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="HTaspReceiving_NEW.aspx.cs" Inherits="FGWHSEClient.Form.HTaspReceiving_NEW" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblHeader" runat="server" Font-Size="Small" ForeColor="Black">Receiving Serial Scan</asp:Label>
    <div>
        <br />
        <center>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
            
            <table runat="server" width="348px" >

                <tr>
                    <td class="auto-style5">
                        <label style="font-size: small; color: #000000; font-weight: normal;">DS Ref No: </label>
                        <asp:Button ID="Button1" runat="server" Text="Button" style="display:none" />
                    </td>
                    
                    <td>
                        <asp:TextBox ID="txtbxRefNo" runat="server" OnTextChanged="txtbxRefNo_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td class="auto-style5">
                        <label style="font-size: small; font-weight: normal; color: #000000">Serial No:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxSerialNo" runat="server" OnTextChanged="txtbxSerialNo_TextChanged" Enabled="False"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style5">
                       
                    </td>
                    <td style="font-size:small">
                          <asp:Label ID="lblScannedSerialQty" runat="server" Text="0"></asp:Label>
                          /<asp:Label ID="lblDSSerialQty" runat="server" Text="0"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="auto-style3"></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <label style="font-size: small; font-weight: normal; color: #000000">List of Serials Scanned</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="height:270px;overflow:auto">
                                <asp:GridView ID="gvSerialScanned" runat="server" AutoGenerateColumns="false"
                                    BackColor="White" Font-Bold="False" ForeColor="Black" Width="325px">
                                    <Columns>
                                         <asp:TemplateField HeaderText="Serial No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNo" runat="server" Text='<%#Bind("SerialNo") %>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle Font-Size="Small"  ForeColor="White" HorizontalAlign="Center" />
                                             <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="Small" BackColor="White" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Scanned Serial">
                                            <ItemTemplate>
                                                <asp:Label ID="lblScannedSerial" runat="server" Text='<%#Bind("ScannedSerial") %>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle Font-Size="Small"  ForeColor="White" HorizontalAlign="Center" />
                                             <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="Small" BackColor="White" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Serial QTY ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialQuantity" runat="server" Text='<%#Bind("SerialQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle Font-Size="Small"  ForeColor="White" HorizontalAlign="Center" />
                                             <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="Small" BackColor="White" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Scanned QTY ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblScannedQuantiy" runat="server" Text='<%#Bind("ScannedQuantiy") %>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle Font-Size="Small"  ForeColor="White" HorizontalAlign="Center" />
                                             <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="Small" BackColor="White" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Part Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartcode" runat="server" Text='<%#Bind("PartCode") %>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="DispNone" />
                                             <ItemStyle CssClass="DispNone" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle BackColor="#184B8A" ForeColor="White" />
                                </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 0px; margin: 0px" align="center" class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style5" align="right">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                    <td class="auto-style1" align="center">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"/></td>
                </tr>

            </table>
            </asp:Panel>
        </center>
        <br />
    </div>

    <cc1:msgBox ID="MsgBox1" runat="server"></cc1:msgBox>
</asp:Content>


<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <link type="text/css" rel="Stylesheet" href="../App_Themes/Stylesheet/Main.css"/>
    <style type="text/css">
        .auto-style1 {
            width: 102px;
        }
        .auto-style3 {
            width: 108px;
        }
        .auto-style4 {
            width: 129px;
        }
        .auto-style5 {
            width: 99px;
        }

        .DispNone{
            display:none;
        }
    </style>
</asp:Content>


