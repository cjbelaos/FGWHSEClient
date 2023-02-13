<%@ Page Language="C#"  MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="EmptyBoxBoard.aspx.cs" Inherits="FGWHSEClient.Form.EmptyBoxBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlHidden" runat="server">
        <asp:Button ID="btnRefresh" runat="server" Text="Button" OnClick="btnRefresh_Click" />

    </asp:Panel>
    <div>
        <div style="margin-left:15px">
            <table>
                <tr valign="top">
                    <td id="tdOverAll" runat="server"></td>
                    <td id="tdTitle" runat="server" style="width:500px;text-align:center">
                        <table>
                            <tr>
                                <td><asp:Label ID="Label1" runat="server" Text="PR WHSE" ForeColor="Black" Font-Size="X-Large"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="height:auto"><asp:Label ID="Label2" Height="100%" runat="server" Text="Empty Box Board" ForeColor="Black" Font-Size="X-Large"></asp:Label> <br /><br /></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="lblUpdate" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>


                    </td>
                    <td>
                        <table  id="tbLegend" runat="server">
                            <tr>
                                <td style="color:black">LEGEND:</td>
                            </tr>

                        </table>

                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div>
                           <table id="tbDisplay" runat ="server">
                              
                           </table>

                        </div>
                        


                    </td>
                </tr>

            </table>



        </div>
    </div>
     <script type="text/javascript">

         var TIMER_INTERVAL = 10000;
         setTimeout(function () { refreshPage() }, TIMER_INTERVAL);

     function refreshPage() {
         var displayButton = document.getElementById("ctl00_ContentPlaceHolder1_btnRefresh");
     
         displayButton.click();
        
       setTimeout(function() {refreshPage()}, TIMER_INTERVAL);
     }
     </script>
    <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>