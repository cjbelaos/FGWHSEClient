<%@ Page  Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="MasterPartsLocationCheck_PPD.aspx.cs" Inherits="FGWHSEClient.Form.MasterPartsLocationCheck_PPD" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <style type="text/css">
    
    .GridPager span
    {
        background-color: #A1DCF2;
        color: black;
        border: 1px solid #3AC0F2;
    }

    .hiddencol { display: none; }
</style>


<div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;PPD PARTS LOCATION CHECK MASTER
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
                    onclick="btnAdd_Click" /> &nbsp;&nbsp
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

  <asp:Panel ID="pnlDN" runat="server">
    <div id ="grid"  runat = "server"  style="width:1200px; overflow:auto; height:555px">
     <div align="center">

        <asp:GridView ID="grdItemControLimitMaster" runat="server" AutoGenerateColumns="False" 
            Font-Names="Calibri" style="font-size: medium" Width="950px" 
            AllowPaging = 'true' PageSize="17"
            onrowdeleting="grdLocationMaster_RowDeleting"
            onrowediting="grdLocationMaster_RowEditing"
            onpageindexchanging="grdLocationMaster_PageIndexChanging" CssClass="GridPager">
        
        <Columns>
        
        
           <asp:CommandField ShowEditButton="true" ShowDeleteButton="True" HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False"
                   EditText="<img src='../Image/Edit.png' alt='Edit this Record' border='0' />" 
                   DeleteText="<img src='../Image/Delete.png' alt='Delete this Record' border='0' />">
                  <ItemStyle Width="50px" />
            </asp:CommandField>
            


            <asp:BoundField DataField="partcode" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="PART CODE" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />

            <ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            
            <asp:BoundField DataField="partdescription" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="200pt" HeaderText="DESCRIPTION" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="200pt" />

            <ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            
            <asp:BoundField DataField="location" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="150pt" HeaderText="LOCATION" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />

                <ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                
                 <asp:BoundField DataField="GNS_Location" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="150pt" HeaderText="GNS LOCATION" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />

                <ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                
                 <asp:BoundField DataField="Warehouse_Name" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="150pt" HeaderText="WAREHOUSE NAME" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="150pt" />

                <ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                
                
                 <asp:BoundField DataField="Inspection" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="Inspection" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />

                <ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="EKanbanRFIDIFFlag" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="EKANBAN IF" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />

                <ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="Capacity" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="CAPACITY" ItemStyle-Wrap="False" >
                <HeaderStyle HorizontalAlign="Center" Width="100pt" />

                <ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>

               <asp:BoundField DataField="ID" HeaderStyle-HorizontalAlign="Center" 
                HeaderStyle-Width="100pt" HeaderText="ID" ItemStyle-Wrap="False" Visible="true"  ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol">
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
  </asp:Panel>   
          

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
                &nbsp;Add / Edit Parts Location</div>
            
            <div id="divApproval" class="pnlShowApproval">
                <table width="350px">
                    <tr>
                        <td class="style2">Part Code</td>
                        <td>
                            <asp:TextBox ID="txtPcode" runat="server" MaxLength="20" Width="180px" 
                                BackColor="LightYellow" ontextchanged="txtPcode_TextChanged" AutoPostBack="true"></asp:TextBox>
                          
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
                        <td class="style2">Warehouse Name</td>
                        <td>
                            <asp:TextBox ID="txtWhseName" runat="server" MaxLength="100" Width="180px" 
                                BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>   
                    <tr>
                        <td class="style2">Location</td>
                        <td>
                            <asp:TextBox ID="txtLocation" runat="server" Width="180px" MaxLength="30" 
                                BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>   
                    
                    <%--<tr>
                        <td class="style2">Stoktaking Location</td>
                        <td>
                            <asp:TextBox ID="txtLocationST" runat="server" Width="180px" MaxLength="30" 
                                BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>     --%>

                        <tr>
                        <td class="style2">GNS Location</td>
                        <td>
                            <asp:TextBox ID="txtGNSLocation" runat="server" Width="180px" MaxLength="30" 
                                BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>     
                    
                     <tr>
                        <td class="style2">Inspection</td>
                        <td>
                            <%--<asp:CheckBox ID="chKIQA" runat="server" />--%>
                            <asp:DropDownList ID="ddlIQA" runat="server">
                             <asp:ListItem Value=""></asp:ListItem>
                             <asp:ListItem Value="FOR IQA">FOR IQA</asp:ListItem>
                             <asp:ListItem Value="FOR IQC">FOR IQC</asp:ListItem>
                             <asp:ListItem Value="FOR SSC">FOR SSC</asp:ListItem>
                            </asp:DropDownList>
                            
                        </td>
                    </tr>     
                    
                     <tr>
                        <td class="style2">EkanbanIF (RefNo-RFID) </td>
                        <td>
                             <asp:CheckBox ID="chkEKANBAN" runat="server" />
                        </td>
                    </tr>     
                    
                     <tr>
                        <td class="style2">Capacity</td>
                        <td>
                            <asp:TextBox ID="txtCapacity" runat="server" Width="180px" MaxLength="30" 
                                BackColor="LightYellow"></asp:TextBox>
                        </td>
                    </tr>     
                    
                    

                    
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td class="style3">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Height="30px" Text="Save" Width="80px" 
                                onclick="btnSave_Click"/>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Height="30px" Text="Cancel" Width="80px" onclick="btnCancel_Click"/>
                            <asp:Button ID="btnCancel1" runat="server" Height="30px" Text="Cancel" Width="80px" Visible="false"/>
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

    PopupControlID="pnlAdd">
</ajaxToolkit:ModalPopupExtender>   
     <%--   CancelControlID="btnCancel"--%>
    
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>