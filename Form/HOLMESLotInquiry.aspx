<%@ Page Language="C#"  MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="HOLMESLotInquiry.aspx.cs" Inherits="FGWHSEClient.Form.AntennaScanHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <div style =" left:40px;top:110px;">
    <br />
   &nbsp; &nbsp; <font style="font-size:15px; color:Black">HOLMES RECEIVE LOT INQUIRY</font>
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
                  <asp:Button ID="btnExcel" runat="server" Text="Excel" Width="150px" onclick="btnExcel_Click" />  &nbsp; &nbsp;
                <asp:Button ID="btnClear" runat="server" Text="Clear" Width="150px" onclick="btnClear_Click" /> 
            </td>
        </tr>
   </table>

        <br />

        <div style="width:1150px;height:280px;overflow:auto">
                <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns ="false" Width ="1450Px">
                <Columns>

                       <asp:TemplateField HeaderStyle-Width="110px" HeaderText="LOT NO"  ItemStyle-Width="110px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLOTNO" runat="server" Text='<%#Bind("LOTNO") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="120px" HeaderText="REF NO"  ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREFNO" runat="server" Text='<%#Bind("REFNO") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="120px" HeaderText="RFID TAG"  ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRFIDTAG" runat="server" Text='<%#Bind("RFIDTAG") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="80px" HeaderText="PART CODE"  ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPARTCODE" runat="server" Text='<%#Bind("PARTCODE") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="70px" HeaderText="QTY"  ItemStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTY" runat="server" Text='<%#Bind("QTY") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>



                    <asp:TemplateField HeaderStyle-Width="80px" HeaderText="AREA NAME"  ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAREANAME" runat="server" Text='<%#Bind("AREANAME") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>



                    <asp:TemplateField HeaderStyle-Width="80px" HeaderText="GNS LOCATION"  ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSYSTEMLOCATION" runat="server" Text='<%#Bind("SYSTEMLOCATION") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    
                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="WH LOCATION"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLOCATION" runat="server" Text='<%#Bind("LOCATION") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                      


                    <asp:TemplateField HeaderStyle-Width="70px" HeaderText="INSPECTION"  ItemStyle-Width="70px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINSPECTION" runat="server" Text='<%#Bind("INSPECTION") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                     
                    <asp:TemplateField HeaderStyle-Width="70px" HeaderText="CAPACITY"  ItemStyle-Width="70px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCAPACITY" runat="server" Text='<%#Bind("CAPACITY") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="WH NAME"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWAREHOUSENAME" runat="server" Text='<%#Bind("WAREHOUSENAME") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                     <asp:TemplateField HeaderStyle-Width="70px" HeaderText="HOLD"  ItemStyle-Width="70px" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblHoldStatus" runat="server" Text='<%#Bind("HOLD_STATUS") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="HOLMES IF DATE"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateDate" runat="server" Text='<%#Bind("CREATEDDATE") %>'></asp:Label>
                                    </ItemTemplate>
                     </asp:TemplateField>

                      <asp:TemplateField HeaderStyle-Width="150px" HeaderText="UPDATE DATE"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUpdateDate" runat="server" Text='<%#Bind("UPDATEDDATE") %>'></asp:Label>
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