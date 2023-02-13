<%@ Page Language="C#"  MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PDMAMonitoring.aspx.cs" Inherits="FGWHSEClient.Form.PDMAMonitoring" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

    var TIMER_INTERVAL = 10000;
    setTimeout(refreshPage, TIMER_INTERVAL);




    function refreshPage() {
        //var e = document.getElementById('ctl00$ContentPlaceHolder1$ddlLoadingDock');
        //var strLoadingDock = e.options[e.selectedIndex].value;
        //window.location = "DNReceivingScreen.aspx?loadingdock=" + strLoadingDock;
        // window.location = "http://172.16.52.100/EWHS/Form/DNReceivingScreen.aspx?loadingdock=LD0101";
        //var displayButton = document.getElementById("ctl00_ContentPlaceHolder1_btnDisplay");
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
 
    <div style="float:right; margin-right:15px; font-size:13px; color:Black">
    Gate: &nbsp; <asp:DropDownList ID="ddlLoadingDock" runat="server" 
            Width="250px" 
            AutoPostBack="True" 
            onselectedindexchanged="ddlLoadingDock_SelectedIndexChanged">
            <asp:ListItem Value="Gate1">Gate 1</asp:ListItem>
        </asp:DropDownList>
    </div>
    <br />
    <br />

   &nbsp; &nbsp; <font style="font-size:15px; color:Black"> PD MA MONITORING </font>
   <br />
   <br />
    <div style="display:none;">
        <asp:Button ID="btnDisplay" runat="server" Text="Display" 
            onclick="btnDisplay_Click" />
    </div>
 </div>
 
  <div align="center">
    
           <asp:UpdatePanel runat="server" ID="UPanel1">
           <ContentTemplate>
               <asp:GridView ID="gvParts" runat="server" Width="1150px" 
                AutoGenerateColumns="False" CellPadding="3" BackColor="White" Font-Size="Small" 
                 GridLines="Both" onrowdatabound="gvParts_RowDataBound1"  >
                 <RowStyle ForeColor="Black" BorderColor="black"  BorderWidth="1pt" />
                 <Columns>
                     
                     <asp:TemplateField ItemStyle-BorderColor="Black" HeaderText="USER ID" HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px" ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" ItemStyle-Height="50px" HeaderStyle-Height="50px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkUserID" runat="server" OnClick ="lnk_Click" Text='<%#Bind("RegisterUserID") %>'>LinkButton</asp:LinkButton>
               
                                </ItemTemplate>
                    </asp:TemplateField>


                     <asp:TemplateField ItemStyle-BorderColor="Black" HeaderText="USER ID" HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px" ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" ItemStyle-Height="50px" HeaderStyle-Height="50px">
                          <HeaderStyle CssClass="NoDisplay"/>
                             <ItemStyle CssClass="NoDisplay"/>        
                            <ItemTemplate>

                                    <asp:Label ID="lblEKTransferListID" Text='<%#Bind("TransferListID") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                    </asp:TemplateField>


                     <asp:BoundField DataField="User_Name" ItemStyle-BorderColor="Black" HeaderText="USER NAME" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="400px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" ItemStyle-Height="50px" HeaderStyle-Height="50px">
                     </asp:BoundField>
                      <asp:BoundField DataField="Actual" ItemStyle-BorderColor="Black" HeaderText="ACTUAL RFID LIST QTY" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign ="Center" ItemStyle-ForeColor="Black"  >
                     </asp:BoundField>
                      <asp:BoundField DataField="Target" ItemStyle-BorderColor="Black" HeaderText="RFID LIST QTY" HtmlEncode="False" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" HeaderStyle-Width="110px"  >
                     </asp:BoundField>
                      <asp:BoundField DataField="DIFFERENCE" ItemStyle-BorderColor="Black" HeaderText="DIFFERENCE" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                       ItemStyle-Font-Size="23px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                     </asp:BoundField>
                      <asp:BoundField DataField="LINEID" ItemStyle-BorderColor="Black" HeaderText="LINE ID" 
                      HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
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