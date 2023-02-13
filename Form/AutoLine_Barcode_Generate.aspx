<%@ Page Language="C#"  MasterPageFile="~/Form/MasterPalletMonitoring.Master"  AutoEventWireup="true" CodeBehind="AutoLine_Barcode_Generate.aspx.cs" Inherits="FGWHSEClient.Form.AutoLine_Barcode_Generate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript" >
    function toUpperText(e) {

        var txt = document.getElementById(e).value;
        document.getElementById(e).value = txt.toUpperCase();
    }

    function ifChecked(e) {

        var isChecked = document.getElementById(e).checked;
   
  
        if (isChecked == true) {

            if (e == "ctl00_ContentPlaceHolder1_chkLine") {
                document.getElementById("ctl00_ContentPlaceHolder1_chkMold").checked = false;
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_chkLine").checked = false;
            }
        }
        else {

            if (e == "ctl00_ContentPlaceHolder1_chkLine") {
                document.getElementById("ctl00_ContentPlaceHolder1_chkMold").checked = true;
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_chkLine").checked = true;
            }
        }
      
       document.getElementById('<%= "ctl00_ContentPlaceHolder1_btnCheck" %>').click();
    }
        
</script>

<div style="text-align:left;width:1200px">
    
     <div style="margin-left:10px">
         <br />
            <span>AUTO-LINE BARCODE LABEL CREATION</span>
        <br />
        <br />

         <div style="text-align:left">
            <table cellspacing ="5">
                <tr>
                    <td style ="text-align:right;vertical-align:top">PRODUCT :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="ddFACTORY" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddFACTORY_SelectedIndexChanged"></asp:DropDownList><font color="red">&nbsp; *</font>
                    </td>

                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style ="text-align:right;vertical-align:top">
                        ITEM CODE :
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
                                </td>
                                <td id="tdItem" runat="server" valign="top">
                                    &nbsp;
                                    <asp:Button ID="btnItemCode" runat="server" Text="***" Width="32px"  />
                                </td>
                                <td><font color="red">&nbsp; **</font></td>
                            </tr>
                        </table>
                        
                        
                    </td>

                </tr>

                <tr>
                    <td style ="text-align:right;vertical-align:top">SUPPLIER :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td >
                        <asp:TextBox ID="txtSupplier" runat="server" Width="80px" onkeyup ="toUpperText(this.id)" MaxLength="8"></asp:TextBox>
                        <font color="red">&nbsp; *</font>
                    </td>

                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style ="text-align:right;vertical-align:top">
                        PRODUCTION DATE :
                    </td>
                    <td>
                        <asp:TextBox ID="txtProdDate" runat="server"></asp:TextBox><font color="red">&nbsp; **</font>

                    </td>
                </tr>
                

                <tr>
                    <td style ="text-align:right;vertical-align:top">PART CODE :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtPartCode" runat="server" MaxLength="15" AutoPostBack="True" OnTextChanged="txtPartCode_TextChanged"></asp:TextBox>
                        <font color="red">&nbsp; *</font>
                        <asp:CheckBox ID="chkValidate" TextAlign ="RIGHT" runat="server" Checked="true" AutoPostBack="True" OnCheckedChanged="chkValidate_CheckedChanged"/><asp:Label ID="Label1" runat="server" Text="Validate in GNS+" Font-Bold ="false"></asp:Label>
                    </td>

                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style ="text-align:right;vertical-align:top">
                        <asp:CheckBox ID="chkLine" runat="server" Checked="true" onclick ="ifChecked(this.id)" /><asp:Label ID="Label3" runat="server" Text="LINE"></asp:Label>
                        <asp:CheckBox ID="chkMold" runat="server" onclick ="ifChecked(this.id)" /><asp:Label ID="Label4" runat="server" Text="MOLD - CAVITY" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddLine" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="ddMold" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="ddCavity" runat="server"></asp:DropDownList>
                        <font color="red">&nbsp; **</font>
                    </td>
                </tr>

                <tr>
                    <td style ="text-align:right;vertical-align:top">PARTS NAME :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtPartsDesc" runat="server"></asp:TextBox>
                    </td>


                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style ="text-align:right;vertical-align:top">
                        SHIFT :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddShift" runat="server"></asp:DropDownList><font color="red">&nbsp; **</font>
                    </td>
                </tr>

                

                <tr>
                    <td style ="text-align:right;vertical-align:top">QUANTITY :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td style ="vertical-align:top">
                        <asp:TextBox ID="txtQTY" runat="server" Width="50px"></asp:TextBox><font color="red">&nbsp; *</font>
                        &nbsp;&nbsp;&nbsp;&nbsp
                        <asp:Label ID="Label6" runat="server" Text="Std. Pkg. Qty.:"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtPQTY" runat="server" Width="50px"></asp:TextBox><font color="red">&nbsp; *</font>
                    </td>

                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style ="text-align:right;vertical-align:top">
                        START SERIES :
                    </td>
                    <td>
                        <asp:TextBox ID="txtSeries" runat="server" Width="45px" MaxLength="4"></asp:TextBox>
                         <font color="red">&nbsp; **</font>
                        <asp:CheckBox ID="chkAutoSeries" TextAlign ="RIGHT" runat="server" /><asp:Label ID="Label5" runat="server" Text="w/ Auto Series" Font-Bold ="false"></asp:Label>
                    </td>
                </tr>


                


                <tr>
                    <td style ="text-align:right;vertical-align:top">LOT NO :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtLotNo" runat="server" MaxLength="30"></asp:TextBox>
                        <font color="red">&nbsp; **</font>
                        <asp:CheckBox ID="chkGen" TextAlign ="RIGHT" runat="server" AutoPostBack="True" OnCheckedChanged="chkGen_CheckedChanged"/><asp:Label ID="Label2" runat="server" Text="Auto Generate" Font-Bold ="false"></asp:Label>
                    </td>

                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style ="text-align:right;vertical-align:top;display:none">
                        COLOR :
                    </td>
                    <td style="display:none">
                        <asp:DropDownList ID="ddColor" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style ="text-align:right;vertical-align:top">REMARKS :</td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                       <asp:TextBox ID="txtRemarks" runat="server" MaxLength="65"></asp:TextBox>
                    </td>


                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style ="text-align:right;vertical-align:top">
                        SPECS :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddSpecs" runat="server"></asp:DropDownList>
                        <font color="red">&nbsp; **</font>
                        <asp:CheckBox ID="chkWSpecs" runat="server" AutoPostBack="True" OnCheckedChanged="chkWSpecs_CheckedChanged" /><asp:Label ID="Label7" runat="server" Text="w/ Specs"></asp:Label>
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


                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style ="text-align:right;vertical-align:top">
                        COLOR :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddSpecsColor" runat="server"></asp:DropDownList>
                        <font color="red">&nbsp; **</font>
                    </td>
                </tr>
               


            </table>

            <br />
        </div>
     </div>
