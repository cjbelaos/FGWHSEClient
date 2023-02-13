<%@ Page Language="C#"  MasterPageFile="~/Form/MasterPalletMonitoring.Master"  AutoEventWireup="true" CodeBehind="EmptyBoxBoardDetails.aspx.cs" Inherits="FGWHSEClient.Form.EmptyBoxBoardDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display:none">
     <asp:Label ID="lblSupplierID" runat="server" Text="Label"></asp:Label>
 </div>
<table>
    <tr>
        <td>
            <asp:ImageButton ID="imgBack" runat="server" ImageUrl ="~/Image/Back.png" />
        </td>
        <td>
            <asp:LinkButton ID="lnkBack" runat="server" Font-Underline="false" ForeColor ="#3399FF" OnClientClick="javascript:window.close();">Back To Monitoring Screen</asp:LinkButton>
        </td>
    </tr>
</table>
<div style =" left:40px;top:110px;">
    <br />
    <div style="margin-left:15px; float:left; font-family:Calibri; color:Black">
       <table>
            <tr style="font-size:18px">
                <td>
                    <asp:Label ID= "lblSupplierName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="font-size:14px">
                <td>
                    Updated as of : <asp:label ID="lblDate" runat="server"></asp:label>
                </td>
            </tr>
       </table>

        <br />
        <div style="height:320px;overflow:auto">
            <asp:GridView ID="gvLoad" runat="server" Width="1150px" AutoGenerateColumns="False" CellPadding="3" BackColor="White" Font-Size="Small" GridLines="Both">
                 <RowStyle ForeColor="Black" BorderColor="black"  BorderWidth="1pt" />
                 <Columns>
                    

                     <asp:TemplateField HeaderText="RFID TAG" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="14px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="11px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblRFIDTAG" runat="server" Text='<%# Eval("RFIDTAG") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>

                     <asp:TemplateField HeaderText="EPC" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="14px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="11px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblEPC" runat="server" Text='<%# Eval("EPC") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>


                     <asp:TemplateField HeaderText="SCANNED BY" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="14px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="11px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblCREATEDBY" runat="server" Text='<%# Eval("CREATEDBY") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>

                     <asp:TemplateField HeaderText="SCANNED DATE" >
                             <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor ="#538dd5" Font-Size="14px"/>
                             <ItemStyle HorizontalAlign="Center" Font-Size="11px" /> 
                             <ItemTemplate>
                                  <asp:Label ID="lblCREATEDDATE" runat="server" Text='<%# Eval("CREATEDDATE") %>'></asp:Label>
                             </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
               </asp:GridView>

        </div>

        <br />

        <table>
            <tr>
                <td>Pick-up Rate : </td>
                <td>
                    <asp:Label ID="lblRate" runat="server" Text=""></asp:Label>
                </td>
            </tr>

        </table>

    </div>
</div>

</asp:Content>
