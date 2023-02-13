<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Form/MasterPalletMonitoring.Master"  CodeBehind="PSI_MASTER_PARTS.aspx.cs" Inherits="FGWHSEClient.Form.PSI_MASTER_PARTS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:left;width:1200px">
<div style="margin-left:10px">
    <br />
    <span>PARTS MASTER MAINTENANCE</span>

    <br />
    <br />
    <table >
        <tr>
            <td>
                VENDOR CODE/NAME : 
            </td>
            <td >
                &nbsp;&nbsp;<asp:TextBox ID="txtVendorCode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            
            <td >
               &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="SEARCH" OnClick="btnSearch_Click"/>
            </td>

        </tr>

    </table>
  

    <div style ="margin-left: 5px">
            <br />

                <span style="font-size:small;font-style:italic">Click on vendor code to update delete the parts information</span>
            <br />
            <asp:GridView ID="grdSupplier" runat="server" AutoGenerateColumns ="false" AllowPaging ="true" PageSize ="12" OnPageIndexChanging="grdSupplier_PageIndexChanging">
                <HeaderStyle BackColor =  "Blue"  ForeColor ="White" />
            <Columns>
                <asp:TemplateField HeaderStyle-Width="150px" HeaderText="VENDOR CODE"  ItemStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSupplierCode" runat="server" OnClick ="lnk_Click" Text='<%#Bind("SUPPLIERCODE") %>'>LinkButton</asp:LinkButton>
               
                                </ItemTemplate>
                 </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="400px" HeaderText="VENDOR NAME"  ItemStyle-Width="400px">
                                <ItemTemplate>
                                    <asp:Label ID="lblVendorName" runat="server" Text='<%#Bind("SUPPLIERNAME") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>
                <%--  --%>

                <asp:TemplateField HeaderStyle-Width="250px" HeaderText="LAST UPLOAD DATE"  ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDateTransferred" runat="server" Text='<%#Bind("DATATRANSFERDATE") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>

                

            </Columns>
            </asp:GridView>
    </div>
    
 </div>

</div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <link type="text/css" rel="Stylesheet" href="../App_Themes/Stylesheet/Main.css"/>  

     
      <script type="text/javascript">
        function startTime() {
            var today = new Date();
            var h = today.getHours();
            var m = today.getMinutes();
            var s = today.getSeconds();

            var date = today.toDateString();

            // add a zero in front of numbers<10
            m = checkTime(m);
            s = checkTime(s);
            document.getElementById('ctl00_datetime').innerHTML = "Current Date : " + date + " " + h + ":" + m + ":" + s;



            t = setTimeout(function() { startTime() }, 500);
        }

        function checkTime(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        }




         //Menu
         // Copyright 2006-2007 javascript-array.com

         var timeout = 500;
         var closetimer = 0;
         var ddmenuitem = 0;

         // open hidden layer
         function mopen(id) {
             // cancel close timer
             mcancelclosetime();

             // close old layer
             if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';

             // get new layer and show it
             ddmenuitem = document.getElementById(id);
             ddmenuitem.style.visibility = 'visible';

         }
         // close showed layer
         function mclose() {
             if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';
         }

         // go close timer
         function mclosetime() {
             closetimer = window.setTimeout(mclose, timeout);
         }

         // cancel close timer
         function mcancelclosetime() {
             if (closetimer) {
                 window.clearTimeout(closetimer);
                 closetimer = null;
             }
         }

         // close layer when click-out
         document.onclick = mclose; 
    



    </script>
    
    <style type="text/css">
        .auto-style1 {
            width: 200px;
            height: 35px;
        }
        .auto-style2 {
            width: 150px;
            height: 35px;
        }
        .auto-style3 {
            height: 35px;
        }
    </style>
</asp:Content>
