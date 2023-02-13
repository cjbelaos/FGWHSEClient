<%@ Page  Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="MasterRFIDTag.aspx.cs" Inherits="FGWHSEClient.Form.MasterRFIDTag" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;RFID TAG MASTER
<br /><br />

 &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="TOTAL RFID TAG:" style="font-size:12px; color: Black"></asp:Label>
 &nbsp;<asp:Label ID="lbltotalrfidtag" runat="server" Text="25,125" style="font-size:12px; color: Black"></asp:Label>
 
<br /><br />

<table>
<tr>
<td>
 <div class="divLocMaster">
        <table style ="text-align:left">
                <tr style ="color:Black">
                   <td style="padding:5px; text-align:right;">
                        RFID Tag No
                    </td>
                    <td style="padding:5px;text-align:left">
                    <asp:TextBox ID="txtRFIDTAG" Width="270px" runat="server"></asp:TextBox>
                        
                    </td>

                    
                </tr>
                 <tr style ="color:Black">
                   <td style="padding:5px; text-align:right;">
                        Supplier
                    </td>
                    <td style="padding:5px;text-align:left">
                        <asp:DropDownList ID="ddlSupplier" runat="server" Width="268px">
                        </asp:DropDownList>
                        &nbsp;</td>
                    
                </tr>
                
                <tr>
                    <td></td>
                    <td style =" text-align:LEFT">
                    
                     <asp:Button ID="btnSearch" runat="server" Text="SEARCH"  Width="81px" 
                            onclick="btnSearch_Click"/> &nbsp;&nbsp
                    </td>
                </tr>
                
          </table>  
       
         </div>

</td>

<td style="padding:5px; text-align:right;width: 772px; vertical-align:top">
  
   <div class="divLocMaster" style="text-align:left;">
        <table style ="text-align:left;">
                <tr style ="color:Black">
                   <td style="padding:5px; text-align:right;">
                        Upload RFID Tag
                    </td>
                    <td style="padding:5px;text-align:right">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td></td>
                    <td style =" text-align:LEFT">
                     <asp:Button ID="lblCheck" runat="server" Text="CHECK"  Width="81px" 
                            onclick="lblCheck_Click"/> &nbsp;&nbsp
                    </td>
                </tr>
                
          </table>  
       
         </div>

</td>

</tr>

</table>





            <br />
     
