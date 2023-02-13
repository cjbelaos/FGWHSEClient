<%@ Page Title="" Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="HTaspDelivery.aspx.cs" Inherits="FGWHSEClient.Form.HTaspDelivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblHeader" runat="server" Font-Size="Small" ForeColor="Black">Delivery Scan</asp:Label>
    <div>
        <br />
        <center>
             <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
            <table runat="server" width="348px">

                <tr>
                    <td class="auto-style2">
                        <label style="font-size: small; color: #000000; font-weight: normal;">DS Ref No: </label>
                    </td>
                    <td style="text-align:left">
                        <asp:TextBox ID="txtbxDSRefNo" runat="server" OnTextChanged="txtbxDSRefNo_TextChanged" ></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="Button" style="display:none" />
                    </td>
                </tr>
               
                <tr>
                    <td colspan="2" class="auto-style3"></td>
                </tr>
                <tr>
                    <td colspan="2" class="auto-style3">
                        <label style="font-size: small; font-weight: normal; color: #000000">
                        List of Serials Scanned</label> </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="height:270px;overflow:auto">
                            <asp:GridView ID="gvDeliver" runat="server" AutoGenerateColumns="false"
                            BackColor="White" Font-Bold="False" ForeColor="Black" Width="325px">
                            <Columns>

                                <asp:TemplateField HeaderText="DS" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDS" runat="server"  OnClick ="lnk_Click"  Text='<%# Eval("DSREFNO") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="Small" BackColor="White"  />
                                            <HeaderStyle Font-Size="Small" ForeColor="White" HorizontalAlign="center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Serial No" DataField="SerialNo" HeaderStyle-Font-Size="Small" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="center">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="Small" BackColor="White" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Partcode" DataField="Partcode" HeaderStyle-Font-Size="Small" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="center">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="Small" BackColor="White" />
                                </asp:BoundField>



                            </Columns>
                            <HeaderStyle BackColor="#184B8A" ForeColor="White" />
                        </asp:GridView>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="padding: 0px; margin: 0px" align="center" class="auto-style2"></td>
                </tr>
                <tr>
                    <td class="auto-style2" align="right">
                        <asp:Button ID="btnDeliver" runat="server" Text="Deliver" OnClick="btnDeliver_Click" /></td>
                    <td class="auto-style1">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"/></td>
                </tr>

            </table>
                 </asp:Panel>

        </center>
        <br />
    </div>


    <div>
        <asp:GridView ID="grdHidden" runat="server" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="DS" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDS" runat="server" Text='<%# Eval("DSREFNO") %>'></asp:Label>
                                            </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SERIAL" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerial" runat="server" Text='<%# Eval("DSREFNO") %>'></asp:Label>
                                            </ItemTemplate>
                                </asp:TemplateField>

                                



                            </Columns>
                            <HeaderStyle BackColor="#184B8A" ForeColor="White" />
                        </asp:GridView>

    </div>
    <cc1:msgBox ID="MsgBox1" runat="server"></cc1:msgBox>
</asp:Content>

