<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="ContainerLabelPrinting.aspx.cs" Inherits="FGWHSEClient.Form.ContainerLabelPrinting" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
   

    <script language ="javascript" type="text/javascript">


        function printdiv(printpage) {
            var headstr = "<html><head><title></title></head><body>";
            var footstr = "</body>";
            var newstr = document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
  
    
    
    function body_onLoad() {
        document.all("txtLocationID").focus();
    }
    
    
</script>


<div style =" left:40px;top:110px; font-weight:600;">

<br />
&nbsp;&nbsp;CONTAINER LABEL PRINTING
<br /><br />

        <div class="divInputContainerLabelPrinting">
        <table style ="text-align:left">
               
                <tr style ="color:Black"><td style="padding:5px; text-align:right">
                        OD No. / Container No.
                    </td>
                    <td style="padding:5px">
                        <asp:TextBox ID="txtContainerNo" runat="server" Width="200px" Height="25px" Font-Size="20px"></asp:TextBox></td></tr><tr>
                <td></td>
        
                 
                <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="SEARCH" Height="35px" 
                        Width="70px" onclick="btnSearch_Click"/>
                    &nbsp;&nbsp;
                    <%--<asp:Button ID="btnPrint" runat="server" Text="PRINT" Height="35px" Width="70px"
                    onclick="printdiv('divPrint')"  />
                    
                    <input name="b_print" type="button" class="1pt"   onClick="printdiv('ctl00_ContentPlaceHolder1_divPrint');" value=" Print ">--%>
                      
                </td>
                </tr>
                
          </table>  
         </div>
          
          
          
          <%--<div id="divPrint" runat="server" align="center">
             <table>
             <tr>
             <td>
                OD No.:
             </td>
             <td>
                <asp:Label ID="lblODNo" runat="server" ></asp:Label>
             </td>     
             </tr>
             
             <tr>
             <td>
                Container No.:
             </td>
             <td>
                 <asp:Label ID="lblContainerNo" runat="server" ></asp:Label>
             </td>
             </tr>
             </table>
             
             
             <asp:Image id="myBarCode"  runat="server"></asp:Image>
             
              <asp:Label ID="lblBarcodeContainer" runat="server" Font-Size="50px" Font-Bold="false"  Font-Names="3of9"></asp:Label>
          
          </div>--%>
          
        </div>
 

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>

