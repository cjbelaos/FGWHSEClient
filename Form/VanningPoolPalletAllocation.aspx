<%@ Page Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="VanningPoolPalletAllocation.aspx.cs" Inherits="FGWHSEClient.VanningPoolPalletAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language ="javascript" type="text/javascript">

    function body_onLoad() {
        document.all("txtLocationID").focus();
    }
    
    function autosize() {
        
      if (screen.width == 480) {
                document.write ('<body style="zoom: 200%">');
                document.location.href = 'PalletAllocationHT.aspx';
          }
         else
         {
              document.location.href='PalletAllocation.aspx';
         }
     }


     function selectText() {
         document.getElementById("<%=txtPalletNo.ClientID %>").select();
     }



</script>


<div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;VANNING POOL ALLOCATION
<br /><br />
        <table >
            <tr>
                <td style=" vertical-align:top; text-align:left">
                     <div>
                <table style ="text-align:left">
                <tr style ="color:Black">
                   <td style="padding:5px; text-align:right; width:225px" >
                        Vanning Pool Container No.
                    </td>
                    <td style="padding:5px;text-align:LEFT">
                        <asp:TextBox ID="txtLocationID" runat="server" Width="200px" Height="25px" 
                            Font-Size="20px" ontextchanged="txtLocationID_TextChanged"></asp:TextBox>
                        <div style=" display:none"><asp:Label ID="lblPalCount" runat="server" Text="0"></asp:Label></div>
                    </td>
                    </tr>
                    <tr style ="color:Black"><td style="padding:5px; text-align:right" >
                        Pallet No.
                    </td>
                    <td style="padding:5px">
                        <asp:TextBox ID="txtPalletNo" runat="server" Width="200px" Height="25px"
                            Font-Size="20px" MaxLength="8" ontextchanged="txtPalletNo_TextChanged"></asp:TextBox></td></tr><tr>
                    <td ></td>
                    <td>
                    <asp:RadioButtonList ID="rdoAllocate" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected ="true">Allocate</asp:ListItem><asp:ListItem>Unallocate</asp:ListItem></asp:RadioButtonList></td></tr><tr>
                    <td ></td></tr>
                 
                 
                <tr>
                <td ></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="SAVE" Height="35px" Width="70px" onclick="btnSave_Click"/>
                     &nbsp;
                    
                    <asp:Button ID="btnClear" runat="server" Text="CLEAR" Height="35px" Width="70px" onclick="btnClear_Click"/>
                    
                </td>
                </tr>
                
          </table>  
         </div>
                </td>
                
            </tr>
        </table>      
          
</div>
 
 
 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
 TargetControlID="txtPalletNo"  FilterType="Numbers" />
     

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

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
//            document.getElementById('ctl00_datetime').innerHTML = "Current Date : " + date + " " + h + ":" + m + ":" + s;

               
            document.getElementById('ctl00_ContentPlaceHolder1_GNtxtSafety').innerHTML = s;
            
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
    



</asp:Content>
