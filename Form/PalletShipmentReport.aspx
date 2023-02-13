<%@ Page Title="PALLET SHIPMENT REPORT" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PalletShipmentReport.aspx.cs" Inherits="FGWHSEClient.Form.PalletShipmentReport"  EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta http-equiv="refresh" content="60">


<div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;PALLET SHIPMENT REPORT
<br /><br />

        <div class="divPalletShipment">
        
        <table width="800px"><tr><td>
        
        
        
                <table style ="text-align:left" width="550px">
                        <tr style ="color:Black">
                           <td style="padding:5px; text-align:right;">
                                Container No.<font style="color:Red;">*</font>
                            </td>
                            <td style="padding:5px;text-align:left">
                                <asp:TextBox ID="txtContainerNo" runat="server" Width="200px" Height="25px" Font-Size="20px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr style ="color:Black"><td style="padding:5px; text-align:right">
                                O/D No.
                            </td>
                            <td style="padding:5px">
                                <asp:TextBox ID="txtODNo" runat="server" Width="200px" Height="25px"
                                    Font-Size="20px"></asp:TextBox></td></tr>
                             
                                
                      <tr><td></td></tr>
                         
                         
                        <tr>
                        <td>
                        
                        <font style="color:Red; font-size:x-small">* Required Field</font>
                        <br />
                        <font style="color:Blue; font-size:x-small; font-style:italic">Auto Refresh every 1 min.</font>
                        
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="SEARCH" Height="35px" 
                                Width="70px" onclick="btnSearch_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnExcelDownload" runat="server" Text="EXCEL DOWNLOAD" 
                                Height="35px" Width="150px" onclick="btnExcelDownload_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnPrintReport" runat="server" Text="PRINT REPORT" 
                                Height="35px" Width="120px" onclick="btnPrintReport_Click" />
                            
                           
                        </td>
                        </tr>
                        
                  </table>  
          
          </td>
          
          
          <td width="300px">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pallet Scanned: &nbsp; <asp:Label ID="lblPalletScanned" runat="server" ForeColor="Red" Font-Size="25px"></asp:Label>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessage" runat="server" ForeColor="Blue" Font-Size="15px"></asp:Label>
          </td>
          
          
          </tr></table>
          
          
          
          
          
         </div>
          
          <br /><br />
          
          <div id="divGrdPalletValidation" runat="server" align="center">
          
          <asp:GridView ID="GrdPalletValidation" runat="server" AutoGenerateColumns="False"
            ondatabound="GrdPalletValidation_DataBound" onrowdatabound="GrdPalletValidation_RowDataBound"
            onrowdeleting="GrdPalletValidation_RowDeleting" Font-Names="Calibri" style="font-size: medium" Width="900px">
           <Columns>
        
        
           <asp:CommandField ShowDeleteButton="true" HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False"
               HeaderStyle-BackColor="LightBlue" DeleteText="<img src='../Image/Delete.png' alt='Delete this Record' border='0' />">
           <ItemStyle Width="50px" />
           </asp:CommandField>
            
            
           <asp:BoundField DataField="container_no" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-ForeColor="Black"
                HeaderStyle-Width="100pt" HeaderText="Container" ItemStyle-Wrap="False" >
           <HeaderStyle HorizontalAlign="Center" Width="100pt" />
           <ItemStyle Wrap="False"></ItemStyle>
           </asp:BoundField>
            
            
           <asp:BoundField DataField="od_no" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-ForeColor="Black"
                HeaderStyle-Width="100pt" HeaderText="OD No." ItemStyle-Wrap="False" >
           <HeaderStyle HorizontalAlign="Center" Width="100pt" />
           <ItemStyle Wrap="False"></ItemStyle>
           </asp:BoundField>
           
            
           <asp:BoundField DataField="pallet_no" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue" HeaderStyle-ForeColor="Black"
                HeaderStyle-Width="100pt" HeaderText="Pallet No." ItemStyle-Wrap="False" >
           <HeaderStyle HorizontalAlign="Center" Width="100pt" />
           <ItemStyle Wrap="False"></ItemStyle>
           </asp:BoundField>
           
  
           <asp:BoundField DataField="CONTAINERNO" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="CornflowerBlue" HeaderStyle-ForeColor="Black"
                HeaderStyle-Width="100pt" HeaderText="Container" ItemStyle-Wrap="False" >
           <HeaderStyle HorizontalAlign="Center" Width="100pt" />
           <ItemStyle Wrap="False"></ItemStyle>
           </asp:BoundField>
            
            
            <asp:BoundField DataField="PALLETNUMBER" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="CornflowerBlue" HeaderStyle-ForeColor="Black"
                HeaderStyle-Width="100pt" HeaderText="Pallet No." ItemStyle-Wrap="False" >
            <HeaderStyle HorizontalAlign="Center" Width="100pt" />
            <ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            
            
            <asp:BoundField DataField="CREATEDDATE" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="CornflowerBlue" HeaderStyle-ForeColor="Black"
                HeaderStyle-Width="130pt" HeaderText="Date/Time" ItemStyle-Wrap="False" >
            <HeaderStyle HorizontalAlign="Center" Width="130pt" />
            <ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            
            
            <asp:BoundField DataField="FORKLIFTDRIVER" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="CornflowerBlue" HeaderStyle-ForeColor="Black"
                HeaderStyle-Width="100pt" HeaderText="Forklift Driver" ItemStyle-Wrap="False" >
            <HeaderStyle HorizontalAlign="Center" Width="100pt" />
            <ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            
            
            <asp:BoundField DataField="RESULT" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                HeaderStyle-Width="50pt" HeaderText="Result" ItemStyle-Wrap="False" >
            <HeaderStyle HorizontalAlign="Center" Width="50pt" />
            <ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            
            
            <asp:BoundField DataField="ID" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black"
                HeaderStyle-Width="50pt" HeaderText="ID" ItemStyle-Wrap="False" >
            <HeaderStyle HorizontalAlign="Center" Width="50pt" />
            <ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            
            
            
         </Columns>
         <FooterStyle BackColor="#00008b" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
         <PagerStyle BackColor="#00008b" ForeColor="White" HorizontalAlign="Left" Width="500px"/>
         <SelectedRowStyle BackColor="LightBlue" Font-Bold="True" ForeColor="White" />
         <HeaderStyle Height="25pt" Font-Size="11pt" BackColor=#00008b ForeColor=White/>
         <RowStyle Height="20pt" HorizontalAlign=Center Font-Size="10pt" />
          
          </asp:GridView>
          
          </div>
          
          
          
          
          
        </div>
 

<asp:HiddenField ID="hidID" runat="server" />

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
