<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Form/MasterPalletMonitoring.Master" EnableEventValidation="false" CodeBehind="PSI_SUPPLER_STOCKS_UPLOADING.aspx.cs" Inherits="FGWHSEClient.Form.PSI_SUPPLER_STOCKS_UPLOADING" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:left;width:1200px">
<div style="margin-left:10px;">
    <br />
    <span>SUPPLIER STOCKS MANUAL UPLOAD</span>

    <br />
    <br />
    
    
 </div>
<div style="margin-left:10px">
    <table>
        <tr>
            <td>SUPPLIER :</td>
            <td>
                <asp:DropDownList ID="ddSupplier" runat="server"></asp:DropDownList>
            </td>
            
            <td>&nbsp;&nbsp;&nbsp;</td>
            <td><asp:Button ID="btnDeleteAll" runat="server" Text="DELETE SUPPLIER STOCKS" Width="227px" OnClick="btnDeleteAll_Click" /></td>

        </tr>
        <tr>
            <td>MATERIAL CODE:</td>
            <td>
                <asp:TextBox ID="txtMATERIALCODE" runat="server" Width="202px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="SEARCH" OnClick="btnSearch_Click" />
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;</td>
                        <td>
                            <asp:Button ID="btnExport" runat="server" Text="EXPORT" OnClick="btnExport_Click" />
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
        </table>
    <br />
        <div>

            <asp:GridView ID="grdParts" runat="server" AutoGenerateColumns ="false" AllowPaging ="true" PageSize ="12" OnPageIndexChanging="grdParts_PageIndexChanging" OnRowDataBound="grdParts_RowDataBound"  >
                <HeaderStyle BackColor =  "Blue"  ForeColor ="White" Width ="400px" />
            <Columns>
                

                <asp:TemplateField HeaderStyle-Width="150px" HeaderText="MATERIALCODE"  ItemStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:Label ID="lblMATERIALCODE" runat="server" Text='<%#Bind("ITEMCODE") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="300px" HeaderText="DESCRIPTION"  ItemStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDESCRIPTION" runat="server" Text='<%#Bind("DESCRIPTION") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="200px" HeaderText="UPDATED DATE"  ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lblUPDATEDDATE" runat="server" Text='<%#Bind("LastUpdate") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="70px" HeaderText="QTY"  ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label ID="lblQTY" runat="server" Text='<%#Bind("QTY") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="300px" HeaderText="PROBLEM CATEGORY"  ItemStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lblProblemCat" runat="server" Text='<%#Bind("Problem_Cat") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="300px" HeaderText="REASON"  ItemStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lblReason" runat="server" Text='<%#Bind("Reason") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="300px" HeaderText="PIC"  ItemStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lblPIC" runat="server" Text='<%#Bind("PIC") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="300px" HeaderText="COUNTERMEASURE"  ItemStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lblCounterMeasure" runat="server" Text='<%#Bind("CounterMeasure") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                

                <asp:TemplateField HeaderStyle-Width="300px" HeaderText="SUPPLIER NAME"  ItemStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lblSUPPLIERNAME" runat="server" Text='<%#Bind("SUPPLIERNAME") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="70px" HeaderText="UPDATE"  ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" Text="UPDATE" OnClick ="lnk_update"></asp:LinkButton>
               
                                </ItemTemplate>
                 </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="70px" HeaderText="DELETE"  ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDELETE" runat="server" Text="DELETE" OnClick ="lnk_delete"></asp:LinkButton>
               
                                </ItemTemplate>
                 </asp:TemplateField>


                
                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="SUPPLIERID"  ItemStyle-Width="500px">
                    <HeaderStyle CssClass ="NoDisplay" />
                    <ItemStyle CssClass="NoDisplay" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSUPPLIERID" runat="server" Text='<%#Bind("SUPPLIERID") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>


                
            </Columns>
            </asp:GridView>
        </div>
        <br />
        <table>
             <tr>
                <td>File Upload</td>
                <td>
                    <table>
                        <tr>
                            <td>&nbsp;&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Height="22px" /></td>
                            <td>&nbsp;<asp:Button ID="btnUpload" runat="server" Text="UPLOAD" OnClick="btnUpload_Click"/></td>
                            <td>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkTemplate" runat="server" OnClick="lnkTemplate_Click">TEMPLATE</asp:LinkButton></td>
                            
                        </tr>

                    </table>

                </td>
                               
              </tr>

        </table>
</div>
    
</div>

