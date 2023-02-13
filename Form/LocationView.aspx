<%@ Page Language="C#"  MasterPageFile="~/Form/MasterPalletMonitoring.Master"  AutoEventWireup="true" CodeBehind="LocationView.aspx.cs" Inherits="FGWHSEClient.Form.LocationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">


function getScrollPosition()
{
    document.getElementById('<%= "ctl00_ContentPlaceHolder1_hdfy" %>').value = document.getElementById('<%= "ctl00_ContentPlaceHolder1_pnlBorder" %>').scrollTop;
    document.getElementById('<%= "ctl00_ContentPlaceHolder1_hdfx" %>').value = document.getElementById('<%= "ctl00_ContentPlaceHolder1_pnlBorder" %>').scrollLeft;
} 


function SetScrollPosition()
{
  var y = document.getElementById('<%= "ctl00_ContentPlaceHolder1_hdfy" %>').value;
  var x = document.getElementById('<%= "ctl00_ContentPlaceHolder1_hdfx" %>').value;
  
  document.getElementById('<%= "ctl00_ContentPlaceHolder1_pnlBorder" %>').scrollTop = y;
  document.getElementById('<%= "ctl00_ContentPlaceHolder1_pnlBorder" %>').scrollLeft = x;
} 

</script>
<input id="hdfy" type="hidden" runat="server" NAME="hdfy">
<input id="hdfx" type="hidden" runat="server" NAME="hdfx">
<link href="../App_Themes/Stylesheet/Main.css" rel="stylesheet" type="text/css" />
<div id="dvPage" runat= "server">
    <table id="tbDIV" runat="server">
    <tr>
        <td id="tdHeaderContainer" runat="server" style ="text-align:center; ">
            <table id="tbHeader" runat="server"  cellspacing = "0" style ="font-weight:bold">
        
            </table>
        </td>
    </tr>
    <tr>
    <td><br /></td>
    </tr>
    <tr>
        <td id = "tdBodyLayOut" runat ="server">
            <table id="tblLayOut" runat="server"  cellspacing = "0" style ="font-weight:bold">
        
            </table>
        </td>
    </tr>
    </table>
</div>
<%--<ajaxToolkit:modalpopupextender ID="mp1"   runat="server" PopupControlID="pnlPop"  TargetControlID="tbDIV" BackgroundCssClass="modalBackground" ></ajaxToolkit:modalpopupextender>--%>
    <%--<asp:Panel ID="pnlPop" runat="server">
    </asp:Panel>--%>
    
  <asp:Timer ID="Timer1" runat="server" Interval ="60000" >
    </asp:Timer> 
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>