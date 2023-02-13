<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="HOLMESLotUpdateHistory.aspx.cs" Inherits="FGWHSEClient.Form.HOLMESLotUpdateHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style =" left:40px;top:110px;">
    <br />
   &nbsp; &nbsp; <font style="font-size:15px; color:Black">PPD LOT TRACE INQUIRY</font>
   <br />
   
    <div style="margin-left:16px; margin-top:20px; color:Black">
   <table border=0 cellspacing=2 style="font-size:13px; font-weight:normal">
        <tr>
            <td>
                PARTCODE:
            </td>
            <td>
                <asp:TextBox ID="txtPartCode" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td>&nbsp;&nbsp;</td>
            <td>
                LOT NO:
            </td>
            <td>
                <asp:TextBox ID="txtLotNo" runat="server" Width="250px"></asp:TextBox>
            </td>
           
        </tr>
          <tr>
            <td>
                RFIDTAG:
            </td>
            <td>
                <asp:TextBox ID="txtRFIDTag" runat="server" Width="250px"></asp:TextBox>
            </td>

              <td>&nbsp;&nbsp;</td>
              <td>
                REF NO:
            </td>
            <td>
                <asp:TextBox ID="txtRefNo" runat="server" Width="250px"></asp:TextBox>
            </td>
           
        </tr>
 
      
        <tr>
             <td>
                TRANSACTION DATE:
            </td>
            <td>
                <asp:TextBox ID="txtDateFrom" Width="150px" runat="server"></asp:TextBox> &nbsp;
                 <asp:ImageButton ID="imgCalendar3" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" /> &nbsp; - &nbsp;
        <asp:TextBox ID="txtDateTo" Width="150px" runat="server"></asp:TextBox> &nbsp;
          <asp:ImageButton ID="imgCalendar2" runat="server" ImageUrl="../Image/calendar_1.png" Height="20px" />  
            </td>

            <td></td>

            <td>
                AREA:
            </td>
            <td>
                <asp:DropDownList ID ="ddlArea" runat="server" Width="250px"></asp:DropDownList>
            </td>
        </tr>
      
        
        <tr>
            <td colspan="2" style="padding-top:20px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="150px" 
                    onclick="btnSearch_Click" /> &nbsp; &nbsp;
                  <asp:Button ID="btnExcel" runat="server" Text="Excel" Width="150px" onclick="btnExcel_Click" /> &nbsp; &nbsp;
                <asp:Button ID="btnClear" runat="server" Text="Clear" Width="150px" onclick="btnClear_Click" /> 
            </td>
        </tr>
   </table>

        <br />

        <div style="width:1150px;height:280px;overflow:auto">
                <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns ="false" Width ="1450Px" OnDataBound="grdDetails_DataBound" HorizontalAlign="Center" >
                 <Columns>
                  
                    

                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="LOT NO"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLOTNO" runat="server" Text='<%#Bind("LOTNO") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                      <asp:TemplateField HeaderStyle-Width="100px" HeaderText="REF NO"  ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREFNO" runat="server" Text='<%#Bind("REFNO") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="80px" HeaderText="PART CODE"  ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPARTCODE" runat="server" Text='<%#Bind("PARTCODE") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="70px" HeaderText="QTY"  ItemStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTY" runat="server" Text='<%#Bind("QTY") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="REMARKS"  ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%#Bind("REMARKS") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="90px" HeaderText="UPDATE FIELD"  ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUpdateField" runat="server" Text='<%#Bind("UpdateField") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="90px" HeaderText="OLD DATA"  ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOLDDATA" runat="server" Text='<%#Bind("OLDDATA") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="NEW DATA"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNEWDATA" runat="server" Text='<%#Bind("NEWDATA") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                      <asp:TemplateField HeaderStyle-Width="150px" HeaderText="UPDATED DATE"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Bind("UpdatedDate") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                      <asp:TemplateField HeaderStyle-Width="150px" HeaderText="UPDATED BY"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUpdatedBy" runat="server" Text='<%#Bind("UpdatedBy") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                     
                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="RFID TAG"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRFIDTAG" runat="server" Text='<%#Bind("RFIDTAG") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>



                </Columns>
                <HeaderStyle Font-Size="9pt" HorizontalAlign="center" VerticalAlign="Middle" Font-Bold="True"  BackColor = "#C5D9F1"  />
                <RowStyle Font-Size="9pt" HorizontalAlign="center" VerticalAlign="Middle" Font-Bold ="false"  />
                
                
                </asp:GridView>
            </div>

   </div>
   
   <%-- Details per RFID Tag--%>  
    <div runat="server" id="divexport">
     <table border=0 style="margin-top:10px; ">
           
            <tr>
                <td colspan="2" style="margin-top:10px">
                   <asp:Panel ID="pnl" runat="server" Width="1160px" >
                       
                    
                   </asp:Panel> 
                   <%--<asp:UpdatePanel ID="pnl" runat="server">
                    <ContentTemplate>
                    
                    </ContentTemplate>
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDisplay" EventName="Click" />
                    </Triggers>
                   </asp:UpdatePanel> --%>
                   <br />
                   <br />
                   
                </td>
            </tr>
       </table>
       </div>
</div>

<ajaxToolkit:Calendarextender ID="Calendarextender1" runat="server" BehaviorID="calendar1"
TargetControlID="txtDateFrom"
Format="dd-MMM-yyyy HH:mm tt"
PopupButtonID="imgCalendar3"  >
</ajaxToolkit:Calendarextender>


<ajaxToolkit:Calendarextender ID="Calendarextender3" runat="server" BehaviorID="calendar2"
TargetControlID="txtDateTo"
Format="dd-MMM-yyyy HH:mm tt"
PopupButtonID="imgCalendar2">
</ajaxToolkit:Calendarextender>
</asp:Content>