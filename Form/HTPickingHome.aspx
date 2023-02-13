<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" CodeBehind="HTPickingHome.aspx.cs" Inherits="FGWHSEClient.Form.HTPickingHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">
//    setInterval(function () 
//    { 
//        var grd = document.getElementById('<%= "ctl00_ContentPlaceHolder1_lblPickingListCount" %>').innerHTML 
//        if(grd == 0)
//        {
//            document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnPickingList" %>').click();
//        }
//     
//    }, 2000);
    
    
    
    document.onkeydown = function (evt) {
            if (navigator.userAgent.indexOf("Opera") == -1) {
                evt = evt || window.event;
            }
            DoTask(evt.keyCode);
        };
        function DoTask(keyCode) {
            switch (keyCode) {
                case 115:
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnExecute" %>').click();
                     break;
                case 114:
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnCancel" %>').click();
                    break;
               
            }
        }
        
  
</script>



    <asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Conditional" > 
        <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate> 
        <!-- your content here, no timer -->
            <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
            </asp:Timer>
            
            <div style =" left:40px;top:110px; font-weight:600; width:477px;">

            <br />
            &nbsp;&nbsp;PICKING LIST
            <center>

            <br /><span style ="display:none">
            <asp:Button ID="btnPickingList" runat="server" Text="" onclick="btnPickingList_Click" />
            <asp:Label ID="lblPickingListCount" runat="server" Text=""></asp:Label>
            </span><br />

            <div style ="margin-left:10px">
                <asp:GridView ID="grdPickingList" runat="server" AutoGenerateColumns ="false">
                    <Columns>
                        <asp:TemplateField HeaderText = "PALLET UNIT NO">
                            <HeaderStyle CssClass="nodisplay"/>
                            <ItemStyle HorizontalAlign="Center" CssClass="nodisplay"></ItemStyle>
                            <ItemTemplate><asp:Label ID="lblPALLETUNITNO" runat="server" Text='<%# Eval("PALLETUNITNO") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText = "CONTAINER NO">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate><asp:Label ID="lblCONTAINERNO" runat="server" Text='<%# Eval("CONTAINERNO") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText = "PALLET NO">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate><asp:Label ID="lblPALLETNO" runat="server" Text='<%# Eval("PALLETNO") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText = "LOCATION">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate><asp:Label ID="lblLOCATION" runat="server" Text='<%# Eval("LOCATION") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                    
                    </Columns>
                    <HeaderStyle Font-Size ="Large" ForeColor="Black"  Font-Bold="True"  BackColor = "#C5D9F1" BorderStyle ="Solid" BorderColor ="Black" BorderWidth = "1px" />
                    <RowStyle Font-Size ="Large" ForeColor="Black" Font-Bold ="false" BorderStyle ="Solid" BorderColor ="Black" BorderWidth = "1px" />
                    
                </asp:GridView>
                
                <br />
                
                <table id="tbPick" runat="server" style ="display:none">
                    <tr>
                        <td><asp:Button ID="btnExecute" runat="server" Text="Execute [F4]" onclick="btnExecute_Click" /></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:Button ID="btnCancel" runat="server" Text="Cancel [F3]" onclick="btnCancel_Click" /></td>
                    </tr>
                </table>
                
            </div>
            <br />
            </center>     
            </div>

        </ContentTemplate> 
    </asp:UpdatePanel> 







<cc1:msgBox id="MsgBox1" runat="server"></cc1:msgBox>
</asp:Content>