<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="PPWMonitoring.aspx.cs" Inherits="FGWHSEClient.Form.PPWMonitoring" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

    var TIMER_INTERVAL = 10000;
    setTimeout(refreshPage, TIMER_INTERVAL);

    function refreshPage() {
        //var e = document.getElementById('ctl00$ContentPlaceHolder1$ddlLoadingDock');
        //var strLoadingDock = e.options[e.selectedIndex].value;
        //window.location = "DNReceivingScreen.aspx?loadingdock=" + strLoadingDock;
        // window.location = "http://172.16.52.100/EWHS/Form/DNReceivingScreen.aspx?loadingdock=LD0101";
//24JUN2021        var displayButton = document.getElementById("ctl00_ContentPlaceHolder1_btnDisplay");
        var displayButton = document.getElementById("<%= btnDisplay.ClientID %>");
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
      <div style="float:right; margin-right:15px; font-size:13px; color:Black;">
    Gate: &nbsp; <asp:DropDownList ID="ddlLoadingDock" runat="server" 
            Width="250px" 
            AutoPostBack="True" 
              onselectedindexchanged="ddlLoadingDock_SelectedIndexChanged">
        </asp:DropDownList>
    </div>

    <br />
    <br />
    <br />
    <center><asp:Label ID="Label1" runat="server" Text="PPWH MONITORING" 
            Font-Names="Calibri" Font-Size="24px" ForeColor="Black"></asp:Label></center>
    <div style="display:none;">
        <asp:Button ID="btnDisplay" runat="server" Text="Display" 
            onclick="btnDisplay_Click" />
    </div>
    <br />
 </div>
 
  <div align="center">
    
           <asp:UpdatePanel runat="server" ID="UPanel1">
           <ContentTemplate>
               <asp:GridView ID="gvReceive" runat="server" Width="1150px" 
                AutoGenerateColumns="False" CellPadding="5"  BackColor="White" Font-Size="Small" 
                 GridLines="Both" onrowdatabound="gvReceive_RowDataBound">
                 <RowStyle ForeColor="Black" BorderColor="black"  BorderWidth="1pt" />
                 <Columns>
                    <asp:TemplateField HeaderText="Part Code" SortExpression="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#538dd5" HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Size="15px" ItemStyle-Width="120px" ItemStyle-Font-Size="23px" ItemStyle-Height="50px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <asp:Label ID="lblPartCode" runat="server" Text='<%# Bind("PartCode") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                <%--     <asp:BoundField DataField="PartCode" ItemStyle-BorderColor="Black" HeaderText="Part Code" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" ItemStyle-Height="50px" HeaderStyle-Height="50px">
                     </asp:BoundField>--%>
                     
                      <asp:BoundField DataField="PartName" HeaderText="Part Name" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="460px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign ="Left"  >
                     </asp:BoundField>
                     
                    <asp:TemplateField HeaderText="Box Count" SortExpression="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#538dd5" HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Size="15px" ItemStyle-Width="100px" ItemStyle-Font-Size="23px" ItemStyle-Height="50px">
                    <ItemTemplate>
                            <asp:Label ID="lblBoxCount" runat="server" Text='<%# Bind("BoxCount") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                      <%--<asp:BoundField DataField="BoxCount" ItemStyle-BorderColor="Black" HeaderText="Box Count" HtmlEncode="False" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" HeaderStyle-Width="110px"  >
                     </asp:BoundField>--%>
                     
                     <asp:TemplateField HeaderText="Qty" SortExpression="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#538dd5" HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Size="15px" ItemStyle-Width="100px" ItemStyle-Font-Size="23px" ItemStyle-Height="50px">
                    <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                   <%--   <asp:BoundField DataField="Qty" ItemStyle-BorderColor="Black" HeaderText="Qty." 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                     </asp:BoundField>--%>
                     
                     <asp:TemplateField HeaderText="Date" SortExpression="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#538dd5" HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Size="15px" ItemStyle-Width="150px" ItemStyle-Font-Size="23px" ItemStyle-Height="50px">
                    <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>' DataFormatString="{0:g}"></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                     
                    <%-- <asp:BoundField DataField="Date" ItemStyle-BorderColor="Black" HeaderText="Date" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                     </asp:BoundField>--%>
                     
                      <asp:TemplateField HeaderText="Time" SortExpression="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#538dd5" HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Size="15px" ItemStyle-Width="100px" ItemStyle-Font-Size="23px" ItemStyle-Height="50px">
                    <ItemTemplate>
                            <asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                     
                   <%--  <asp:BoundField DataField="Time" ItemStyle-BorderColor="Black" HeaderText="Time" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                     </asp:BoundField>--%>
                     
                     <%-- <asp:BoundField DataField="Print" ItemStyle-BorderColor="Black" HeaderText="Print" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                     </asp:BoundField>--%>
                     
                     <asp:TemplateField HeaderText="Print" SortExpression="Filepath" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-BackColor="#538dd5" HeaderStyle-ForeColor="White"
                     HeaderStyle-Font-Size="15px" ItemStyle-Font-Size="23px">
                <ItemTemplate>

                    <asp:LinkButton ID="lnkEmpNo" ForeColor = "#3063A5" Font-Underline = "true" Font-Bold ="true" runat="server" CausesValidation="false" CommandName="Edit"
                        Text="Print" OnClick="lnkEmpNo_Click">
                    </asp:LinkButton>
                    
                    
                </ItemTemplate>

                <HeaderStyle Width="100px"></HeaderStyle>
                

               
              </asp:TemplateField>
              
               <asp:TemplateField HeaderText="LotMovementID" SortExpression="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#538dd5" HeaderStyle-ForeColor="White"
                    HeaderStyle-Font-Size="15px" ItemStyle-Width="150px" ItemStyle-Font-Size="23px" ItemStyle-Height="50px">
                    <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("LotMovementID") %>' DataFormatString="{0:g}"></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
              
                     
                 </Columns>
               </asp:GridView>    
           </ContentTemplate>
               <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btnDisplay" EventName="Click" />
               </Triggers>
           </asp:UpdatePanel>            
    </div>
</asp:Content>