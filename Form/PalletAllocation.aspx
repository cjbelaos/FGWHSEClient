<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PalletAllocation.aspx.cs" Inherits="FGWHSEClient.Form.PalletAllocation" %>


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
&nbsp;&nbsp;PALLET ALLOCATION
<br /><br />
        <table >
            <tr>
                <td style=" vertical-align:top; text-align:left">
                     <div class="divInput">
        <table style ="text-align:left">
                <tr style ="color:Black">
                   <td style="padding:5px; text-align:right;">
                        Location ID
                    </td>
                    <td style="padding:5px;text-align:LEFT">
                        <asp:TextBox ID="txtLocationID" runat="server" Width="200px" Height="25px" AutoPostBack="true"
                            Font-Size="20px" ontextchanged="txtLocationID_TextChanged"></asp:TextBox>
                        <div style=" display:none"><asp:Label ID="lblPalCount" runat="server" Text="0"></asp:Label></div>
                    </td>
                </tr>
                <tr style ="color:Black"><td style="padding:5px; text-align:right">
                        Pallet No.
                    </td>
                    <td style="padding:5px">
                        <asp:TextBox ID="txtPalletNo" runat="server" Width="200px" Height="25px"  AutoPostBack="true"
                            Font-Size="20px" MaxLength="8" ontextchanged="txtPalletNo_TextChanged"></asp:TextBox></td></tr><tr>
                <td></td>
                <td>
                    <asp:RadioButtonList ID="rdoAllocate" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected ="true">Allocate</asp:ListItem><asp:ListItem>Unallocate</asp:ListItem></asp:RadioButtonList></td></tr><tr><td></td></tr>
                 
                 
                <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="SAVE" Height="35px" Width="70px" onclick="btnSave_Click"/>
                     &nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="DELETE" Height="35px" 
                        Width="70px" onclick="btnDelete_Click"/>
                     &nbsp;
                    <asp:Button ID="btnClear" runat="server" Text="CLEAR" Height="35px" Width="70px" onclick="btnClear_Click"/>
                    
                </td>
                </tr>
                
          </table>  
         </div>
                </td>
                <td style=" vertical-align:top; text-align:left; ">
                    <div style=" border-style:solid; border-color:gray; border-bottom-width:thin;border-right-width:thin; margin-left: 50px; width:723px; height:153px">
                        <% 
                            Response.Write("<table>"); 
                            for( int x = 1; x<= dtPallet.DefaultView.Count; x++)
                            {
                                if (x == 1 || (x-1) % 10 == 0)
                                {
                                    Response.Write("<tr>");
                                }

                                Response.Write("<td style =  font-size:x-small;border-style:solid;border-color:black;border-width:thin;padding:5px >");
                                string strPalletNo = dtPallet.DefaultView[x - 1]["PalletNo"].ToString();
                                Response.Write(strPalletNo);
                                Response.Write("</td>");

                                if (x % 10 == 0)
                                {
                                    Response.Write("</tr>");
                                }
                                
                            }

                            Response.Write("</table>"); 
                        %>
                    </div>
                </td>
            </tr>
        </table>      
          
</div>
 
 
 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
 TargetControlID="txtPalletNo"  FilterType="Numbers" />
     

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>

