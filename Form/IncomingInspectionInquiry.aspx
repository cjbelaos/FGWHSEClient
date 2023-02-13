<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="IncomingInspectionInquiry.aspx.cs" Inherits="FGWHSEClient.Form.IncomingInspectionInquiry" Title="Incoming Inspection Inquiry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style =" left:40px;top:50px;">

   &nbsp; &nbsp; <font style="font-size:15px; color:Black"> Incoming Inspection Inquiry </font>
   <br />
   
    <div style="margin-left:16px; margin-top:20px; color:Black">
   <table border=0 cellspacing=2 style="font-size:13px; font-weight:normal">
        <tr>
            <td>
                DN NO:<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDNNo" runat="server" Width="570px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="Separated by comma if multiple DN" style="font-size:10px"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" style="padding-top:20px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="150px" onclick="btnSearch_Click" 
                    /> &nbsp; &nbsp;
                  <asp:Button ID="btnExcel" runat="server" Text="Excel" Width="150px" onclick="btnExcel_Click" 
                     /> 
            </td>
        </tr>
   </table>
   </div>
   
   <div id ="grid" runat = "server" align="center" style="font-size:10px;overflow:auto;">
    <table>
        <tr>
            <td>
    <asp:GridView ID="gvIncoming" runat="server" 
            AutoGenerateColumns="False"  CellPadding="3" BackColor="White"  
             GridLines="Both" AllowPaging="false"  
                    RowStyle-Wrap="True" onpageindexchanging="gvIncoming_PageIndexChanging" 
                    onrowdatabound="gvIncoming_RowDataBound" 
                    onprerender="gvIncoming_PreRender" ForeColor="Black" 
                    onselectedindexchanged="gvIncoming_SelectedIndexChanged">
             <RowStyle  BorderColor="black"  BorderWidth="1pt" />
             <Columns>
                <asp:BoundField DataField="BARCODEDNNO" ItemStyle-BorderColor="Black" HeaderText="DN NO" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-Width="150px" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="18px" ItemStyle-Height="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="false" >
                 </asp:BoundField>
                 <asp:BoundField DataField="ITEMCODE" ItemStyle-BorderColor="Black" HeaderText="ITEM CODE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-Width="185px" ItemStyle-Width="185px"
                   ItemStyle-Font-Size="24px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="false"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="PARTSDESCRIPTION" ItemStyle-BorderColor="Black" HeaderText="DESCRIPTION" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="200px"
                   ItemStyle-Font-Size="18px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  ItemStyle-Font-Bold="false" >
                 </asp:BoundField>
                 <asp:BoundField DataField="LOTNO" ItemStyle-BorderColor="Black" HeaderText="LOT NO." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="225px"
                   ItemStyle-Font-Size="24px" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="false" >
                 </asp:BoundField>
                  <asp:BoundField DataField="EPPIINSPECTIONCHECK" ItemStyle-BorderColor="Black" HeaderText="INSPECTION REQMT." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="175px"
                   ItemStyle-Font-Size="24px" ItemStyle-HorizontalAlign="Center"  ItemStyle-Font-Bold="false">
                 </asp:BoundField>
                  <asp:BoundField DataField="EPPILOCATIONCHECK" ItemStyle-BorderColor="Black" HeaderText="WH LOCATION" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="215px"
                   ItemStyle-Font-Size="24px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"   ItemStyle-Font-Bold="false">
                 </asp:BoundField>
                 <asp:BoundField HeaderText="PARTCODE_COLOR"   DataField="PARTCODE_COLOR" Visible="false">
                        <HeaderStyle Width="10px">
                          </HeaderStyle>
                 </asp:BoundField>	
                 
             </Columns>
             
    </asp:GridView>
    </td>
    </tr>
    </table>
    </div>
 </div>
 <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>

