<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="BypassMaintenance.aspx.cs" Inherits="FGWHSEClient.Form.BypassMaintenance" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="left: 40px; top: 110px;">

        <br />
        &nbsp; &nbsp; <font style="font-size: 17px; color: Black; font-weight: bold">BYPASS USER MAINTENANCE</font>
        <br />

        <div style="margin-left: 16px; margin-top: 20px; color: Black" align="center">
            <table border="0" cellspacing="2" style="font-size: 13px; font-weight: normal">


                <tr>
                    <td class="auto-style4">User ID:  </td>


                    <td align="center" class="auto-style6" valign="middle">
                        <asp:TextBox ID="txtUserId" runat="server" Width="120px" Enabled="True"></asp:TextBox>
                    </td>
                    <td align="center" class="auto-style2" valign="middle">  <asp:Button ID="btnSearch" runat="server" Text="Search" Width="70px" OnClick="btnSearch_Click"/></td>
                </tr>

                </table>
        </div>

        <asp:Panel ID="pnlDN" runat="server">
            <br />

            <div id="grid" runat="server" align="center" style="font-size: 10px; overflow: auto;">
                <table>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle">
                            <asp:GridView ID="gvBypass" runat="server" Width="560px"
                                AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                GridLines="Both" AllowPaging="true" PageSize="15"
                                OnRowDataBound="gvBypass_RowDataBound" OnRowCommand="gvBypass_RowCommand" OnSelectedIndexChanged="gvBypass_SelectedIndexChanged" OnRowDeleting="gvBypass_RowDeleting">
                               

                                

                <Columns>
                    <asp:TemplateField HeaderStyle-BackColor="#538dd5" ItemStyle-Wrap="true" ItemStyle-Width="50px" ItemStyle-BackColor="White">  
                    <ItemTemplate>  
                            <asp:LinkButton ID="btnCancel" class="btn btn-danger btn-xs" Text="<i class='ace-icon fa fa-remove bigger-110'></i>Delete" CommandName="Delete" runat="server" OnClick="btnCancel_Click"/>
                    </ItemTemplate> 
                </asp:TemplateField>  
                                

                                    <asp:BoundField DataField="EmployeeNo" ItemStyle-BorderColor="Black" HeaderText="USER ID"
                                         HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="50px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black"  >
                                    </asp:BoundField>

                                                 
                                    <asp:BoundField DataField="FULLNAME" ItemStyle-BorderColor="Black" HeaderText="USER NAME"
                                         HeaderStyle-BackColor="#538dd5" HeaderStyle-Font-Size="10px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" ItemStyle-Width="150px"
                   ItemStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" >
                                    </asp:BoundField>
                                </Columns>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <RowStyle HorizontalAlign="Center" />

                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>

        </asp:Panel>
        <br />


        <div style="margin-left: 16px; margin-top: 20px; color: Black" align="center">
            <table border="0" cellspacing="2" style="font-size: 13px; font-weight: normal">


              


                <tr>
                    <td align="center" valign="middle" class="auto-style4">User ID:  </td>
                    <td align="center" valign="middle" class="auto-style5">
                        <asp:TextBox ID="TxtUserId1" runat="server" Width="120px" Enabled="True"></asp:TextBox>
                    </td>

                </tr>


                <tr>
                    <td colspan="2" style="padding-top: 20px" align="center" valign="middle">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="60px" OnClick="btnAdd_Click" />
                     
                    </td>
                </tr>
            </table>
        </div>

    </div>
    <%--<ajaxToolkit:Calendarextender ID="Calendarextender1" runat="server" BehaviorID="calendar1"
TargetControlID="txtDateFrom"
Format="dd-MMM-yyyy HH:mm tt"
PopupButtonID="imgCalendar3"  >
</ajaxToolkit:Calendarextender>


<ajaxToolkit:Calendarextender ID="Calendarextender3" runat="server" BehaviorID="calendar2"
TargetControlID="txtDateTo"
Format="dd-MMM-yyyy HH:mm tt"
PopupButtonID="imgCalendar2">
</ajaxToolkit:Calendarextender>--%>

    <cc1:msgBox ID="MsgBox1" runat="server"></cc1:msgBox>
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <link type="text/css" rel="Stylesheet" href="../App_Themes/Stylesheet/Main.css"/>  

     
      <script>
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
        .auto-style2 {
            height: 30px;
            width: 109px;
        }
        .auto-style4 {
            height: 30px;
            width: 70px;
        }
        .auto-style5 {
            height: 30px;
        }
        .auto-style6 {
            height: 30px;
            width: 144px;
        }
    </style>
</asp:Content>


