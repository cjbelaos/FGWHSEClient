<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Form/MasterPalletMonitoring.Master" CodeBehind="PSI_MASTER_PARTS_VENDOR.aspx.cs" Inherits="FGWHSEClient.Form.PSI_MASTER_PARTS_VENDOR" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style ="text-align:left; margin-left:5px">
    <table>
        <tr>
            <td>Vendor Code (6 digit)</td>
            <td><asp:Label ID="lblVendorCode" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Vendor Name</td>
            <td><asp:Label ID="lblVendorName" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Material Code</td>
            <td><asp:TextBox ID="txtMaterial" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
           <td></td>
            
            <td><asp:Button ID="btnSearch" runat="server" Text="SEARCH" Height="30px" OnClick="btnSearch_Click"  /></td>
        </tr>
        
    </table>
    <br />
    <br />
        <asp:Label ID="lblPartsList" runat="server" Text="PARTS LIST"></asp:Label>
     <br />
     <div style ="overflow:auto;height:500px;width:1175px">

         <asp:GridView ID="grdParts" runat="server" AutoGenerateColumns ="false" AllowPaging ="true" PageSize ="12" OnPageIndexChanging="grdParts_PageIndexChanging" OnRowDataBound="grdParts_RowDataBound" >
                <HeaderStyle BackColor =  "Blue"  ForeColor ="White" Width ="30000px" />
            <Columns>
                

                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="MATERIALCODE"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblMATERIALCODE" runat="server" Text='<%#Bind("MATERIALCODE") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="DESCRIPTION"  ItemStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDESCRIPTION" runat="server" Text='<%#Bind("DESCRIPTION") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="SUPPLIERNAME"  ItemStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:Label ID="lblSUPPLIERNAME" runat="server" Text='<%#Bind("SUPPLIERNAME") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="DELIVERY CATEGORY"  ItemStyle-Width="500px">
                    <HeaderStyle CssClass ="NoDisplay" />
                    <ItemStyle CssClass="NoDisplay" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDELIVERYCATEGORY" runat="server" Text='<%#Bind("DELIVERYCATEGORY") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                
                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="DELIVERY CATEGORY"  ItemStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDLVRY" runat="server" Text='<%#Bind("DLVRY") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="PARTSIZE"  ItemStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:Label ID="lblPARTSIZE" runat="server" Text='<%#Bind("PARTSIZE") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="DOSLEVEL"  ItemStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDOSLEVEL" runat="server" Text='<%#Bind("DOSLEVEL") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="SPQ"  ItemStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:Label ID="lblSPQ" runat="server" Text='<%#Bind("SPQ") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                

                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="LASTMAINDATE"  ItemStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:Label ID="lblLASTMAINDATE" runat="server" Text='<%#Bind("LASTMAINDATE") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>                


                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="UPDATE"  ItemStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkUPDATE" runat="server"  Text="UPDATE"  OnClick ="lnk_Click" ></asp:LinkButton>
               
                                </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="DELETE"  ItemStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDELETE" runat="server" Text="DELETE" OnClick ="lnk_delete"></asp:LinkButton>
               
                                </ItemTemplate>
                 </asp:TemplateField>



                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="MODEL"  ItemStyle-Width="500px">
                    <HeaderStyle CssClass ="NoDisplay" />
                    <ItemStyle CssClass="NoDisplay" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMODEL" runat="server" Text='<%#Bind("MODELCODE") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="500px" HeaderText="CATEGORY"  ItemStyle-Width="500px">
                    <HeaderStyle CssClass ="NoDisplay" />
                    <ItemStyle CssClass="NoDisplay" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCATEGORY" runat="server" Text='<%#Bind("CATEGORY") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

            </Columns>
            </asp:GridView>

     </div>
    <div>
        <br />

        <br />

        <table>
             <tr>
                <td>File Upload</td>
                <td>
                    <table>
                        <tr>
                            <td>&nbsp;&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" /></td>
                            <td>&nbsp;<asp:Button ID="btnUpload" runat="server" Text="UPLOAD" OnClick="btnUpload_Click" /></td>
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
                <div style ="text-align:left">
                   <asp:Panel ID="pnlItemCode" runat="server" CssClass="modalPopup" align="center" style="display:compact" Width="400px" BackColor="#EAE9EB" Height="500px">
                       <br />
                       <br />
                       <table>
                          <tr>
                              <td>
                                  Vendor Code (6 digit) :
                              </td>
                              <td> 
                                  <asp:Label ID="lblPVendorCode" runat="server" Text=""></asp:Label>
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  Vendor Name :
                              </td>
                              <td> 
                                  <asp:Label ID="lblPVendorName" runat="server" Text=""></asp:Label>
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <br />
                              </td>
                          </tr>
                          <tr>
                              <td>Delivery Category</td>
                              <td>  <%--<asp:CheckBoxList ID="chkDelCat" runat="server" RepeatDirection ="Horizontal" >
                                        <asp:ListItem Text ="Kanban Based" Value ="1" >
                                        </asp:ListItem>
                                      <asp:ListItem Text ="Delivery Based" Value ="0" >
                                        </asp:ListItem>
                                  </asp:CheckBoxList>--%>
                                  <asp:RadioButtonList ID="rdb" runat="server" RepeatDirection="Horizontal" >
                                      <asp:ListItem Text ="Kanban Based" Value ="1" ></asp:ListItem>
                                      <asp:ListItem Text ="Delivery Based" Value ="0" ></asp:ListItem>

                                  </asp:RadioButtonList>
                              
                              </td>
                          </tr>
                          <tr>
                              <td><br /></td>

                          </tr>


                          <tr>
                              <td>Parts Code (9 Digit)</td>
                              <td>
                                  <asp:TextBox ID="txtPartCode" runat="server"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td>Parts Description</td>
                              <td>
                                  <asp:TextBox ID="txtDescription" runat="server"  ReadOnly="true"></asp:TextBox>
                              </td>
                          </tr>

                           <tr>
                              <td>Product Category</td>
                              <td>
                                  <asp:TextBox ID="txtPCat" runat="server" ></asp:TextBox>
                              </td>
                          </tr>
                           <tr>
                              <td>Model</td>
                              <td>
                                  <asp:TextBox ID="txtModel" runat="server" ></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td>SPQ</td>
                              <td>
                                  <asp:TextBox ID="txtSPQ" runat="server"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td>Parts Size</td>
                              <td>
                                  <asp:TextBox ID="txtPArtSize" runat="server"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td>DOS Level</td>
                              <td>
                                  <asp:TextBox ID="txtDOS" runat="server"></asp:TextBox>
                              </td>
                          </tr>
                           <tr>
                               <td><br /></td>
                           </tr>
                           <%--<tr>
                               <td>File Upload</td>
                               <td>
                                   <table>
                                       <tr>
                                           <td><asp:FileUpload ID="FileUpload1" runat="server" /></td>
                                           <td><asp:Button ID="btnUpload" runat="server" Text="UPLOAD" /></td>
                                       </tr>

                                   </table>

                               </td>
                               
                           </tr>--%>
                           <tr>
                               <td><br /></td>
                           </tr>
                           <tr>
                               <td colspan ="2">
                                   <div style="margin-left:5px">
                                    <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" OnClick="btnUpdate_Click" />&nbsp;
                                    &nbsp;
                                    <asp:Button ID="btnCanceL" runat="server" Text="CANCEL" />
                                    </div>
                               </td>
                           </tr>

                      </table>
                       
                </asp:Panel>
                </div>
            </div>
       </div>
   </div>