</div>
<div style ="display:none">
    <asp:Button ID="btnCheck" runat="server" Text="Button" OnClick="btnCheck_Click" />

</div>
    <asp:Panel ID= "pnlpopup" runat="server" CssClass="Panel" style="display:none">
    
  <div id="div cose" class="divclose" >
             <asp:ImageButton ID="btnCancel" runat="server" 
             ImageUrl="~/image/button close.png" />
             
     </div>
     
<asp:Panel ID = "pnlItemCode" runat ="server" CssClass="PanelInside">
    
 <div id="PanelTitle" runat="server" class="PanelTitle">
         <asp:Label ID="lblErrorMessage" runat="server" Text="Label" Visible="true" Font-Size= "Large"></asp:Label>
</div>
         <asp:GridView ID="grdItemCode" runat="server" AutoGenerateColumns="False" 
              Font-Bold="False" Font-Names="Calibri" Font-Size="Medium" ForeColor="Black" 
             onselectedindexchanged="grdItemCode_SelectedIndexChanged" 
             onselectedindexchanging="grdItemCode_SelectedIndexChanging" Width = "265px">
             <RowStyle BackColor="#F7F7DE" />
             <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
             <Columns>
                 <asp:CommandField ButtonType="Image" SelectImageUrl="~/image/Select.gif" ShowSelectButton="True" ItemStyle-Width="25px" />
                 <asp:BoundField DataField="itemcode" HeaderText="Item Code" ItemStyle-Width="70px"></asp:BoundField>
                 <asp:BoundField DataField="partname" HeaderText="Part Name" ItemStyle-Width="150px"> </asp:BoundField>
             </Columns>
             <AlternatingRowStyle BackColor="White" />
         </asp:GridView>
</asp:Panel>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
            
	        targetcontrolid="btnItemCode" 
	        cancelcontrolid="btnCancel" 
	        popupcontrolid="pnlpopup" 
	        backgroundcssclass="ModalPopupBG" 
	        Y="150" 
            RepositionMode="RepositionOnWindowResize"
            >
</ajaxToolkit:ModalPopupExtender>
<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
