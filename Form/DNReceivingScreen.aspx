<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="DNReceivingScreen.aspx.cs" Inherits="FGWHSEClient.Form.DNReceivingScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">

    var TIMER_INTERVAL = 10000;
    setTimeout(refreshPage, TIMER_INTERVAL);




    function refreshPage() {
        //var e = document.getElementById('ctl00$ContentPlaceHolder1$ddlLoadingDock');
        //var strLoadingDock = e.options[e.selectedIndex].value;
        //window.location = "DNReceivingScreen.aspx?loadingdock=" + strLoadingDock;
        // window.location = "http://172.16.52.100/EWHS/Form/DNReceivingScreen.aspx?loadingdock=LD0101";
        var displayButton = document.getElementById("ctl00_ContentPlaceHolder1_btnDisplay");
       displayButton.click(); // this will trigger the click event
        setTimeout(refreshPage, TIMER_INTERVAL);
    }




</script>
<div style =" left:40px;top:110px;">
    <div style="float:left; margin-left:15px; font-size:13px; color:red">
<%--    <%Response.Write("Last updated: " + DateTime.Now.ToString());%>--%>

               <asp:UpdatePanel runat="server" ID="UPanel2">
           <ContentTemplate>
                Last updated: <asp:Label ID="lblLastUpdateDate" runat="server" Text=""></asp:Label>
               </ContentTemplate>
               <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btnDisplay" EventName="Click" />
               </Triggers>
           </asp:UpdatePanel>   
    </div>
    <div style="float:right; margin-right:15px; font-size:13px; color:Black">
    Loading Dock: &nbsp; <asp:DropDownList ID="ddlLoadingDock" runat="server" 
            Width="200px" onselectedindexchanged="ddlLoadingDock_SelectedIndexChanged" 
            AutoPostBack="True"></asp:DropDownList>
    </div>
    <br />
    <br />
    <div align="center" style="font-size:10px; color:Gray; ">
        <img src="../Image/whitebox.png" style="width:10px; height:10px" /> With DN Data &nbsp;&nbsp;
        <img src="../Image/yellowbox.png" style="width:10px; height:10px" /> Bypass DN Data
    </div>
    <br />
    <div style="display:none;">
        <asp:Button ID="btnDisplay" runat="server" Text="Display" 
            onclick="btnDisplay_Click" />
    </div>
 </div>
    <div align="center">
    
           <asp:UpdatePanel runat="server" ID="UPanel1">
           <ContentTemplate>
               <asp:GridView ID="gvDNReceiving" runat="server" Width="1150px" 
                AutoGenerateColumns="False" CellPadding="3" BackColor="White" Font-Size="Small" 
                 GridLines="Both" onrowdatabound="gvDNReceiving_RowDataBound" >
                 <RowStyle ForeColor="Black" BorderColor="black"  BorderWidth="1pt" />
                 <Columns>
                     <asp:BoundField DataField="DNNo" ItemStyle-BorderColor="Black" HeaderText="DN No." 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="300px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" ItemStyle-Height="50px" HeaderStyle-Height="50px">
                     </asp:BoundField>
                      <asp:BoundField DataField="Supplier" ItemStyle-BorderColor="Black" HeaderText="Supplier" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="300px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign ="Center" ItemStyle-ForeColor="Black"  >
                     </asp:BoundField>
                      <asp:BoundField DataField="ActualRFIDQty" ItemStyle-BorderColor="Black" HeaderText="Actual<br/>RFID Qty." HtmlEncode="False" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" HeaderStyle-Width="110px"  >
                     </asp:BoundField>
                      <asp:BoundField DataField="SupplierRFIDQty" ItemStyle-BorderColor="Black" HeaderText="Supplier RFID Qty." 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                     </asp:BoundField>
                      <asp:BoundField DataField="Difference" ItemStyle-BorderColor="Black" HeaderText="Difference" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                     </asp:BoundField>
                      <asp:BoundField DataField="FirstArrivalTime" ItemStyle-BorderColor="Black" HeaderText="First Arrival Time" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="300px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                     </asp:BoundField>
                 </Columns>
               </asp:GridView>    
           </ContentTemplate>
               <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btnDisplay" EventName="Click" />
               </Triggers>
           </asp:UpdatePanel>            
    </div>
    
 </asp:Content>