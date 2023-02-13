<%@ Page Title="" Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="HTDefault.aspx.cs" Inherits="FGWHSEClient.Form.HTDefault" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
&nbsp;&nbsp;MAIN PAGE
<br /><br />
      
    <asp:Panel ID="pnlLogin" runat="server" align="center" Width="480px">
            <table id="Table1" cellspacing="1" cellpadding="1" width="480" border="0" style="width: 420px; height: 292px;" align="center">
                <tr>
                    <td background="../Image/About.png">
                        <table class="about" align="center">
                            <tr>
                                <td><asp:Label runat="server" ID="lblSystemName"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="lblVersion"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="lblPublishDate"></asp:Label><asp:Label runat="server" ID="lblUpdateDate"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="lblRemarks"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Information Systems Department</td>
                            </tr>
                            <tr>
                                <td ><br /></td>
                            </tr>
                            <tr>
                                <td>For feedback, questions or accessibility issues:</td>
                            </tr>
                            <tr>
                                <td>Please call local 2312</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
 
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>

</asp:Content>

