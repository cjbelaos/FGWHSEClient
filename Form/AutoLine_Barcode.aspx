<%@ Page Language="C#"  MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="AutoLine_Barcode.aspx.cs" Inherits="FGWHSEClient.Form.AutoLine_Barcode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" >
       function remainingQTY() {
           var origQTY = document.getElementById("ctl00_ContentPlaceHolder1_lblOrigQTY").innerHTML;
           var KittedQTY = document.getElementById("ctl00_ContentPlaceHolder1_txtKittedQTY").value;
           var forPrint = document.getElementById("ctl00_ContentPlaceHolder1_txtForPrint").value;

           var remQTY = origQTY - (KittedQTY * forPrint)

           if (forPrint == "") {
               document.getElementById("ctl00_ContentPlaceHolder1_lblRemainingQTY").innerHTML = "";
               document.getElementById("ctl00_ContentPlaceHolder1_txtRemainingQTY").value = "";
           }
           else {
               document.getElementById("ctl00_ContentPlaceHolder1_txtRemainingQTY").value = remQTY
               document.getElementById("ctl00_ContentPlaceHolder1_lblRemainingQTY").innerHTML = remQTY
           }
       }
   </script>
    <div style="text-align:left;width:1200px">
    <div style="margin-left:10px">
        <br />
            <span>AUTO-LINE BARCODE LABEL</span>
        <br />
        <br />

        <div style="text-align:left">
            <table cellspacing ="5">
                <tr>
                    <td style ="text-align:right">
                        Tag :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:Label ID="lblRFIDTag" Font-Bold ="false" ForeColor ="Black"  runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="text-align:right;vertical-align:top">REFERENCE NO :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtRefno" runat="server" Width="197px" OnTextChanged="txtRefno_TextChanged" AutoPostBack="True"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td style ="text-align:right;vertical-align:top">PART CODE :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:Label ID="lblPartCode" Font-Bold ="false" ForeColor ="Black"  runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style ="text-align:right;vertical-align:top">PARTS DESCRIPTION :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:Label ID="lblPartsDesc" Font-Bold ="false" ForeColor ="Black"  runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style ="text-align:right;vertical-align:top">LOT NO :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:Label ID="lblLotNo" Font-Bold ="false" ForeColor ="Black"  runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style ="text-align:right;vertical-align:top">LOT LABEL ORIG. QTY :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:Label ID="lblOrigQTY" Font-Bold ="false" ForeColor ="Black"  runat="server" Text=""></asp:Label>
                    </td>
                </tr>


                <tr>
                    <td style ="text-align:right;vertical-align:top">REMARKS :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:Label ID="lblRemarks" Font-Bold ="false" ForeColor ="Black"  runat="server" Text=""></asp:Label>
                    </td>
                </tr>


                <tr>
                    <td style ="text-align:right;vertical-align:top">KITTED QTY :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtKittedQTY" runat="server" Width="80px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td style ="text-align:right;vertical-align:top">FOR PRINT :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtForPrint" runat="server" Width="79px" onkeyup="remainingQTY()"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td style ="text-align:right;vertical-align:top">REMAINING QTY :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:Label ID="lblRemainingQTY" Font-Bold ="false" ForeColor ="Black"  runat="server" Text=""></asp:Label>
                        <div style="display:none">
                            <asp:TextBox ID="txtRemainingQTY" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtArea" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>


                <tr>
                    <td style ="text-align:right"></td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:Button ID="btnGenerate" runat="server" Text="GENERATE" Width="87px" OnClick="btnGenerate_Click" UseSubmitBehavior="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="CLEAR" Width="87px" UseSubmitBehavior="false" OnClick="btnClear_Click"/>
                    </td>
                </tr>


            </table>

            <br />
        </div>

    </div>
</div>

<cc1:msgbox id="msgBox" runat="server"></cc1:msgbox>
</asp:Content>
