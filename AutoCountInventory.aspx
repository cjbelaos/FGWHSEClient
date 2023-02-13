<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="AutoCountInventory.aspx.cs" Inherits="FGWHSEClient.AutoCountInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AUTO COUNT INVENTORY</title>
    <link type="text/css" rel="Stylesheet" href="../App_Themes/Stylesheet/Main.css" />


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



            t = setTimeout(function () { startTime() }, 500);
        }

        function checkTime(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        }

        //refresh page
        //var TIMER_INTERVAL = 10000;
        //setTimeout(refreshPage, TIMER_INTERVAL);

        //function refreshPage() {
        //    window.location = "APISystemDisplay.aspx";
        //    setTimeout(refreshPage, TIMER_INTERVAL);
        //}


       <%-- function ShowModal(line) {

            document.getElementById('<%= "ctl00_ContentPlaceHolder1_txtproddate"%>').value = line;
            document.location.href = "APISystemDisplay.aspx?proddate=" + line;


        }--%>




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

        //confirmationAlert
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("End of Shift Stocks Count is correct?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        //accepts numeric only
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        //loading
        function DisplayProgressMessage(ctl, msg) {

            document.getElementById('ctl00_ContentPlaceHolder1_lblProgress').innerText = msg;

            $(ctl).prop("disabled", true);
            $("body").addClass("submit-progress-bg");


            setTimeout(function () {
                $(".submit-progress").removeClass("hidden");
            }, 1);

        }
        function StopProgress() {
            $(this).prop("disabled", true);
            $("body").removeClass("submit-progress-bg");

            setTimeout(function () {
                $(".submit-progress").addClass("hidden");
            }, 1);
        }


        //var shouldsubmit = false; // Represent the button should ask for confirmation or not.
        //var isFiredTwice = false;
        //window.onbeforeunload = confirmUnloading;

        //// Funcion called before unloading of the page.
        //function confirmUnloading() {
        //    // Check for both the condition to check whether some change occur in the page or not.
        //    if (!shouldsubmit) {
        //        if (navigator.appName == "Microsoft Internet Explorer") // For IE
        //        {
        //            if (!isFiredTwice) {
        //                event.returnValue = "If you have any unsaved data in the current page, it will be lost."
        //                isFiredTwice = true;
        //                setTimeout("isFiredTwice = false;", 0)
        //            }
        //        }
        //        else {
        //            // For FF
        //            return "If you have any unsaved data in the current page, it will be lost.";
        //        }
        //    }
        //}

        //function onEnterStartStocks(e) {


        //    shouldsubmit = true;
        //    isFiredTwice = true;

        //}




    </script>

    <style type="text/css">
        .auto-style2 {
            width: 347px;
            height: 100px;
        }

        .auto-style11 {
            width: 703px;
        }

        .auto-style14 {
            height: 100px;
        }

        .upper-case {
            text-transform: uppercase
        }

        .auto-style15 {
            width: 345px;
            height: 40px;
        }

        .auto-style16 {
            height: 40px;
        }

        .auto-style19 {
            width: 347px;
            height: 80px;
        }

        .auto-style20 {
            height: 80px;
        }

        .auto-style21 {
            width: 345px;
            height: 60px;
        }

        .auto-style22 {
            height: 60px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="height: 800px; width: 800px; align-content: center">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div style="height: 750px; width: 800px;">
            <div style="border: thin solid #184B8A; background-color: #e6f7ff; height: 910px; width: 750px; padding-left: 40px;">
                <br />
                <h1 align="center" style="color: #003399">AUTO COUNT INVENTORY SYSTEM</h1>
                 
             
                
                <table width="750px" height="80px" align="center" style="font-size: small; font-weight: normal; vertical-align: middle; text-align: center">
                    <tr>
                        <td class="auto-style15" align="left">
                            <label style="font-size: x-large; color: #184B8A; font-weight: normal;">USER ID: </label>
                            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
                        </td>
                        <td class="auto-style16" valign="middle">
                            <asp:TextBox ID="txtbxUserID" runat="server" Height="30px" Width="301px" CssClass="upper-case" Font-Size="X-Large" Font-Bold="False" 
                                onkeypress="return isNumberKey(event)" OnTextChanged="txtbxUserID_TextChanged" AutoPostBack="True"></asp:TextBox>
                         
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style15" align="left">
                            <label style="font-size: x-large; font-weight: normal; color: #184B8A"></label>
                        </td>
                        <td class="auto-style16" valign="middle">
                            <asp:Label ID="lblName" runat="server" Text="" Font-Size="X-Large"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="750px" height="120px" align="center" style="font-size: small; font-weight: normal; vertical-align: middle; text-align: center">
                    <tr>
                        <td class="auto-style21" align="left">
                            <label style="font-size: x-large; color: #184B8A; font-weight: normal;">PART CODE: </label>
                            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
                        </td>

                        <td class="auto-style22" valign="middle">
                          
                            <asp:TextBox ID="txtbxPartCode" runat="server" Height="50px" Width="301px" Font-Size="X-Large" CssClass="upper-case" Font-Bold="True" Enabled="False" 
                                OnTextChanged="txtbxPartCode_TextChanged"></asp:TextBox>
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style21" align="left">
                            <label style="font-size: x-large; font-weight: normal; color: #184B8A">LINE: </label>
                        </td>
                        <td class="auto-style22" valign="middle">
                            <asp:TextBox ID="txtbxLine" runat="server" Height="50px" Width="300px" Font-Size="X-Large" AutoPostBack="True" 
                                OnTextChanged="txtbxLine_TextChanged" CssClass="upper-case" Enabled="False"></asp:TextBox>
                            
                        </td>
                    </tr>
                </table>

                   
        
                <h2 align="left" style="color: #003399; text-align: left;" class="auto-style11">INVENTORY</h2>

                <asp:UpdatePanel ID="UpdatePnl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                <table width="750px" height="420px" align="center" style="font-size: small; font-weight: normal; vertical-align: middle; text-align: center">
                    <tr>
                        <td class="auto-style19" align="left">
                            <label style="font-size: x-large; font-weight: normal; color: #184B8A">START OF SHIFT STOCKS:</label>
                        </td>

                        <td class="auto-style20" valign="middle">
                            <asp:TextBox ID="txtbxStrtstocks" runat="server" Height="70px" Width="300px" Font-Size="X-Large" onkeypress="return isNumberKey(event)" 
                                OnTextChanged="txtbxStrtstocks_TextChanged" CssClass="upper-case" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style19" align="left">
                            <label style="font-size: x-large; font-weight: normal; color: #184B8A">TOTAL PARTS DELIVERED:</label>
                        </td>


                        <td class="auto-style20" valign="middle">
                            <asp:TextBox ID="txtbxPDelivered" runat="server" Height="70px" Width="300px" Font-Size="X-Large" Enabled="False"></asp:TextBox>
                        </td>



                    </tr>
                    <tr>
                        <td class="auto-style19" align="left" style="font-size: x-large">
                            <label style="font-size: x-large; font-weight: normal; color: #184B8A">TOTAL PARTS USED:</label>
                        </td>


                        <td class="auto-style20" valign="middle">
                            <asp:TextBox ID="txtbxPUsed" runat="server" Height="70px" Width="300px" Font-Size="X-Large" Enabled="False"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="auto-style19" align="left" style="font-size: x-large">
                            <label style="font-size: x-large; font-weight: normal; color: #184B8A">REMAINING STOCKS:</label>
                        </td>


                        <td class="auto-style20" valign="middle">
                            <asp:TextBox ID="txtbxRStocks" runat="server" Height="70px" Width="300px" Font-Size="X-Large" Enabled="False" 
                                ></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="auto-style19" align="left" style="font-size: x-large">
                            <label style="font-size: x-large; font-weight: normal; color: #184B8A">END OF SHIFT STOCKS:</label>
                        </td>

                        <td class="auto-style20" valign="middle">
                            <asp:TextBox ID="txtbxEndStocks" runat="server" Height="70px" Width="300px" Font-Size="X-Large" CssClass="upper-case" Enabled="False" 
                                onkeypress="return isNumberKey(event)" OnTextChanged="txtbxEndStocks_TextChanged"  onchange = "Confirm()" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2" align="left">
                            <label style="font-size: xx-large; font-weight: bold; color: #184B8A">VARIANCE:</label>
                        </td>

                        <td valign="middle" class="auto-style14">
                            <asp:TextBox ID="txtbxVariance" runat="server" Height="80px" Width="300px" Font-Size="XX-Large" Enabled="False" Font-Bold="True" ForeColor="Red" ></asp:TextBox>
                        </td>
                    </tr>
                </table>

                  </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtbxStrtstocks" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div>

                    <asp:Timer ID="Timer" runat="server" Enabled="False" Interval="20000" OnTick="Timer_Tick"></asp:Timer>

                    <asp:Timer ID="Timer1" Enabled="False" Interval="10000" OnTick="Timer1_Tick" runat="server">
                    </asp:Timer>

                </div>
                <div style="float: right; margin-right: 50px; margin-left: 50px; width: 700px;">
                    <asp:Label ID="displayELog" runat="server" Font-Size="Large" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <br />
                <div style="float: right; margin-right: 50px; margin-left: 50px; width: 700px;">
                    <asp:Label ID="displayMsg" runat="server" Font-Size="Large" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <br />
                <div style="float: right; margin-right: 50px; margin-left: 50px; width: 700px;">
                    <asp:Label ID="lblReferenceNo" runat="server" Font-Size="Large" ForeColor="Red" Visible="False"></asp:Label>
                </div>

           
            </div>
        </div>
    </form>
</body>
</html>