<div class="modal fade" id="myModaL" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
   <div class="modal-dialog">
        <div class="modal-content">
             <div class="modal-header">
                
                   <asp:Panel ID="pnlItemCode" runat="server" CssClass="modalPopup" align="center" style="display:compact" Width="400px" BackColor="#EAE9EB" Height="500px">
                    <br />
                    <br />
                    <div style ="text-align:left">
                      <table>
                            <tr>
                                <td>SUPPLIER ID:</td>
                                <td>&nbsp;</td>
                                <td><asp:Label ID="lblPOPSupplier" runat="server" Text=""></asp:Label></td>
                            </tr>
                             <tr>
                                <td>Item Code:</td>
                                <td>&nbsp;</td>
                                <td><asp:Label ID="lblPOPMaterial" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>QTY :</td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="txtQty" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Problem Category:</td>
                                <td>&nbsp;</td>
                                <td><asp:DropDownList ID="ddProblemCat" runat="server" Height="25px" Width="237px" onchange="selectPIC(this);"></asp:DropDownList></td>
                
                            </tr>
                            <tr>
                                <td>Reason:</td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="txtReason" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>PIC:</td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="txtPIC" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Countermeasure:</td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="txtCountermeasure" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr><td><br /></td></tr>
                            <tr>
                                <td></td>
                                <td colspan="2">
                                    <table>
                        
                                        <tr>
                                            <td><asp:Button ID="btnSave" runat="server" Text="SAVE" Width="83px" OnClick="btnSave_Click" /></td>
                                            <td>&nbsp;&nbsp;</td>
                                            <td><asp:Button ID="btnCancel" runat="server" Text="CANCEL" Width="83px" /></td>
                                        </tr>

                                    </table>


                                </td>
                            </tr>
                    </table>      
                    </div>
                  </asp:Panel>

            </div>
       </div>
   </div>
</div>
    <cc1:msgBox ID="msgBox" runat="server" />

    <div style ="display:none">
     <button type="button" class="btn nk-indigo btn-info" data-toggle="modal" data-target="#myModaL" id="btnModal">Indigo</button>
    <asp:Label ID="lblMaterialHidden" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblSUPPLIERIDHidden" runat="server" Text=""></asp:Label>
    <asp:TextBox ID="txtMaterialHidden" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtSUPPLIERIDHidden" runat="server"></asp:TextBox>
    <asp:Label ID="lblFileTemp" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblUID" runat="server" Text=""></asp:Label>
</div>


<script type="text/javascript">

    function chkBox() {
        //chk1 = document.getElementById('ctl00_ContentPlaceHolder1_chkDelCat_0');
        //chk2 = document.getElementById('ctl00_ContentPlaceHolder1_chkDelCat_1');
        var selectedIndex = checkboxes.index(checkboxes.find(':checked'));
        alert(selectedIndex);

    }
    function showModal(materialcode, description, qty, problemcat, reason, pic, countermeasure, suppliername, supplierid) {


        document.getElementById('ctl00_ContentPlaceHolder1_txtMaterialHidden').value = materialcode;
        document.getElementById('ctl00_ContentPlaceHolder1_txtSUPPLIERIDHidden').value = supplierid;

        document.getElementById('ctl00_ContentPlaceHolder1_lblPOPSupplier').innerHTML = suppliername;
        document.getElementById('ctl00_ContentPlaceHolder1_lblPOPMaterial').innerHTML = description;


        document.getElementById('ctl00_ContentPlaceHolder1_txtQty').value = qty;
        var prob = document.getElementById('ctl00_ContentPlaceHolder1_ddProblemCat'); problemcat;
        if (problemcat == "") {
            prob.options[prob.selectedIndex].value = problemcat;
        }
        else {
            prob.options[prob.selectedIndex].text = problemcat;
        }

        document.getElementById('ctl00_ContentPlaceHolder1_txtReason').value = reason;
        document.getElementById('ctl00_ContentPlaceHolder1_txtPIC').value = pic;
        document.getElementById('ctl00_ContentPlaceHolder1_txtCountermeasure').value = countermeasure;
        document.getElementById('btnModal').click();
    }

    function dismissModal(sel) {
        document.getElementById('btnDismissModal').click();
    }

    function selectPIC(sel) {
        var value = sel.options[sel.selectedIndex].value;

        var text = sel.options[sel.selectedIndex].text;
        document.getElementById('ctl00_ContentPlaceHolder1_txtPIC').value = value;
    
    }

    
    </script>
</asp:Content>