<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="PDAntennaMonitoringScreen.aspx.cs" Inherits="FGWHSEClient.Form.PDAntennaMonitoringScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<span style="font-size:12px;"><a href="javascript:history.back()"><span style="text-decoration: none;border:0;"><img src="../Image/Back.png" style="border:0;vertical-align:middle;" />Back to Monitoring Screen</span></a></span>


<div style ="margin-left:20px;top:110px;width:900px">
    <div>

    <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
    <br />
    </div>
    <div>
        <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="False"  CellPadding="3" BackColor="White"  GridLines="Both">
        <RowStyle ForeColor="Black"  BorderColor="black"  BorderWidth="1pt" />
        <Columns>
            <asp:BoundField DataField="EKREFNO" ItemStyle-BorderColor="Black" HeaderText="EK Refno." 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px"  HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
            </asp:BoundField>

            <asp:BoundField DataField="LINEID" ItemStyle-BorderColor="Black" HeaderText="Line ID" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px"  HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
            </asp:BoundField>


            <asp:BoundField DataField="PICKDATE" ItemStyle-BorderColor="Black" HeaderText="EK Pick Date" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px"  HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
            </asp:BoundField>


            <asp:BoundField DataField="ANTENNAREADDATE" ItemStyle-BorderColor="Black" HeaderText="Antenna Read Date" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px"  HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
            </asp:BoundField>

            <asp:BoundField DataField="PICKSTATUS" ItemStyle-BorderColor="Black" HeaderText="STATUS" 
                  HeaderStyle-BackColor="#538dd5" HeaderStyle-Height="30px"  HeaderStyle-Font-Size="13px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="350px"
                   ItemStyle-Font-Size="11px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
            </asp:BoundField>

        </Columns>

    </asp:GridView>
    </div>
    
</div>
</asp:Content>