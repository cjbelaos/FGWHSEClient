<%@ Page Language="C#" MasterPageFile="~/Form/HTMasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PLCMenu.aspx.cs" Inherits="FGWHSEClient.Form.PLCMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
 window.history.forward(1);
 </script> 
 
 
    <div style =" left:40px;top:110px; font-weight:600;">
    <center>
      </center><asp:Panel id="pnlPassed" runat="server" style="display:block" >
      <table align="left" width="470px" height="260px" bordercolordark="#006666" frame="border" cellpadding="0px" cellspacing="0px" style='border-color:black;border-width:thin; border-style:solid;'>
            <tr align="center" style="background-color:Silver">
                 <td align="center" height="15PX"> 
                 
                 </td>
            </tr>
            
            <tr>
                <td>
                    <center>
                                     <center>
                                 
                                     <asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="20pt"  ForeColor="BLACK" Text="STORING PROCESS" ></asp:Label>
                                     </center>

                            <br />
                                     
                                     
 


                        
                                        
                      <%--  <table>
                         
                        <tr>
                        <td>
                            <div id="div1" style=" border-color: inherit; border-width: 1; background-color:white; border-style:groove; " runat="server" class="auto-style1" >
                             <asp:Label ID="lblLocationST" runat="server" Font-Names="Arial" Font-Size="10pt"  ForeColor="#2F4F4F" Text="w/o Mother Lot" Font-Bold="True" ></asp:Label>
                            </div> 
                        </td>
                        <td>
                            <div id="divInspection" style=" border:1; background-color:white ; border-style:groove; width:210px; height:30px" runat="server" >
                             <asp:Label ID="lblInspection" runat="server" Font-Names="Arial" Font-Size="10pt"  ForeColor="#2F4F4F" Text="with Mother Lot" Font-Bold="True"  ></asp:Label>
                            </div> 
                        </td>
                        </tr>
                        </table>        --%>    
                                        
                                       <%--  <div id="divLocationST" style=" border:1; background-color:#ffc000 ; border-style:groove; width:415px; height:30px" runat="server" >
                                            
                                        </div> --%>
                                        <table>
                                            <tr>
                                                <td>

                                        <div style="margin-top:5px">
                                        <asp:Button ID="btnPS1" runat="server" Text="PS1" Font-Size ="Larger"
                                         Width="200px" Height="50px"  onclick="btnPS1_Click" 
                                                BackColor="#59CE8F" Font-Bold="True"  />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnPS2" runat="server" Text="PS2" Font-Size ="Larger" Width="200px" 
                                         Height="50px" BackColor="#59CE8F" onclick="btnPS2_Click" Font-Bold="True" />
                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                    
                         <table>
                         
                        <tr>
                        <td>
                            <div runat="server" class="auto-style1" >
                             <asp:Label ID="lblPS1" runat="server" Font-Names="Arial" Font-Size="10pt"  ForeColor="#2F4F4F" Text="w/o Mother Lot" Font-Bold="True" ></asp:Label>
                            </div> 
                        </td>
                        <td>
                            <div   runat="server" class="auto-style1">
                             <asp:Label ID="lblPS2" runat="server" Font-Names="Arial" Font-Size="10pt"  ForeColor="#2F4F4F" Text="with Mother Lot" Font-Bold="True"  ></asp:Label>
                            </div> 
                        </td>
                        </tr>
                        </table>   
                           
                    </center>
                    
                </td>
            </tr>  
        </table>
        </asp:Panel>

</div>










</asp:Content>  
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <link type="text/css" rel="Stylesheet" href="../App_Themes/Stylesheet/Main.css"/>
    <style type="text/css">
        .auto-style1 {
            width: 210px;
            height: 30px;
        }
    </style>
</asp:Content>
  