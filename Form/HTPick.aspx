<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="HTPick.aspx.cs" Inherits="FGWHSEClient.Form.HTPick" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript">
         
    function onEnterPalletNo(e) 
        {
           
            
            if (e.keyCode == 13) 
            {
               var Pallet = document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtPallet" %>').value;
               var exPallet = document.getElementById('<%= "ctl00_ContentPlaceHolder1_lblPallet" %>').innerHTML;
            
               if(Pallet == exPallet)
               {
               
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnPick" %>').click();
//                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtPallet" %>').disabled = true;
//                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtLocation" %>').disabled = false;  
//                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtLocation" %>').focus();
//                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_lblError" %>').innerHTML = ""
                    
                    return false;
                }
                else
                {
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtLocation" %>').disabled = true;
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtPallet" %>').value = "";
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_lblError" %>').innerHTML = "Incorrect Pallet scanned!"
                }
            }
           
        }
   
   
   
   function onEnterLocation(e) 
        {
            
            
            if (e.keyCode == 13) 
            {
               var Location = document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtLocation" %>').value;
               var exLocation = document.getElementById('<%= "ctl00_ContentPlaceHolder1_lblLocationGuide" %>').innerHTML;
               
               var Pallet = document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtPallet" %>').value;
               
               
               if(Location == exLocation)
               {
//                document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtLocation" %>').focus();
                document.getElementById('<%= "ctl00_ContentPlaceHolder1_lblError" %>').innerHTML = ""
                return false;
                }
                 else
                {
                    
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtLocation" %>').value = "";
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_lblError" %>').innerHTML = "Incorrect Location scanned!"
                    document.getElementById('<%= "ctl00_ContentPlaceHolder1_lblOK" %>').innerHTML = ""
                }
            }
           
        }
        
        
        
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

    
<div style =" left:40px;top:110px; font-weight:600; width:477px; color: Black">
    <br />
    <center>
        <div style ="border:solid 1px black; width:450px">
        <br />
            <center>
            <div style ="display:none">
            <asp:Label ID="lblPalletUnit" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblPallet" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnPick" runat="server" Text="Button" onclick="btnPick_Click" />
           
            
            </div>
                
            <table>
                <tr>
                    <td style ="text-align:left; font-weight:bold">CONTAINER &nbsp;&nbsp;</td>
                    <td style ="text-align:left; font-weight:normal"><asp:Label ID="lblContainer" runat="server" Text=""></asp:Label></td>
                </tr>
                
                <tr>
                    <td style ="text-align:left; font-weight:bold">ODN NO &nbsp;&nbsp;</td>
                    <td style ="text-align:left; font-weight:normal"><asp:Label ID="lblODNO" runat="server" Text=""></asp:Label></td>
                </tr>
                
                <tr>
                    <td style ="text-align:left; font-weight:bold">V-LANE &nbsp;&nbsp;</td>
                    <td style ="text-align:left; font-weight:normal"><asp:Label ID="lblVLANE" runat="server" Text=""></asp:Label></td>
                </tr>
                
                <tr>
                    <td style ="text-align:left; font-weight:bold">L-BAY &nbsp;&nbsp;</td>
                    <td style ="text-align:left; font-weight:normal"><asp:Label ID="lblLBAY" runat="server" Text=""></asp:Label></td>
                </tr>
                
                
            </table>
            <br />
            </center>
            
         </div>
        
         <br />
         
         <table>
            <tr>
                <td><asp:TextBox ID="txtPallet" runat="server" style="text-align: center" Font-Size="X-Large" Height="25px" Width="289px" onkeyup="onEnterPalletNo(event)"></asp:TextBox></td>
            </tr>
            <tr>
                <td>PALLET :<asp:Label ID="lblPalletGuide" runat="server" Text=""></asp:Label><br /><br /></td>
            </tr>
            
            <tr>
                <td><asp:TextBox ID="txtLocation" runat="server" style="text-align: center"  Font-Size="X-Large" Height="25px" Width="289px" onkeyup= "onEnterLocation(event)"> </asp:TextBox></td>
            </tr>
            <tr>
                <td>LOCATION :<asp:Label ID="lblLocationGuide" runat="server" Text=""></asp:Label></td>
            </tr>
         </table>
         <br />
         
         
         <asp:GridView ID="grdPickingList" runat="server" AutoGenerateColumns ="false">
                <Columns>
                    
                    
                    <asp:TemplateField HeaderText = "PALLET NO">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate><asp:Label ID="lblPALLETNO" runat="server" Text='<%# Eval("PALLETNO") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText = "PRODUCT CODE">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate><asp:Label ID="lblPALLETNO" runat="server" Text='<%# Eval("PRODUCTCODE") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText = "STATUS">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate><asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUSNAME") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                
                </Columns>
                <HeaderStyle Font-Size ="Medium" ForeColor="Black"  Font-Bold="True"  BackColor = "#C5D9F1" BorderStyle ="Solid" BorderColor ="Black" BorderWidth = "1px" />
                <RowStyle Font-Size ="Medium" ForeColor="Black" Font-Bold ="false" BorderStyle ="Solid" BorderColor ="Black" BorderWidth = "1px" />
                
            </asp:GridView>
            <br />
            
            <table>
                <tr>
                    <td><asp:Button ID="btnExecute" runat="server" Text="Execute [F4]" onclick="btnExecute_Click" /></td>
                    <td>&nbsp;&nbsp;</td>
                    <td><asp:Button ID="btnCancel" runat="server" Text="Cancel [F3]" onclick="btnCancel_Click" /></td>
                </tr>
            </table>
         </center>
        <br />
            <asp:Label ID="lblError" runat="server" ForeColor ="Red" Text=""></asp:Label>
            <asp:Label ID="lblOK" runat="server" ForeColor ="Green" Text=""></asp:Label>
        <br />
        <br />
</div>

<%--<ajaxToolkit:TextBoxWatermarkExtender ID ="extPallet" runat="server" TargetControlID="txtPallet" WatermarkText=""   WatermarkCssClass="txtwatermark" ></ajaxToolkit:TextBoxWatermarkExtender>
<ajaxToolkit:TextBoxWatermarkExtender ID ="extLocation"  runat="server" TargetControlID="txtLocation" WatermarkText=""  WatermarkCssClass="txtwatermark" ></ajaxToolkit:TextBoxWatermarkExtender>
--%>
</asp:Content>