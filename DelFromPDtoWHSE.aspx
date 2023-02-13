<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DelFromPDtoWHSE.aspx.cs" Inherits="FGWHSEClient.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 87px;
        }

        .auto-style2 {
            width: 502px;
            height: 86px;
        }

        .auto-style3 {
            width: 165px;
        }
        .auto-style4 {
            width: 44px;
            height: 38px;
        }
        .auto-style5 {
            width: 179px;
        }
        .auto-style6 {
            width: 180px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width:384px">
        <div>
            <hr />
            <h5 style="background-color: #99CCFF">Welcome, </h5>
            <hr />
            <label>Main Menu</label>
            <br />
        </div>
        <div>
            <table style="margin: 0px; padding: 0px; width: 384px;" class="auto-style2">
                <tr>
                    <td class="auto-style1" style="font-size: small" align="center">&nbsp;</td>
                    <td class="auto-style3" align="center">
                        <button id="RecievePD" runat="server" class="auto-style6">RECIEVE PD <img src="Image/RECEIVE.PNG" class="auto-style4"/></button>
                    </td>
                    <td class="style14" align="right">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1" style="font-size: small" align="center"></td>
                    <td class="auto-style3" align="center">
                        <button id="Delivery" runat="server" class="auto-style5">DELIVERY<img src="Image/deliver.PNG" class="auto-style4"/></button>
                    </td>
                    <td class="style14" align="right">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1"></td>
                    <td class="auto-style3" align="center">
                        <button id="Exit" runat="server" class="auto-style5">EXIT<img src="Image/exit.PNG" class="auto-style4"/></button>
                    </td>
                    <td class="style4" align="right">&nbsp;</td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
