<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" CodeBehind="HTMenu.aspx.cs" Inherits="FGWHSEClient.Form.HTMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblHeader" runat="server"></asp:Label>
    <div>
        <br />
        <center>
            <link href="../App_Themes/Stylesheet/DENSO.css" rel="stylesheet" type="text/css" />
            <table id="tbPartWhse" runat="server">

                <tr id="trPartWhse" runat="server">
                    <td style="background-color: #715098; color: white; height: 30px; border-color: #660066; border-style: solid; border-width: thin"
                        bgcolor="#660066">PARTS WHSE</td>
                </tr>
                <tr id="trLotDataScanning" runat="server">
                    <td>
                        <asp:Button ID="btnLotDataScanning" runat="server" Text="LOT DATA SCANNING" CssClass="partsWHButton" OnClick="btnLotDataScanning_Click" /></td>
                </tr>

                <%--<tr id = "trLotDataScanningBypass" runat ="server" >
                <td><asp:Button ID="btnLotDataScanningBypass" runat="server" Text="LOT DATA SCANNING (BYPASS)" CssClass="partsWHButton" onclick="btnLotDataScanningBypass_Click" /></td>
            </tr>--%>

                <tr id="trDNReceiving" runat="server">
                    <td>
                        <asp:Button ID="btnDNReceiving" runat="server" Text="PARTS LOCATION CHECK"
                            CssClass="partsWHButton" OnClick="btnDNReceiving_Click" /></td>
                </tr>

                 <tr id="trDNReceiving2" runat="server">
                    <td>
                        <asp:Button ID="btnDNReceiving2" runat="server" Text="PARTS LOCATION CHECK v2"
                            CssClass="partsWHButton" OnClick="btnDNReceiving2_Click" /></td>
                </tr>
                <tr id="trPartsInspectionLocationCheck" runat="server">
                    <td>
                        <asp:Button ID="btnPartsInpectionLocationCheck" runat="server" Text="PARTS LOCATION INSPECTION CHECK"
                            CssClass="partsWHButton" OnClick="btnPartsInpectionLocationCheck_Click" /></td>
                </tr>
                <tr id="trRFIDReceiving" runat="server">
                    <td>
                        <asp:Button ID="btnRFIDReceiving" runat="server" Text="RFID RECEIVING"
                            CssClass="partsWHButton" OnClick="btnRFIDReceiving_Click" /></td>
                </tr>
                <tr id="trRFIDDNDelete" runat="server">
                    <td>
                        <asp:Button ID="btnRFIDDNDelete" runat="server" Text="RFID DN DELETE"
                            CssClass="partsWHButton" OnClick="btnRFIDDNDelete_Click" /></td>
                </tr>
                <tr id="trReceivePD" runat="server">
                    <td>
                        <asp:Button ID="btnReceive" runat="server" Text="RECEIVE PD" CssClass="partsWHButton" OnClick="btnReceive_Click"/></td>
                </tr>
                <tr id="trDelivery" runat="server">
                    <td>
                        <asp:Button ID="btnDelivery" runat="server" Text="DELIVERY" CssClass="partsWHButton" OnClick="btnDelivery_Click"/></td>
                </tr>
                <tr id="trExit" runat="server">
                    <td>
                        <asp:Button ID="btnExit" runat="server" Text="EXIT" CssClass="partsWHButton" /></td>
                </tr>

            </table>






            <table id="tbFGWHSE" runat="server">
                <tr id="trFGWHSE" runat="server">
                    <td style="background-color: #8AAE3F; color: white; height: 30px; border-color: #4F8118; border-style: solid; border-width: thin">FG WHSE</td>
                </tr>
                <tr id="trContainerAllocation" runat="server">
                    <td>
                        <asp:Button ID="btnContainerAllocation" runat="server" Text="CONTAINER ALLOCATION" CssClass="FGWHButton" OnClick="btnContainerAllocation_Click" /></td>
                </tr>

                <tr id="trPalletAllocation" runat="server">
                    <td>
                        <asp:Button ID="btnPalletAllocation" runat="server" Text="PALLET ALLOCATION" CssClass="FGWHButton" OnClick="btnPalletAllocation_Click" /></td>
                </tr>
                <tr id="trPalletPicking" runat="server" style="display:none">
                    <td><asp:Button ID="btnPalletPicking" runat="server" Text="PALLET PICKING" CssClass ="FGWHButton"  onkeyup="HTbeep(event)" onclick="btnPalletPicking_Click" /></td>
                </tr>
                <tr id="trODAllocation" runat="server">
                    <td>
                        <asp:Button ID="btnODAllocation" runat="server" Text="OD ALLOCATION" CssClass="FGWHButton" OnClick="btnODAllocation_Click" /></td>
                </tr>
                <tr id="trPalletLoading" runat="server">
                    <td>
                        <asp:Button ID="btnPalletLoading" runat="server" Text="PALLET LOADING" CssClass="FGWHButton" OnClick="btnPalletLoading_Click" /></td>
                </tr>
                <tr id="trPalletShipmentLoading" runat="server" style="display:none">
                    <td><asp:Button ID="btnPalletShipmentLoading"  runat="server" 
                            Text="PALLET SHIPMENT LOADING" CssClass ="FGWHButton" 
                            onclick="btnPalletShipmentLoading_Click" /></td>
                </tr>
                <tr id="trExitFactory" runat="server">
                    <td>
                        <asp:Button ID="btnExitFactory" runat="server" Text="EXIT FACTORY" CssClass="FGWHButton" OnClick="btnExitFactory_Click" /></td>
                </tr>
                <tr id="trCartonInformation" runat="server">
                    <td>
                        <asp:Button ID="btnCartonInformation" runat="server" Text="CARTON INFORMATION" CssClass="FGWHButton" OnClick="btnCartonInformation_Click" /></td>
                </tr>

                <tr id="trVanPoolAllocation" runat="server" style="display:none">
                    <td><asp:Button ID="btnVanPoolAllocation" runat="server" Text="VAN POOL ALLOCATION" 
                            CssClass ="FGWHButton" onclick="btnVanPoolAllocation_Click" /></td>
                </tr>


                
            </table>
            <%--PRECIOUS LOUISSE DEL PILAR 5/4/2021--%>


            <%--MELVIN MACARAIG 2/4/2022--%>
            <table id="tblPPD" runat="server">
                <tr id="tr2" runat="server">
                    <td style="background-color: #ED7D31; color: white; height: 30px; border-color: #833C0C; border-style: solid; border-width: thin">PPD WHSE</td>
                </tr>

                <tr id="trPPDPLC" runat="server">
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="PARTS LOCATION CHECK" CssClass="FGWHButton" OnClick="btnPPDPLC_Click" BackColor="#F8CBAD" /></td>
                </tr>

                <tr id="trPPDReturn" runat="server">
                    <td>
                        <asp:Button ID="btnPartsReturn" runat="server" Text="HOLMES PARTS RETURN" CssClass="FGWHButton" OnClick="btnPartsReturn_Click" BackColor="#F8CBAD" /></td>
                </tr>

                <tr id="trPPDDelivery" runat="server">
                    <td>
                        <asp:Button ID="btnPayout" runat="server" Text="PAYOUT / DELIVERY" CssClass="FGWHButton" OnClick="btnPayout_Click" BackColor="#F8CBAD" /></td>
                </tr>

                 

                
            </table>



      




    
    <table id ="tblPair" runat ="server" >
            <tr id = "trPairingHead" runat ="server">
                <td style ="background-color:#4784f5; color:white; height:30px; border-color:#004fe3; border-style:solid; border-width:thin" >RFID PAIRING</td>
            </tr>
            <tr id="trLotRFID" runat="server">
                <td><asp:Button ID="btnLotRfidPairing" runat="server" Text="LOT-RFID PAIRING" 
                        CssClass ="FGWHButton" BackColor="#9BBCFA" onclick="btnLotRfidPairing_Click" 
                        /></td>
            </tr>
            
            <tr id="trMultiLotRFID" runat="server">
                <td><asp:Button ID="btnMultiLotRFID" runat="server" Text="MULTI LOT-RFID PAIRING" 
                        CssClass ="FGWHButton" BackColor="#9BBCFA" OnClick="btnMultiLotRFID_Click" /></td>
            </tr>
            <tr id="trPPLotRFID" runat="server">
                <td><asp:Button ID="btnPPLotRfid" runat="server" Text="PPD UPDATE LOT-RFID PAIRING" 
                        CssClass ="FGWHButton" BackColor="#9BBCFA" 
                        onclick="btnPPLotRfid_Click"/></td>
            </tr>


            
            <tr id="trDeleteLotRFID" runat="server">
                <td><asp:Button ID="btnPairedLotRfidDeletion" runat="server" 
                        Text="PAIRED LOT RFID DELETION" CssClass ="FGWHButton" BackColor="#9BBCFA" onclick="btnPairedLotRfidDeletion_Click" 
                        /></td>
            </tr>
            
    </table>
    

        </center>
        <br />
    </div>
    <cc1:msgBox ID="MsgBox1" runat="server"></cc1:msgBox>
</asp:Content>
