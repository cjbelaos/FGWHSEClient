<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master"  CodeBehind="ReportsForwarderWSS.aspx.cs" Inherits="FGWHSEClient.Form.ReportsForwarderWSS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <link href="../App_Themes/Stylesheet/Main.css" rel="stylesheet" type="text/css" />
    
    <asp:HiddenField id="hdnScrollPos" runat="server"/> 
    <div>
    <div style=" margin-left:15px">
    <br />
        <asp:Label ID="lblHead" runat="server" Text="WSS History"></asp:Label>
        <span style ="display:none"><asp:Label ID="lblTag" runat="server" Text="Updating Of Container"></asp:Label></span>
<%--    <span></span>--%>
    <br />
    <br />
    <table style="margin-left:15px; font-size:small; color:Black">
        <tr>
            <td>Invoice No.</td>
            <td><asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox></td>
            <td>&nbsp&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>OB Date From</td>
            <td><asp:TextBox ID="txtOBDate" runat="server"></asp:TextBox></td>
            <td>&nbsp&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>OB Date To</td>
            <td><asp:TextBox ID="txtOBDateTo" runat="server"></asp:TextBox></td>
            <td style ="display:none"><asp:Label ID="lblUserID" runat="server" Text=""></asp:Label><asp:Label ID="lblTemp" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>OD No.</td>
            <td><asp:TextBox ID="txtODNo" runat="server"></asp:TextBox></td>
            <td>&nbsp&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>ExFact Date From</td>
            <td><asp:TextBox ID="txtExFactDate" runat="server"></asp:TextBox></td>
            <td>&nbsp&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>ExFact Date To</td>
            <td><asp:TextBox ID="txtExFactDateTo" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>PO No.</td>
            <td><asp:TextBox ID="txtPONo" runat="server"></asp:TextBox></td>
            <td></td>
            
            <td>Destination</td>
            <td><asp:TextBox ID="txtDestination" runat="server"></asp:TextBox></td>
            
            
        </tr>
        <tr>
            <td>Item Code</td>
            <td><asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox></td>
            <td></td>
            <td>Trade</td>
            <td><asp:TextBox ID="txtTrade" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Status</td>
            <td id ="tdStatusFilter" runat="server" colspan ="4"><asp:CheckBoxList ID="chckLStatus" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
        </tr>
        <tr>
            <td></td>
            <td colspan ="2"><asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" />
            &nbsp;
            <asp:Button ID="btnExport" runat="server" Text="Export" onclick="btnExport_Click" /></td>
            
        </tr>
    </table>
    <br />
    
    <center>
        <table id = "tbLegend" runat = "server" style ="font-size:x-small;color:Black">
            <tr id = "trLegend" runat = "server">
                <td>Legend : &nbsp;&nbsp;&nbsp;</td>
            </tr>
        </table>
    </center>
    
    <br />
    <div id="DivRoot">
        <div style="overflow: hidden; font-size:small" id="DivHeaderRow">

        </div>
        
        <div id="dvtest" runat="server" ></div>
        <div id="DivMainContent" runat = "server" style="overflow:scroll; width:1150px; height:400px; font-size:small; color:black"  >
        
            <asp:GridView ID="grdWeeklyShipmentSchedule" Width="6700px" runat="server" 
                AutoGenerateColumns ="False" 
                onrowediting="grdWeeklyShipmentSchedule_RowEditing" 
                onrowdatabound="grdWeeklyShipmentSchedule_RowDataBound">
            
            <Columns>
                
                
                <asp:TemplateField HeaderText = "ID">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("WSSID") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText = "GNS + INVOICE NUMBER">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtGNSINVOICENUMBER" Visible ="false" runat="server" Width="150px" BackColor="#ffffcc" Text='<%# Eval("GNSINVOICENUMBER")%>'></asp:TextBox>
                    <asp:Label ID="lblGNSINVOICENUMBER" runat="server" Text='<%# Eval("GNSINVOICENUMBER") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText = "PRODUCTION LOCATION">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtPRODUCTIONLOCATION" Visible ="false" runat="server" Width="150px" BackColor="#ffffcc" Text='<%# Eval("PRODUCTIONLOCATION")%>'></asp:TextBox>
                    <asp:Label ID="lblPRODUCTIONLOCATION" runat="server" Text='<%# Eval("PRODUCTIONLOCATION") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "SHIPPING LOCATION">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtSHIPPINGLOCATION" Visible ="false" runat="server" Width="130px" BackColor="#ffffcc" Text='<%# Eval("SHIPPINGLOCATION")%>'></asp:TextBox>
                    <asp:Label ID="lblSHIPPINGLOCATION" runat="server" Text='<%# Eval("SHIPPINGLOCATION") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "OD NO">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtODNO" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("ODNO")%>'></asp:TextBox>
                    <asp:Label ID="lblODNO" runat="server" Text='<%# Eval("ODNO") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "LOT NO">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtLOTNO" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("LOTNO")%>'></asp:TextBox>
                    <asp:Label ID="lblLOTNO" runat="server" Text='<%# Eval("LOTNO") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "SHIPMENT NO">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtSHIPMENTNO" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("SHIPMENTNO")%>'></asp:TextBox>
                    <asp:Label ID="lblSHIPMENTNO" runat="server" Text='<%# Eval("SHIPMENTNO") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "PO MONTH">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtPOMONTH" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("POMONTH")%>'></asp:TextBox>
                    <asp:Label ID="lblPOMONTH" runat="server" Text='<%# Eval("POMONTH") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "PO NUMBER">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtPONUMBER" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("PONUMBER")%>'></asp:TextBox>
                    <asp:Label ID="lblPONUMBER" runat="server" Text='<%# Eval("PONUMBER") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CONSIGNEE PO NUMBER">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCONSIGNEEPONUMBER" Visible ="false" runat="server" Width="150px" BackColor="#ffffcc" Text='<%# Eval("CONSIGNEEPONUMBER")%>'></asp:TextBox>
                    <asp:Label ID="lblCONSIGNEEPONUMBER" runat="server" Text='<%# Eval("CONSIGNEEPONUMBER") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "PO WK">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtPOWK" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("POWK")%>'></asp:TextBox>
                    <asp:Label ID="lblPOWK" runat="server" Text='<%# Eval("POWK") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "ITEM CODE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtITEMCODE" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("ITEMCODE")%>'></asp:TextBox>
                    <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "MODEL DESCRIPTION">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtMODELDESCRIPTION" Visible ="false" runat="server" Width="150px" BackColor="#ffffcc" Text='<%# Eval("MODELDESCRIPTION")%>'></asp:TextBox>
                    <asp:Label ID="lblMODELDESCRIPTION" runat="server" Text='<%# Eval("MODELDESCRIPTION") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "MODEL NAME">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtMODELNAME" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("MODELNAME")%>'></asp:TextBox>
                    <asp:Label ID="lblMODELNAME" runat="server" Text='<%# Eval("MODELNAME") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "PLANNED EX-FACT DATE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtPLANNEDEXFACTDATE" Visible ="false" runat="server" Width="160px" BackColor="#ffffcc" Text='<%# Eval("PLANNEDEXFACTDATE","{0:dd-MMM-yyyy}")%> '></asp:TextBox>
                    <asp:Label ID="lblPLANNEDEXFACTDATE" runat="server" Text='<%# Eval("PLANNEDEXFACTDATE","{0:dd-MMM-yyyy}") %>'></asp:Label>
                    <ajaxtoolkit:calendarextender ID="calPlannedDate" runat="server" TargetControlID="txtPLANNEDEXFACTDATE" Format="dd MMM yyyy" PopupButtonID="txtPLANNEDEXFACTDATE"></ajaxtoolkit:calendarextender>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "PLANNED OB DATE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtPLANNEDOBDATE" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("PLANNEDOBDATE","{0:dd-MMM-yyyy}")%>'></asp:TextBox>
                    <asp:Label ID="lblPLANNEDOBDATE" runat="server" Text='<%# Eval("PLANNEDOBDATE","{0:dd-MMM-yyyy}") %>'></asp:Label>
                    <ajaxtoolkit:calendarextender ID="calPLANNEDOBDATE" runat="server" TargetControlID="txtPLANNEDOBDATE" Format="dd MMM yyyy" PopupButtonID="txtPLANNEDOBDATE"></ajaxtoolkit:calendarextender>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                
                <asp:TemplateField HeaderText = "QTY">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtQTY" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("QTY")%>'></asp:TextBox>
                    <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("QTY") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CUSTOMER">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCUSTOMER" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("CUSTOMER")%>'></asp:TextBox>
                    <asp:Label ID="lblCUSTOMER" runat="server" Text='<%# Eval("CUSTOMER") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "SHIMPENT MOD">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtSHIMPENTMOD" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("SHIMPENTMOD")%>'></asp:TextBox>
                    <asp:Label ID="lblSHIMPENTMOD" runat="server" Text='<%# Eval("SHIMPENTMOD") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "PALLET TYPE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtPALLETTYPE" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("PALLETTYPE")%>'></asp:TextBox>
                    <asp:Label ID="lblPALLETTYPE" runat="server" Text='<%# Eval("PALLETTYPE") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "DESTINATION">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtDESTINATION" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("DESTINATION")%>'></asp:TextBox>
                    <asp:Label ID="lblDESTINATION" runat="server" Text='<%# Eval("DESTINATION") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText = "TRADE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtTRADE" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("TRADE")%>'></asp:TextBox>
                    <asp:Label ID="lblTRADE" runat="server" Text='<%# Eval("TRADE") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CONTAINER QTY HC">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCONTAINERQTYHC" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("CONTAINERQTYHC")%>'></asp:TextBox>
                    <asp:Label ID="lblCONTAINERQTYHC" runat="server" Text='<%# Eval("CONTAINERQTYHC") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CONTAINER QTY 40 FT">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCONTAINERQTY40FT" Visible ="false" runat="server" Width="150px" BackColor="#ffffcc" Text='<%# Eval("CONTAINERQTY40FT")%>'></asp:TextBox>
                    <asp:Label ID="lblCONTAINERQTY40FT" runat="server" Text='<%# Eval("CONTAINERQTY40FT") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CONTAINER QTY 20 FT">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCONTAINERQTY20FT" Visible ="false" runat="server" Width="150px" BackColor="#ffffcc" Text='<%# Eval("CONTAINERQTY20FT")%>'></asp:TextBox>
                    <asp:Label ID="lblCONTAINERQTY20FT" runat="server" Text='<%# Eval("CONTAINERQTY20FT") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "NO OF PALLET">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtNOOFPALLET" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("NOOFPALLET")%>'></asp:TextBox>
                    <asp:Label ID="lblNOOFPALLET" runat="server" Text='<%# Eval("NOOFPALLET") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "STD CONT LOAD">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtSTDCONTLOAD" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("STDCONTLOAD")%>'></asp:TextBox>
                    <asp:Label ID="lblSTDCONTLOAD" runat="server" Text='<%# Eval("STDCONTLOAD") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CONT USAGE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCONTUSAGE" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("CONTUSAGE")%>'></asp:TextBox>
                    <asp:Label ID="lblCONTUSAGE" runat="server" Text='<%# Eval("CONTUSAGE") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CONT USAGE RATIO">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCONTUSAGERATIO" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("CONTUSAGERATIO")%>'></asp:TextBox>
                    <asp:Label ID="lblCONTUSAGERATIO" runat="server" Text='<%# Eval("CONTUSAGERATIO") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CAS">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCAS" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("CAS","{0:dd-MMM-yyyy}")%>'></asp:TextBox>
                    <asp:Label ID="lblCAS" runat="server" Text='<%# Eval("CAS","{0:dd-MMM-yyyy}") %>'></asp:Label>
                    <ajaxtoolkit:calendarextender ID="calCAS" runat="server" TargetControlID="txtCAS" Format="dd MMM yyyy" PopupButtonID="txtCAS"></ajaxtoolkit:calendarextender>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CTR NUMBER">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCTRNUMBER" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("CTRNUMBER")%>'></asp:TextBox>
                    <asp:Label ID="lblCTRNUMBER" runat="server" Text='<%# Eval("CTRNUMBER") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CTR NUMBER ARRIVAL DATE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblCTRNUMBERARRIVALDATE" runat="server" Text='<%# Eval("CTRNUMBERARRIVALDATE","{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CONFIRMED DATE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCONFIRMEDDATE" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("CONFIRMEDDATE","{0:dd-MMM-yyyy}")%>'></asp:TextBox>
                    <asp:Label ID="lblCONFIRMEDDATE" runat="server" Text='<%# Eval("CONFIRMEDDATE","{0:dd-MMM-yyyy}") %>'></asp:Label>
                    <ajaxtoolkit:calendarextender ID="calCONFIRMEDDATE" runat="server" TargetControlID="txtCONFIRMEDDATE" Format="dd MMM yyyy" PopupButtonID="txtCONFIRMEDDATE"></ajaxtoolkit:calendarextender>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "SHIPPING LIN">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtSHIPPINGLIN" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("SHIPPINGLIN")%>'></asp:TextBox>
                    <asp:Label ID="lblSHIPPINGLIN" runat="server" Text='<%# Eval("SHIPPINGLIN") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "1ST VESSEL">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txt1STVESSEL" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("VESSEL1ST")%>'></asp:TextBox>
                    <asp:Label ID="lbl1STVESSEL" runat="server" Text='<%# Eval("VESSEL1ST") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "2ND VESSEL">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txt2NDVESSEL" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("VESSEL2ND")%>'></asp:TextBox>
                    <asp:Label ID="lbl2NDVESSEL" runat="server" Text='<%# Eval("VESSEL2ND") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "VESSEL DESTINATION">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtVESSELDESTINATION" Visible ="false" runat="server" Width="150px" BackColor="#ffffcc" Text='<%# Eval("VESSELDESTINATION")%>'></asp:TextBox>
                    <asp:Label ID="lblVESSELDESTINATION" runat="server" Text='<%# Eval("VESSELDESTINATION") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "PORT OF DISCHARGE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtPORTOFDISCHARGE" Visible ="false" runat="server" Width="150px" BackColor="#ffffcc" Text='<%# Eval("PORTOFDISCHARGE")%>'></asp:TextBox>
                    <asp:Label ID="lblPORTOFDISCHARGE" runat="server" Text='<%# Eval("PORTOFDISCHARGE") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "ETA DISCHARGE PORT">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtETADISCHARGEPORT" Visible ="false" runat="server" Width="150px" BackColor="#ffffcc" Text='<%# Eval("ETADISCHARGEPORT","{0:dd-MMM-yyyy}")%>'></asp:TextBox>
                    <asp:Label ID="lblETADISCHARGEPORT" runat="server" Text='<%# Eval("ETADISCHARGEPORT","{0:dd-MMM-yyyy}") %>'></asp:Label>
                    <ajaxtoolkit:calendarextender ID="calETADISCHARGEPOR" runat="server" TargetControlID="txtETADISCHARGEPORT" Format="dd MMM yyyy" PopupButtonID="txtETADISCHARGEPORT"></ajaxtoolkit:calendarextender>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CY CUT-OFF/EXFACT CUT-OFF">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCYCUTOFFEXFACTCUTOFF" Visible ="false" runat="server" Width="180px" BackColor="#ffffcc" Text='<%# Eval("CYCUTOFFEXFACTCUTOFF")%>'></asp:TextBox>
                    <asp:Label ID="lblCYCUTOFFEXFACTCUTOFF" runat="server" Text='<%# Eval("CYCUTOFFEXFACTCUTOFF") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "LOADING PORT">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtLOADINGPORT" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("LOADINGPORT")%>'></asp:TextBox>
                    <asp:Label ID="lblLOADINGPORT" runat="server" Text='<%# Eval("LOADINGPORT") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "REASON OF DELAYED EXFACT">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtREASONOFDELAYEDEXFACT" Visible ="false" runat="server" Width="180px" BackColor="#ffffcc" Text='<%# Eval("REASONOFDELAYEDEXFACT")%>'></asp:TextBox>
                    <asp:Label ID="lblREASONOFDELAYEDEXFACT" runat="server" Text='<%# Eval("REASONOFDELAYEDEXFACT") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "REASON OF DELAYED O/B">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtREASONOFDELAYEDOB" Visible ="false" runat="server" Width="180px" BackColor="#ffffcc" Text='<%# Eval("REASONOFDELAYEDOB")%>'></asp:TextBox>
                    <asp:Label ID="lblREASONOFDELAYEDOB" runat="server" Text='<%# Eval("REASONOFDELAYEDOB") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "REMARKS">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtREMARKS" Visible ="false" runat="server" Width="300px" BackColor="#ffffcc" Text='<%# Eval("REMARKS")%>'></asp:TextBox>
                    <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "WSS STATUS FLAG">
                    <HeaderStyle CssClass="nodisplay"/>
                    <ItemStyle HorizontalAlign="Center" CssClass="nodisplay"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtWSSSTATUSFLAG" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("WSSSTATUSFLAG")%>'></asp:TextBox>
                    <asp:Label ID="lblWSSSTATUSFLAG" runat="server" Text='<%# Eval("WSSSTATUSFLAG") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CREATEDBY">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCREATEDBY" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("CREATEDBY")%>'></asp:TextBox>
                    <asp:Label ID="lblCREATEDBY" runat="server" Text='<%# Eval("CREATEDBY") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "CREATEDDATE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtCREATEDDATE" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("CREATEDDATE")%>'></asp:TextBox>
                    <asp:Label ID="lblCREATEDDATE" runat="server" Text='<%# Eval("CREATEDDATE") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "UPDATEDBY">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtUPDATEDBY" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("UPDATEDBY")%>'></asp:TextBox>
                    <asp:Label ID="lblUPDATEDBY" runat="server" Text='<%# Eval("UPDATEDBY") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "UPDATEDDATE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtUPDATEDDATE" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("UPDATEDDATE")%>'></asp:TextBox>
                    <asp:Label ID="lblUPDATEDDATE" runat="server" Text='<%# Eval("UPDATEDDATE") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "COLOR DISPLAY">
                    <HeaderStyle CssClass="nodisplay"  />
                    <ItemStyle HorizontalAlign="Center" CssClass="nodisplay"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblColorDisplay" runat="server" Text='<%# Eval("COLORDISPLAY") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText = "UPDATED COLUMNS">
                    <HeaderStyle CssClass="nodisplay"  />
                    <ItemStyle HorizontalAlign="Center" CssClass="nodisplay"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblUPDATEDCOLUMNS" runat="server" Text='<%# Eval("UPDATEDCOLUMNS") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText = "TEXTCOLORDISPLAY">
                    <HeaderStyle CssClass="nodisplay"  />
                    <ItemStyle HorizontalAlign="Center" CssClass="nodisplay"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblTEXTCOLORDISPLAY" runat="server" Text='<%# Eval("TEXTCOLORDISPLAY") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "REASON OF CANCELLATION" >
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblReasonOfCancellation" runat="server" Text='<%# Eval("REASONOFCANCELLATION") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText = "REVISION NO" >
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblRevisionNo" runat="server" Text='<%# Eval("REVISIONNO") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                
                
            </Columns>
            
            
            <HeaderStyle  Font-Bold="True"  BackColor = "#C5D9F1" BorderStyle ="Solid" BorderColor ="Black" />
            <RowStyle Font-Bold ="false" BorderStyle ="Solid" BorderColor ="Black" BorderWidth = "10px" />
            
            </asp:GridView>
        
        
        
        </div>
        <div id="DivFooterRow" style="overflow:hidden; display:none">

        </div>
    </div>
    <div id="dvExport" style="display:none"  runat="server">
    <br />
        <table>
            <tr>
                <td colspan ="3" style ="height:50px"><asp:Label ID="lblExporHeader" Font-Bold="true" Font-Size="X-Large" runat="server" Text="SHIPMENT SCHEDULE"></asp:Label></td>
                
            </tr>
            <tr style ="height:20px; display:none;">
                <td colspan ="20">
                    <table id = "tbLegendExport" runat = "server" style ="font-size:x-small;color:Black">
                    <tr id = "trLegendExport" runat = "server">
                        <td>Legend : &nbsp;&nbsp;&nbsp;</td>
                        </tr>
                    </table>
                
                </td>
            </tr>
        </table>
        
        <br />
        <br />
        <asp:GridView ID="grdExport" Width="6700px" runat="server"  AutoGenerateColumns ="False" onrowdatabound="grdExport_RowDataBound" >
        
        <Columns>
            <asp:TemplateField HeaderText = "ID">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                <asp:Label ID="lblID" runat="server" Text='<%# Eval("WSSID") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText = "GNS + INVOICE NUMBER">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblGNSINVOICENUMBER" runat="server" Text='<%# Eval("GNSINVOICENUMBER") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText = "PRODUCTION LOCATION">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblPRODUCTIONLOCATION" runat="server" Text='<%# Eval("PRODUCTIONLOCATION") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "SHIPPING LOCATION">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblSHIPPINGLOCATION" runat="server" Text='<%# Eval("SHIPPINGLOCATION") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "OD NO">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblODNO" runat="server" Text='<%# Eval("ODNO") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "LOT NO">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblLOTNO" runat="server" Text='<%# Eval("LOTNO") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "SHIPMENT NO">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblSHIPMENTNO" runat="server" Text='<%# Eval("SHIPMENTNO") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "PO MONTH">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblPOMONTH" runat="server" Text='<%# Eval("POMONTH") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "PO NUMBER">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblPONUMBER" runat="server" Text='<%# Eval("PONUMBER") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CONSIGNEE PO NUMBER">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCONSIGNEEPONUMBER" runat="server" Text='<%# Eval("CONSIGNEEPONUMBER") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "PO WK">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblPOWK" runat="server" Text='<%# Eval("POWK") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "ITEM CODE">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "MODEL DESCRIPTION">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblMODELDESCRIPTION" runat="server" Text='<%# Eval("MODELDESCRIPTION") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "MODEL NAME">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblMODELNAME" runat="server" Text='<%# Eval("MODELNAME") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "PLANNED EX-FACT DATE">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblPLANNEDEXFACTDATE" runat="server" Text='<%# Eval("PLANNEDEXFACTDATE","{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "PLANNED OB DATE">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblPLANNEDOBDATE" runat="server" Text='<%# Eval("PLANNEDOBDATE","{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            
            
            <asp:TemplateField HeaderText = "QTY">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblQTY" runat="server" Text='<%# Eval("QTY") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CUSTOMER">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCUSTOMER" runat="server" Text='<%# Eval("CUSTOMER") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "SHIMPENT MOD">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblSHIMPENTMOD" runat="server" Text='<%# Eval("SHIMPENTMOD") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "PALLET TYPE">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblPALLETTYPE" runat="server" Text='<%# Eval("PALLETTYPE") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "DESTINATION">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblDESTINATION" runat="server" Text='<%# Eval("DESTINATION") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "TRADE">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:TextBox id="txtTRADE" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("TRADE")%>'></asp:TextBox>
                    <asp:Label ID="lblTRADE" runat="server" Text='<%# Eval("TRADE") %>'></asp:Label></ItemTemplate>
             </asp:TemplateField>
                
            <asp:TemplateField HeaderText = "CONTAINER QTY HC">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCONTAINERQTYHC" runat="server" Text='<%# Eval("CONTAINERQTYHC") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CONTAINER QTY 40 FT">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCONTAINERQTY40FT" runat="server" Text='<%# Eval("CONTAINERQTY40FT") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CONTAINER QTY 20 FT">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCONTAINERQTY20FT" runat="server" Text='<%# Eval("CONTAINERQTY20FT") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "NO OF PALLET">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblNOOFPALLET" runat="server" Text='<%# Eval("NOOFPALLET") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "STD CONT LOAD">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblSTDCONTLOAD" runat="server" Text='<%# Eval("STDCONTLOAD") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CONT USAGE">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCONTUSAGE" runat="server" Text='<%# Eval("CONTUSAGE") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CONT USAGE RATIO">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                <asp:TextBox id="txtCONTUSAGERATIO" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("CONTUSAGERATIO")%>'></asp:TextBox>
                <asp:Label ID="lblCONTUSAGERATIO" runat="server" Text='<%# Eval("CONTUSAGERATIO") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CAS">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCAS" runat="server" Text='<%# Eval("CAS","{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CTR NUMBER">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCTRNUMBER" runat="server" Text='<%# Eval("CTRNUMBER") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CTR NUMBER ARRIVAL DATE">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                <asp:Label ID="lblCTRNUMBERARRIVALDATE" runat="server" Text='<%# Eval("CTRNUMBERARRIVALDATE","{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CONFIRMED DATE">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCONFIRMEDDATE" runat="server" Text='<%# Eval("CONFIRMEDDATE","{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "SHIPPING LIN">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblSHIPPINGLIN" runat="server" Text='<%# Eval("SHIPPINGLIN") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "1ST VESSEL">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lbl1STVESSEL" runat="server" Text='<%# Eval("VESSEL1ST") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "2ND VESSEL">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lbl2NDVESSEL" runat="server" Text='<%# Eval("VESSEL2ND") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "VESSEL DESTINATION">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblVESSELDESTINATION" runat="server" Text='<%# Eval("VESSELDESTINATION") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "PORT OF DISCHARGE">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblPORTOFDISCHARGE" runat="server" Text='<%# Eval("PORTOFDISCHARGE") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "ETA DISCHARGE PORT">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblETADISCHARGEPORT" runat="server" Text='<%# Eval("ETADISCHARGEPORT","{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CY CUT-OFF/EXFACT CUT-OFF">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCYCUTOFFEXFACTCUTOFF" runat="server" Text='<%# Eval("CYCUTOFFEXFACTCUTOFF") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "LOADING PORT">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblLOADINGPORT" runat="server" Text='<%# Eval("LOADINGPORT") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "REASON OF DELAYED EXFACT">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblREASONOFDELAYEDEXFACT" runat="server" Text='<%# Eval("REASONOFDELAYEDEXFACT") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "REASON OF DELAYED O/B">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblREASONOFDELAYEDOB" runat="server" Text='<%# Eval("REASONOFDELAYEDOB") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "REMARKS">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "WSS STATUS FLAG">
                <HeaderStyle CssClass="nodisplay"/>
                <ItemStyle HorizontalAlign="Center" CssClass="nodisplay"></ItemStyle>
                <ItemTemplate>
                <asp:TextBox id="txtWSSSTATUSFLAG" Visible ="false" runat="server" Width="120px" BackColor="#ffffcc" Text='<%# Eval("WSSSTATUSFLAG")%>'></asp:TextBox>
                <asp:Label ID="lblWSSSTATUSFLAG" runat="server" Text='<%# Eval("WSSSTATUSFLAG") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CREATEDBY">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCREATEDBY" runat="server" Text='<%# Eval("CREATEDBY") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "CREATEDDATE">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblCREATEDDATE" runat="server" Text='<%# Eval("CREATEDDATE") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "UPDATEDBY">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate> <asp:Label ID="lblUPDATEDBY" runat="server" Text='<%# Eval("UPDATEDBY") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "UPDATEDDATE">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate><asp:Label ID="lblUPDATEDDATE" runat="server" Text='<%# Eval("UPDATEDDATE") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "COLOR DISPLAY">
                <HeaderStyle CssClass="nodisplay"  />
                <ItemStyle HorizontalAlign="Center" CssClass="nodisplay"></ItemStyle>
                <ItemTemplate>
                <asp:Label ID="lblColorDisplay" runat="server" Text='<%# Eval("COLORDISPLAY") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText = "REASON OF CANCELLATION" >
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                <asp:Label ID="lblReasonOfCancellation" runat="server" Text='<%# Eval("REASONOFCANCELLATION") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText = "REVISION NO" >
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                    <asp:Label ID="lblRevisionNo" runat="server" Text='<%# Eval("REVISIONNO") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
        </Columns>
        
        
        <HeaderStyle  Font-Bold="True"  BackColor = "#C5D9F1" BorderStyle ="Solid" BorderColor ="Black" />
        <RowStyle Font-Bold ="false" BorderStyle ="Solid" BorderColor ="Black" BorderWidth = "10px" />
        
        </asp:GridView>
    </div>
    <br />
        
    
    
       
    <br />
        
    </div>
    
    
</div>

<ajaxtoolkit:calendarextender ID="CalOBDate" runat="server" TargetControlID="txtOBDate" Format="dd MMM yyyy" PopupButtonID="txtOBDate"></ajaxtoolkit:calendarextender>
<ajaxtoolkit:calendarextender ID="CalExFactDate" runat="server" TargetControlID="txtExFactDate" Format="dd MMM yyyy" PopupButtonID="txtExFactDate"></ajaxtoolkit:calendarextender>

<ajaxtoolkit:calendarextender ID="CalOBDateTo" runat="server" TargetControlID="txtOBDateTo" Format="dd MMM yyyy" PopupButtonID="txtOBDateTo"></ajaxtoolkit:calendarextender>
<ajaxtoolkit:calendarextender ID="CalExFactDateTo" runat="server" TargetControlID="txtExFactDateTo" Format="dd MMM yyyy" PopupButtonID="txtExFactDateTo"></ajaxtoolkit:calendarextender>



<cc1:msgBox id="MsgBox1" runat="server"></cc1:msgBox>
</asp:Content>