<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master"  AutoEventWireup="true" CodeBehind="AntennaRegistration.aspx.cs" Inherits="FGWHSEClient.Form.AntennaRegistration" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style =" left:40px;top:110px;">
       <br />
        &nbsp; &nbsp; <font style="font-size:15px; color:Black">ANTENNA REGISTRATION</font>
       <br />
        <div style="margin-left:16px; margin-top:20px; color:Black">
              <table>
                  <tr>
                      <td style="text-align:right;vertical-align:top">ANTENNA TYPE :</td>
                      <td><asp:DropDownList ID="ddAntennaType" runat="server"></asp:DropDownList></td>
                      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                      <td style="text-align:right;vertical-align:top"> WITH GNS I/F :</td>
                      <td>
                          <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"> 
                                                <asp:ListItem Text="NO" Value ="NO" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="YES" Value ="YES"></asp:ListItem>

                          </asp:RadioButtonList></td>
                  </tr>
                  <tr>
                      <td style="text-align:right;vertical-align:top" class="auto-style1">ANTENNA READER IP :</td>
                      <td class="auto-style1"><asp:TextBox ID="txtIP" runat="server"></asp:TextBox></td>
                      <td class="auto-style1"></td>
                      <td style="text-align:right;vertical-align:top" class="auto-style1">SYSTEM LOCATION :</td>
                      <td class="auto-style1"><asp:DropDownList ID="ddLocation" runat="server"></asp:DropDownList></td>
                  </tr>
                  <tr>
                      <td style="text-align:right;vertical-align:top">ANTENNA LOCATION NAME :</td>
                      <td><asp:TextBox ID="txtLocName" runat="server">
                          </asp:TextBox></td>
                      <td></td>
                      <td></td>
                      <td>
                          <table>
                              <tr>
                                  <td><asp:Button ID="btnSave" runat="server" Text="SAVE" Height="30px" Width="78px" /></td>
                              </tr>
                          </table>
                          
                      </td>
                  </tr>
                  

                 
              </table>

            <br />
            <div style="overflow:auto; height:260px; width:920px">
                <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns ="false" Width ="900Px">
                <Columns>

                        <asp:TemplateField HeaderStyle-Width="150px" HeaderText="ANTENNA HOSTNAME"  ItemStyle-Width="110px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblANTENNAHOSTNAME" runat="server" Text='<%#Bind("ANTENNAHOSTNAME") %>'></asp:Label>
                                    </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderStyle-Width="110px" HeaderText="LOCATION ID"  ItemStyle-Width="110px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLOCATIONID" runat="server" Text='<%#Bind("LOCATIONID") %>'></asp:Label>
                                    </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-Width="200px" HeaderText="LOCATION NAME"  ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllocation_name" runat="server" Text='<%#Bind("location_name") %>'></asp:Label>
                                    </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderStyle-Width="110px" HeaderText="AREA NAME"  ItemStyle-Width="110px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAREANAME" runat="server" Text='<%#Bind("AREANAME") %>'></asp:Label>
                                    </ItemTemplate>
                        </asp:TemplateField>
                        

                        <asp:TemplateField HeaderStyle-Width="150px" HeaderText="SYSTEM LOCATION"  ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLOCATION" runat="server" Text='<%#Bind("LOCATION") %>'></asp:Label>
                                    </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderStyle-Width="110px" HeaderText="WITH GNS IF"  ItemStyle-Width="110px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGNSIFenableFlag" runat="server" Text='<%#Bind("GNSIFenableFlag") %>'></asp:Label>
                                    </ItemTemplate>
                        </asp:TemplateField>

                    




                </Columns>
                 <HeaderStyle Font-Size="9pt" HorizontalAlign="center" VerticalAlign="Middle" Font-Bold="True"  BackColor = "#C5D9F1"  />
                <RowStyle Font-Size="9pt" HorizontalAlign="center" VerticalAlign="Middle" Font-Bold ="false"  />
                
                </asp:GridView>

            </div>


        </div>
    </div>


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
        .auto-style1 {
            height: 39px;
        }
    </style>
</asp:Content>
