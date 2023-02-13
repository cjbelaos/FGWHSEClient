<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartsDeliveryInspectionPreview.aspx.cs" Inherits="FGWHSEClient.Form.PartsDeliveryInspectionPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Parts Delivery-Inspection Matrix</title>
    <link href="StyleSheet_Font.css" rel="stylesheet" type="text/css" />
</head>
<body>
      <script>

        function PrintDivContent() {
        
        var PrintCommand = '<object ID="PrintCommandObject" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></object>';
        document.body.insertAdjacentHTML('beforeEnd', PrintCommand);
        PrintCommandObject.ExecWB(6, 2,0,0);

             }
         </script>

    <form id="form1" runat="server">
    <div style="width:100%;text-align:left;font-family:Calibri">
        <center>
            <div style="color:Green; font-weight:bold;font-size:24px;">
                <asp:Label ID="lblTitle" runat="server"></asp:Label></div>
        </center>
        <br />
        <table style="font-size:12px">
            <tr>
                <td>DN No.:</td>
              <%--  <td style="width:70px; border-bottom:solid 1px black">
                    <asp:Label ID="lblDN" runat="server"></asp:Label></td>--%>
                <td  class="BarcodeFont" style="font-size:34px;border-bottom:solid 1px black">
                    <% Response.Write(strDN); %></td>
                <td style="width:10px;"></td>
                <td>Supplier:</td>
                <td style="width:70px; border-bottom:solid 1px black">
                    <asp:Label ID="lblSupplier" runat="server"></asp:Label>&nbsp;</td>
                <td style="width:10px;"></td>
                <td>Time of Arrival:</td>
                <td style="width:70px; text-align:center; border-bottom:solid 1px black">:</td>
                <td style="width:10px;"></td>
                <td>Start Receiving:</td>
                <td style="width:70px; text-align:center; border-bottom:solid 1px black">:</td>
            </tr>
             <tr>
                <td>DR No.:</td>
                <td style="width:70px; border-bottom:solid 1px black">
                    <asp:Label ID="lblDRNo" runat="server"></asp:Label>&nbsp;</td>
                <td style="width:10px;"></td>
                <td>Invoice No.:</td>
                <td style="width:70px; border-bottom:solid 1px black">
                    <asp:Label ID="lblInvoice" runat="server"></asp:Label>&nbsp;</td>
                <td style="width:10px;"></td>
                <td>Received By:</td>
                <td style="width:70px; border-bottom:solid 1px black">&nbsp;</td>
                <td style="width:10px;"></td>
                <td>Finished Receiving:</td>
                <td style="width:70px; text-align:center; border-bottom:solid 1px black">:</td>
            </tr>
        </table>
        
        <br />

                        <asp:GridView ID="grdDNData" runat="server" CellPadding="4" ForeColor="#333333" 
                        BorderStyle="Solid"
                        AutoGenerateColumns="False"
                        Font-Size="12px" Font-Bold="false" onrowdatabound="grdDNData_RowDataBound">
        
                    <Columns>
                    
                    <asp:TemplateField  HeaderText="No." 
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px"
                    ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate> <%# Container.DataItemIndex + 1 %> </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:BoundField HeaderText="Partcode"   DataField="PARTCODE" >
                            <HeaderStyle Width="80px">
                              </HeaderStyle>
                         </asp:BoundField>	
                     <asp:BoundField HeaderText="Partname"   DataField="Partname" >
                        <HeaderStyle Width="190px">
                          </HeaderStyle>
                     </asp:BoundField>	   
                    <asp:BoundField HeaderText="Quantity"   DataField="QUANTITY">
                            <HeaderStyle Width="70px">
                              </HeaderStyle>
                              <ItemStyle HorizontalAlign="Right"/>
                         </asp:BoundField>	   
                    <asp:BoundField HeaderText="WH Location"   DataField="WHLOCATION">
                            <HeaderStyle Width="80px">
                              </HeaderStyle>
                         </asp:BoundField>
                     <asp:BoundField HeaderText="Floor"   DataField="FLOORID">
                        <HeaderStyle Width="50px">
                          </HeaderStyle>
                     </asp:BoundField>	
                      <asp:BoundField HeaderText="Scanning Type"   DataField="TYPE">
                        <HeaderStyle Width="90px">
                          </HeaderStyle>
                     </asp:BoundField>
                      <asp:BoundField HeaderText="Inspection Reqmt."   DataField="InspectionReqmt">
                        <HeaderStyle Width="90px">
                          </HeaderStyle>
                     </asp:BoundField>	   
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#4f81bd" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="White" Font-Size="12px"/>
                </asp:GridView>
    </div>
    </form>
</body>
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</html>