</div>
<div style ="display:none">
    <button type="button" class="btn nk-indigo btn-info" data-toggle="modal" data-target="#myModaL" id="btnModal">Indigo</button>
    <button type="button" class="btn btn-default" data-dismiss="modal" id="btnDismissModal">CANCEL</button>
    <asp:Button ID="btnUpdateList" runat="server" Text="Button"/>
    <asp:Label ID="lblMaterialDelete" runat="server" Text=""></asp:Label>
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
        function showModal(vendorcode, vendorname, delcat, partcode, partdesc, spq, partsize, DOS, model, category) {
            
         
        
            document.getElementById('ctl00_ContentPlaceHolder1_lblPVendorCode').innerHTML = vendorcode;
            document.getElementById('ctl00_ContentPlaceHolder1_lblPVendorName').innerHTML = vendorname;
            if (delcat == "0") {
                document.getElementById('ctl00_ContentPlaceHolder1_rdb_1').checked = true;
            }

            if (delcat == "1") {
                document.getElementById('ctl00_ContentPlaceHolder1_rdb_0').checked = true;
            }
            //if (delcat == "0") {
            //    document.getElementById('ctl00_ContentPlaceHolder1_chkDelCat_0').checked = false;
            //    document.getElementById('ctl00_ContentPlaceHolder1_chkDelCat_1').checked = true;
            //} else if (delcat == "1") {
            //    document.getElementById('ctl00_ContentPlaceHolder1_chkDelCat_0').checked = true;
            //    document.getElementById('ctl00_ContentPlaceHolder1_chkDelCat_1').checked = false;
            //}
            //else {
            //    document.getElementById('ctl00_ContentPlaceHolder1_chkDelCat_0').checked = false;
            //    document.getElementById('ctl00_ContentPlaceHolder1_chkDelCat_1').checked = false;
            //}
            
            document.getElementById('ctl00_ContentPlaceHolder1_txtPartCode').value = partcode;
            document.getElementById('ctl00_ContentPlaceHolder1_txtDescription').value = partdesc;
            document.getElementById('ctl00_ContentPlaceHolder1_txtSPQ').value = spq;
            document.getElementById('ctl00_ContentPlaceHolder1_txtPArtSize').value = partsize;
            document.getElementById('ctl00_ContentPlaceHolder1_txtDOS').value = DOS;
            document.getElementById('ctl00_ContentPlaceHolder1_txtModel').value = model;
            document.getElementById('ctl00_ContentPlaceHolder1_txtPCat').value = category;
            document.getElementById('btnModal').click();
        }

        function dismissModal() {
            document.getElementById('btnDismissModal').click();
        }
    </script>

     <cc1:msgBox ID="msgBox" runat="server" />
</asp:Content>