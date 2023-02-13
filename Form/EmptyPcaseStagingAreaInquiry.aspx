<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="EmptyPcaseStagingAreaInquiry.aspx.cs" Inherits="FGWHSEClient.Form.EmptyPcaseStagingAreaInquiry" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Style.css" rel="stylesheet" />
    <div style="margin-left: 15px">
       <font style="font-size:17px; color:Black; font-weight:bold">Empty Box Returnables History</font>
      <br />
       <br />
        <table>
            <tr>
                <td colspan="5">
                    <div style="vertical-align:top">
                        Date From :&nbsp;
                    <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;
                    Date To : &nbsp;<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                    </div>
                </td>
          
            </tr>
            <tr>
                <td>Supplier : &nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddSupplier" runat="server"></asp:DropDownList></td>
            </tr>

            <tr>
                <td>Antenna : &nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddAntenna" runat="server"></asp:DropDownList>
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnAutoRefresh" runat="server" Text="Auto Refresh" OnClick="btnAutoRefresh_Click"  />

                    &nbsp; 
                    <asp:Label ID="lblStat" runat="server" Text="OFF" Font-Size="Small" ForeColor="Red"></asp:Label>

                </td>
               
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="lblRefresh" runat="server" Text="" Font-Size="Medium" ForeColor="Green"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <div style="display:inline-block">
            <div class="dvBoardContainer">
                <div>
                    <table id="tbTable" runat="server">
                    </table>


                </div>

            </div>
        </div>
    </div>

   <div style="display:none">
       <asp:TextBox ID="txtClick" runat="server"></asp:TextBox>

       <asp:Button ID="btnRefresh" runat="server" Text="Button" OnClick="btnRefresh_Click" />
    </div>

    <script type="text/javascript">

        var TIMER_INTERVAL = 10000;
        setTimeout(function () { refreshPage() }, TIMER_INTERVAL);
        function refreshPage() {
            var displayButton = document.getElementById("ctl00_ContentPlaceHolder1_btnRefresh");
            var check = document.getElementById("ctl00_ContentPlaceHolder1_txtClick").value;
            if (check == "1") {
                displayButton.click();
            }

            
            setTimeout(function () { refreshPage() }, TIMER_INTERVAL);
        }
    </script>

    <ajaxToolkit:Calendarextender ID="Calendarextender1" runat="server" BehaviorID="calendar1"
    TargetControlID="txtFrom"
    Format="dd-MMM-yyyy HH:mm tt"
    PopupButtonID="txtFromDate"  >
    </ajaxToolkit:Calendarextender>

    <ajaxToolkit:Calendarextender ID="Calendarextender2" runat="server" BehaviorID="calendar2"
    TargetControlID="txtTo"
    Format="dd-MMM-yyyy HH:mm tt"
    PopupButtonID="txtToDate"  >
    </ajaxToolkit:Calendarextender>
</asp:Content>

