<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="LocationMaster.aspx.cs" Inherits="FGWHSEClient.Form.LocationMaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language ="javascript" type="text/javascript">

    function body_onLoad() {
        document.all("txtLocationID").focus();
    }

</script>


<div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;LOCATION MASTER
<br /><br />

        <div class="divLocMaster">
        <table style ="text-align:left">
                <tr style ="color:Black">
                   <td style="padding:5px; text-align:right;">
                        Warehouse
                    </td>
                    <td style="padding:5px;text-align:right">
                        <asp:DropDownList ID="ddWH" runat="server"  Width="200px" >
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" 
                            onclick="btnSearch_Click" />
                    </td>
                    
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style =" text-align:right">
                        <asp:Button ID="btnAdd" runat="server" Text="ADD" Width="81px"  onclick="btnAdd_Click" />
                         &nbsp;&nbsp
                    </td>
                    <td>
                        <asp:Button ID="btnExport" runat="server" Text="Export" 
                            onclick="btnExport_Click" Width="81px"/>
                    </td>
                </tr>
          </table>  
         </div>
          
          
        </div>
        
        
        <br />
        
        
    <div style="width:1200px; overflow:auto; height:555px">
     <div align="center">

        <asp:GridView ID="grdLocationMaster" runat="server" AutoGenerateColumns="False" 
            Font-Names="Calibri" style="font-size: medium" Width="1100px" 
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
            


            <asp:BoundField DataField="Location_ID" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="LOCATION" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Location_Name" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="180pt" HeaderText="LOCATION NAME" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="180pt" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Location_Type_Name" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="150pt" HeaderText="LOCATION TYPE" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="150pt" />
                </asp:BoundField>
                
            <asp:BoundField DataField="Unit_Type_Name" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="MANAGEMENT UNIT" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Lines" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="80pt" HeaderText="LINES" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="80pt" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Rows" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="80pt" HeaderText="ROWS" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="80pt" />
            </asp:BoundField>

            <asp:BoundField DataField="Display_Order" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="80pt" HeaderText="DISPLAY ORDER" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="80pt" />
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
            
        </tr>
     </table>
     
     
     
     
     
     
   <asp:Panel ID="pnlAdd" runat="server" style="display:none">
            <div class="pnlHeader">
                &nbsp;Add / Edit Location</div>
            
            <div id="divApproval" class="pnlShowApproval">
                <table width="350px">
                    <tr>
                        <td class="style2">Warehouse</td>
                        <td>
                            <asp:DropDownList ID="ddAddWH" runat="server"  Width="180px" BackColor="LightYellow">
                            </asp:DropDownList>
                          
                        </td>
                    </tr>    
                    <tr>
                        <td class="style2">Location ID</td>
                        <td>
                            <asp:TextBox ID="txtLocationID" runat="server" MaxLength="20" Width="180px" 
                                BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>   
                    <tr>
                        <td class="style2">Location Name</td>
                        <td>
                            <asp:TextBox ID="txtLocationName" runat="server" Width="180px" MaxLength="50" BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>     
                    <tr>
                        <td class="style2">Location Type</td>
                        <td>
                            <asp:DropDownList ID="ddLocationType" runat="server"  Width="180px" BackColor="LightYellow">
                            </asp:DropDownList>
                        </td>
                    </tr>    
                    <tr>
                        <td class="style2">Management Unit</td>
                        <td>
                            <asp:DropDownList ID="ddUnit" runat="server"  Width="180px" BackColor="LightYellow">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td class="style2">Lines</td>
                        <td>
                              <asp:TextBox ID="txtLines" runat="server" Width="180px" BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>
                   
                    
                     <tr>
                        <td class="style2">Rows</td>
                        <td>
                            <asp:TextBox ID="txtRows" runat="server" Width="180px" BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>
                    
                      <tr>
                        <td class="style2">Display Order</td>
                        <td>
                             <asp:TextBox ID="txtDisplayOrder" runat="server" Width="180px" BackColor="LightYellow"></asp:TextBox>
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
        </asp:Panel>
        
        <div id = "dvPrint" runat = "server">
    
    
        <asp:GridView ID="grdExport" runat="server">                   
        </asp:GridView>
    
        
    </div>
        
       <div style="display:none">
            <asp:Button ID="btnHidden" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="hidIsEdit" runat="server" />
       </div>
  
  
  
        
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
    BackgroundCssClass="watermarked"
    TargetControlID="btnHidden"
    CancelControlID="btnCancel"
    PopupControlID="pnlAdd">
    </ajaxToolkit:ModalPopupExtender>    
     
     
<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
 TargetControlID="txtLines"  FilterType="Numbers" />
     
<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
 TargetControlID="txtRows"  FilterType="Numbers" />   
 
 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
 TargetControlID="txtDisplayOrder"  FilterType="Numbers" />
   

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>

