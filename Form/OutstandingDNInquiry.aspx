<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="OutstandingDNInquiry.aspx.cs" Inherits="FGWHSEClient.OutstandingDNInquiry" Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style =" left:40px;top:110px;">
   <br />
   &nbsp; &nbsp; <font style="font-size:17px; color:Black; font-weight:bold"> Oustanding DN Inquiry </font>
   <br />

    <div style="margin-left:16px; margin-top:20px; color:Black">
    <table border=0 cellspacing=2 style="font-size:13px; font-weight:normal">
        <tr>
            <td>
                DN NO:
            </td>
            <td>
                <asp:TextBox ID="txtDNNo" runat="server" Width="250px"></asp:TextBox>
            </td>
           
        </tr>
        <tr>
            <td colspan="2" style="padding-top:20px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="150px" 
                    onclick="btnSearch_Click" /> &nbsp; &nbsp;
                  <asp:Button ID="btnExcel" runat="server" Text="Excel" Width="150px" 
                    onclick="btnExcel_Click" /> 
            </td>
        </tr>
   </table>
   </div>
     <asp:Panel ID="pnlDN" runat="server">
    <br />
    
    <div id ="grid" runat = "server" align="center" style="font-size:10px;overflow:auto;">
    <table>
        <tr>
            <td colspan ="10"></td>
        </tr>
        <tr>
            <td>
    <asp:GridView ID="gvOutstandingData" runat="server" Width="1160px" 
            AutoGenerateColumns="False"  CellPadding="3" BackColor="White"  
             GridLines="Both" AllowPaging="true" PageSize="30" 
                    onpageindexchanging="gvOutstandingData_PageIndexChanging" >
             <RowStyle ForeColor="Black"  BorderColor="black"  BorderWidth="1pt" />
             <Columns>
                <asp:TemplateField ItemStyle-BorderColor="Black" HeaderText="NO." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="50px"
                   ItemStyle-Font-Size="11px"  ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
            <ItemTemplate> <%# Container.DataItemIndex + 1 %> </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
                    
                    <asp:BoundField DataField="BARCODE DN" ItemStyle-BorderColor="Black" HeaderText="BARCODED DN" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px"  ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="DNNO" ItemStyle-BorderColor="Black" HeaderText="DN NO." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="DNLINE" ItemStyle-BorderColor="Black" HeaderText="DN LINE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="INVOICE" ItemStyle-BorderColor="Black" HeaderText="INVOICE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="DELIVERY DATE" ItemStyle-BorderColor="Black" HeaderText="DELIVERY DATE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="PO NO." ItemStyle-BorderColor="Black" HeaderText="PO NO." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="PO LINE" ItemStyle-BorderColor="Black" HeaderText="PO LINE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="PLANT" ItemStyle-BorderColor="Black" HeaderText="PLANT" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="100px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="RECEIVING LOCATION" ItemStyle-BorderColor="Black" HeaderText="RECEIVING LOCATION" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="ITEMCODE" ItemStyle-BorderColor="Black" HeaderText="ITEMCODE" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                  <asp:BoundField DataField="DESCRIPTION" ItemStyle-BorderColor="Black" HeaderText="DESCRIPTION" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="370px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="QTY" ItemStyle-BorderColor="Black" HeaderText="QTY" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="200px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                 </asp:BoundField>
             </Columns>
    </asp:GridView>
    </td>
    </tr>
    </table>
    </div>
    
   </asp:Panel>
   
   </div>

  <cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
