<%@ Page  Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="MasterItemControlLimit.aspx.cs" Inherits="FGWHSEClient.Form.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;ITEM CONTROL LIMIT MASTER
<br /><br />


 <div class="divLocMaster">
        <table style ="text-align:left">
                <tr style ="color:Black">
                   <td style="padding:5px; text-align:right;">
                        Partcode
                    </td>
                    <td style="padding:5px;text-align:right">
                    <asp:TextBox ID="txtPartCode" Width="200px" runat="server" MaxLength="11"></asp:TextBox>
                        
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" 
                            onclick="btnSearch_Click" />
                    </td>
                    
                </tr>
                <tr>
                    <td></td>
                    <td style =" text-align:right">
                    
                     <asp:Button ID="btnAdd" runat="server" Text="ADD"  Width="81px"
                    onclick="btnAdd_Click"/> &nbsp;&nbsp
                    </td>
                    <td>
                    
                        <asp:Button ID="btnExport" runat="server" Text="Export" 
                            onclick="btnExport_Click" Width="81px"/>
                    </td>
                </tr>
                
          </table>  
       
         </div>
         
            <br />
     
</div>     
    <div style="width:1200px; overflow:auto; height:555px">
     <div align="center">

        <asp:GridView ID="grdItemControLimitMaster" runat="server" AutoGenerateColumns="False" 
            Font-Names="Calibri" style="font-size: medium" Width="900px" 
            AllowPaging = 'true' PageSize="17"
            onrowdeleting="grdLocationMaster_RowDeleting"
            onrowediting="grdLocationMaster_RowEditing"
            onpageindexchanging="grdLocationMaster_PageIndexChanging">
        
        <Columns>
        
        
           <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False"
                   EditText="<img src='../Image/Edit.png' alt='Edit this Record' border='0' />" 
                   DeleteText="<img src='../Image/Delete.png' alt='Delete this Record' border='0' />">
                  <ItemStyle Width="50px" />
            </asp:CommandField>
            


            <asp:BoundField DataField="part_code" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="PART CODE" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />

<ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            
            <asp:BoundField DataField="description" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="200pt" HeaderText="DESCRIPTION" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="200pt" />

<ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            
            <asp:BoundField DataField="warning_limit" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="WARNING LIMIT" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                
            <asp:BoundField DataField="delivery_limit" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="DELIVERY LIMIT" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />

<ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            
            <asp:BoundField DataField="expiration_limit" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="EXPIRATION LIMIT" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />

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
          
          

<table style="margin-left:10px">
        <tr>
            <td height="15px"></td>
        </tr>
        <tr>
            <td>
               </td>
        </tr>
     </table>
     <div id = "dvPrint" runat = "server">
    
    
        <asp:GridView ID="grdExport" runat="server">                   
        </asp:GridView>
    
        
    </div>

<asp:Panel ID="pnlAdd" runat="server"  style="display:none" >
            <div class="pnlHeader">
                &nbsp;Add / Edit Item Limit Control</div>
            
            <div id="divApproval" class="pnlShowApproval">
                <table width="350px">
                    <tr>
                        <td class="style2">Part Code</td>
                        <td>
                            <asp:TextBox ID="txtPcode" runat="server" MaxLength="20" Width="180px" 
                                BackColor="LightYellow"></asp:TextBox>
                          
                        </td>
                    </tr>    
                    <tr>
                        <td class="style2">Description</td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Width="180px" 
                                BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>   
                    <tr>
                        <td class="style2">Warning Limit</td>
                        <td>
                            <asp:TextBox ID="txtWarningLimit" runat="server" Width="180px" MaxLength="50" BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>     
                    <tr>
                        <td class="style2">Delivery Limit</td>
                        <td>
                            <asp:TextBox ID="txtDeliveryLimit" runat="server" Width="180px" MaxLength="50" BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>    
                    <tr>
                        <td class="style2">Expiration Limit</td>
                        <td>
                            <asp:TextBox ID="txtExpirationLimit" runat="server" Width="180px" MaxLength="50" BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>
                     
                    
                    
                    
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td class="style3">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Height="30px" Text="Save" Width="80px" 
                                onclick="btnSave_Click"/>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Height="30px" Text="Cancel" Width="80px"  />
                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            
            
            <div style="display:none">
            <asp:Button ID="btnHidden" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="hidIsEdit" runat="server" />
       </div>
</asp:Panel>
        
        
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
    BackgroundCssClass="watermarked"
    TargetControlID="btnHidden"
    CancelControlID="btnCancel"
    PopupControlID="pnlAdd">
</ajaxToolkit:ModalPopupExtender>   

 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
 TargetControlID="txtWarningLimit"  FilterType="Numbers" />
 
 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
 TargetControlID="txtDeliveryLimit"  FilterType="Numbers" />
 
 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
 TargetControlID="txtExpirationLimit"  FilterType="Numbers" />
    
    
    
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