</div>     

  <div  id ="divUpload" runat="server" visible=false style="width:1030px; margin-left:75px; height:400px">
     <table border="1" cellpadding="0" cellspacing="0" style="background-color:#00008b; color:White">
     <tr>
     <td style="width:249px;font-size:12px; text-align:center;height:25px">RFID TAG NO</td>
     <td style="width:249px;font-size:12px; text-align:center">EPC DATA</td>
     <td style="width:249px;font-size:12px; text-align:center">SUPPLIER</td>
     <td style="width:249px;font-size:12px; text-align:center">REMARKS</td>
     </tr>
     </table>
     
    <div style="width:1030px; overflow:auto; height:360px" >

       <asp:GridView ID="grdRFIDUpload" runat="server" AutoGenerateColumns="False" 
            Font-Names="Calibri" style="font-size: medium" Width="1000px" 
             onrowdatabound="grdRFIDUpload_RowDataBound"  ShowHeader="False">
        
        <Columns>
        
                   <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="RFID TAG NO" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRFIDTAG" runat="server" Height="25px" Text='<%# Bind("RFIDTAG") %>' Width="250px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                      <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="EPC DATA" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblEPCDATA" runat="server" Height="25px" Text='<%# Bind("EPCDATA") %>' Width="250px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                      <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="SUPPLIER" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSupplier" runat="server" Height="25px" Text='<%# Bind("SUPPLIER") %>' Width="250px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                      <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="REMARKS" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Height="25px" Text='<%# Bind("REMARKS") %>' Width="250px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


        

          
            
      

                
            
      </Columns>
         <FooterStyle BackColor="#00008b" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
         <PagerStyle BackColor="#00008b" ForeColor="White" HorizontalAlign="Left" Width="500px"/>
         <SelectedRowStyle BackColor="LightBlue" Font-Bold="True" ForeColor="White" />
         <HeaderStyle Height="25pt" Font-Size="11pt" BackColor=#00008b ForeColor=White/>
         <RowStyle Height="20pt" HorizontalAlign=Center Font-Size="10pt" />
         
     </asp:GridView>
    
     </div>    
     
     <center>      
     <asp:Button ID="btnClear" runat="server" Text="CLEAR" Width="120px" Height="30" 
             onclick="btnClear_Click" />
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Button ID="btnSave" runat="server" Text="SAVE" Width="120px" Height="30" 
             onclick="btnSave_Click"/>
     </center>

     
     </div>
     
     <div id ="divSearch" runat="server" visible=false style="width:1200px;">
      <table border="1" cellpadding="0" cellspacing="0" style="background-color:#00008b; color:White">
     <td style="width:209px;font-size:12px; text-align:center;">RFID TAG NO</td>
     <td style="width:209px;font-size:12px; text-align:center">EPC DATA</td>
     <td style="width:144px;font-size:12px; text-align:center">CREATED DATE</td>
     <td style="width:159px;font-size:12px; text-align:center">SUPPLIER</td>
     <td style="width:144px;font-size:12px; text-align:center">LAST SUPPLIER SCAN DATE</td>
     <td style="width:69px;font-size:12px; text-align:center">SUPPLIER LOAD COUNT</td>
     <td style="width:149px;font-size:12px; text-align:center">LAST RECEIVED DATE</td>
     <td style="width:69px;font-size:12px; text-align:center">EPPI RECEIVE COUNT</td>
     </tr>
     </table>
     
    <div style="width:1200px; overflow:auto; height:400px" >

     
      <asp:GridView ID="grdRFIDSearch" runat="server" AutoGenerateColumns="False" 
            Font-Names="Calibri" style="font-size: medium" Width="1000px" ShowHeader="False">
        <%--AllowPaging = 'true' PageSize="17" --%>
        <Columns>
        
                   <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="RFID TAG NO" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRFIDTAG" runat="server" Height="25px" Text='<%# Bind("RFIDTAG") %>' Width="210px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                      <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="EPC DATA" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblEPCDATA" runat="server" Height="25px" Text='<%# Bind("EPC") %>' Width="210px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="CREATED DATE" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedDate" runat="server" Height="25px" Text='<%# Bind("CREATEDDATE") %>' Width="145px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="SUPPLIER" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSupplierLocation" runat="server" Height="25px" Text='<%# Bind("SUPPLIERLOCATION") %>' Width="160px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="LAST SUPPLIER SCAN DATE" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblLastLoas" runat="server" Height="25px" Text='<%# Bind("LASTLOAD") %>' Width="145px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="SUPPLIER LOAD COUNT" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblLoadCount" runat="server" Height="25px" Text='<%# Bind("LOADCOUNT") %>' Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                      <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="LAST RECEIVE DATE" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblLastReceived" runat="server" Height="25px" Text='<%# Bind("LASTRECEIVED") %>' Width="150px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                       <asp:TemplateField HeaderStyle-ForeColor="WhiteSmoke" HeaderText="EPPI RECEIVE COUNT" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblReceivedCount" runat="server" Height="25px" Text='<%# Bind("RECEIVECOUNT") %>' Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  Font-Bold="True"  />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


        

          
            
      

                
            
      </Columns>
         <FooterStyle BackColor="#00008b" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
         <PagerStyle BackColor="#00008b" ForeColor="White" HorizontalAlign="Left" Width="500px"/>
         <SelectedRowStyle BackColor="LightBlue" Font-Bold="True" ForeColor="White" />
         <HeaderStyle Height="25pt" Font-Size="11pt" BackColor=#00008b ForeColor=White  />
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
    
    

    
        
    </div>




    
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>