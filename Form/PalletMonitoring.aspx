<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPalletMonitoring.Master" AutoEventWireup="true" CodeBehind="PalletMonitoring.aspx.cs" Inherits="FGWHSEClient.Form.PalletMonitoring" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
<tr>
<td>

      <div style ="left:40px; font-weight:600; width:740px;">
    
         <table style ="text-align:left">
                <tr style ="color:Black">
                   <td style="padding:5px; text-align:right;" >
                        Warehouse
                    </td>
                    <td style="padding:5px;text-align:right">
                        <asp:DropDownList ID="ddWH" runat="server"  Width="200px" AutoPostBack="True" 
                            onselectedindexchanged="ddWH_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                    <%--<td style="padding:5px">
                        <asp:Button ID="btnContent" runat="server" Text="Content List" Width="114px" 
                            onclick="btnContent_Click" />
                    </td>--%>
                    <td style="padding:5px">
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="114px" 
                            onclick="btnRefresh_Click" />
                    </td>
                    <td>
                        <span style =" font-size:xx-small">
                        Last Refreshed : 
                        <asp:Label ID="lblRefresh" runat="server" Text=""></asp:Label>
                        </span>
                    </td>
                    
           
                </tr>
                <tr style ="color:Black">
                    <td style="padding:5px; text-align:right">
                        Location Group
                    </td>
                    <td style="padding:5px">
                        <asp:DropDownList ID="ddLocGrp" runat="server"  Width="200px" 
                            AutoPostBack="True" onselectedindexchanged="ddLocGrp_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                    <td style="padding:5px">
                        <asp:Button ID="btnPackage" runat="server" Text="Package List" Width="114px" 
                            onclick="btnPackage_Click" />
                    </td>
                </tr>
                <tr style ="color:Black">
                    <td style="padding:5px; text-align:right">
                        <div id="display1" runat = "server">
                        Display Mode
                        </div>
                    </td>
               
                        <td style="padding:5px; font-size:smaller" colspan = "4">
                             <div id="display2" runat = "server">
                                    <asp:RadioButtonList ID="rdbDisplayMode" runat="server" RepeatDirection ="Horizontal" >
                                    <asp:ListItem Text ="OQA Status" Selected ="True" Value = "0"></asp:ListItem>
                                    <asp:ListItem Text ="Expiration Status" Enabled = "true" Value = "1"></asp:ListItem>
                                    <asp:ListItem Text ="FIFO" Enabled = "true" Value = "2"></asp:ListItem>
                                    </asp:RadioButtonList>
                            </div>
                            
                        </td>
               
                </tr>
          </table>
        </div>
 
</td>

<td>

           <div style="float:right; top:60px;" >
                     <asp:Image ID="imgLegend" runat="server" ImageUrl="<%=getPathName()%>" Height="150px" Width="445px"/>
           </div>       
        
</td>
</tr>
</table> 
        
        
        
        
    <div style ="font-size:medium; font-family:Arial; text-align:center">
 
      
  
    
      
        
          <%
              try
              {

                  if (Session["UserName"] == null)
                  {
                      Response.Write("<script>");
                      Response.Write("alert('Session expired! Please log in again.');");
                      Response.Write("window.location = '../Login.aspx';");
                      Response.Write("</script>");
                  }
                  else
                  {


                      string strDefaultPalletNo = "&nbsp;";
                      string CellSize = "";
                      string CellHeight = "";
                      //string strDefaultEmptyPallet = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                      //string strDefaultEmptyContainer = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                      string strDefaultStatusColor = "lightgray";
                      Response.Write("<table cellspacing = 10 style = padding-bottom:100px; >");
                      string strUnit = "[ - ]";
                      string strContainerNo = "[ - ]";
                      int intNewline = 1;
                      string strFIFOorLotNo = "";
                      string LaneTypeDisplay = "";



                      for (int intDisp = 1; intDisp <= dvLocType.Count; intDisp++)
                      {


                          //dvLane = maintLB.GET_LOCATION_GROUP("All").Tables[0].DefaultView;
                          string LocGroupLaneName = dvLocType[intDisp - 1]["location_type_name"].ToString();
                          string LocGroupLaneID = dvLocType[intDisp - 1]["location_type_id"].ToString();

                          dvAnyLane = maintLB.GET_LOCATION_BY_LOCATION_TYPE_ID(LocGroupLaneID, ddWH.Text).Tables[0].DefaultView;
                          int AnyLaneCount = 0;



                          if (dvAnyLane.Count > 0)
                          {
                              //if (LocGroupLaneID == "CY")
                              //{
                              AnyLaneCount = 1;
                              //}
                              //else
                              //{
                              AnyLaneCount = dvAnyLane.Count;
                              //}
                              if (intNewline == 1)
                              {
                                  Response.Write("<tr>");
                              }
                              Response.Write("<td style= border-color:black;border-style:solid;border-width:thin;text-align:left;vertical-align:top;>");
                              //if (ddLocGrp.Text == "All")
                              //{
                              Response.Write("<table cellspacing = 2>");



                              string Lanename = "";
                              string LaneID = "";
                              string PalletNo = strDefaultPalletNo;
                              string palletStatusColor = strDefaultStatusColor;
                              string QAStatusColor = strDefaultStatusColor;
                              string strContainerNoDisplay = "";
                              string strODno = "&nbsp";

                              int maxPalletLoc = 0;
                              int maxLine = 0;
                              strUnit = "[ - ]";
                              strContainerNo = "[ - ]";




                              for (int x = 0; x < AnyLaneCount; x++)
                              {

                                  Lanename = dvAnyLane[x]["location_name"].ToString();
                                  LaneID = dvAnyLane[x]["Location_id"].ToString();

                                  LaneTypeDisplay = dvAnyLane[x]["DISPLAYTYPE"].ToString();

                                  maxPalletLoc = Convert.ToInt32(dvAnyLane[x]["MaxPalletLoc"].ToString());
                                  maxLine = Convert.ToInt32(dvAnyLane[x]["lines"].ToString());
                                  strUnit = dvAnyLane[x]["unit_type_name"].ToString();
                                  strContainerNo = dvAnyLane[x]["ContainerNo"].ToString();
                                  if (strContainerNo == "")
                                  {
                                      strContainerNo = "-";
                                  }
                                  //strDefaultPalletNo = dvAnyLane[x]["EMPTY_CODE"].ToString();
                                  CellSize = dvAnyLane[x]["CELLSIZE"].ToString();
                                  CellHeight = dvAnyLane[x]["CELLHEIGHT"].ToString();
                                  //if (LaneTypeDisplay == "CY")
                                  //{
                                  //    strDefaultPalletNo = strDefaultEmptyContainer;
                                  //}
                                  //else
                                  //{
                                  //    strDefaultPalletNo = strDefaultEmptyPallet;
                                  //}
                                  if (LaneTypeDisplay == "LB")
                                  {
                                      strContainerNoDisplay = " - [ " + strContainerNo + " ]";
                                  }

                                  Response.Write("<table style= border-width:thin;width:100% >");
                                  if (x == 0)
                                  {
                                      Response.Write("<tr>");
                                      Response.Write("<td style = height:50px;vertical-align:top;font-size:x-large;color:black >");
                                      Response.Write("<center><strong>" + LocGroupLaneName.ToUpper() + "</strong></center>");

                                      Response.Write("</td>");
                                      Response.Write("</tr>");
                                  }
                                  Response.Write("<tr>");
                                  Response.Write("<td style= border-color:blue;border-bottom-style:solid;>");

                                  Response.Write("<span style = float:left>");
                                  Response.Write("<strong>");
                                  Response.Write(Lanename + strContainerNoDisplay);
                                  Response.Write("</strong>");
                                  Response.Write("</span>");


                                  Response.Write("<span style = float:right>");
                                  Response.Write("unit : " + strUnit);
                                  Response.Write("</span>");


                                  Response.Write("</td>");



                                  Response.Write("</tr>");


                                  Response.Write("<tr>");
                                  Response.Write("<td>");
                                  Response.Write("<tr>");
                                  Response.Write("<td>");



                                  //dvAnyLaneContent = maintLB.GET_PALLETS_BY_LOCATIONID(LaneID).Tables[0].DefaultView;
                                  dvAnyLaneContent = maintLB.GET_PALLETS_BY_LOCATIONID_FILTERED(LaneID, rdbDisplayMode.SelectedValue.ToString()).Tables[0].DefaultView;

                                  Response.Write("<table>");
                                  for (int y = 1; y <= maxPalletLoc; y++)
                                  {
                                      if (y - 1 < dvAnyLaneContent.Count)
                                      {
                                          strODno = dvAnyLaneContent[y - 1]["ODNo"].ToString();
                                          PalletNo = dvAnyLaneContent[y - 1]["PalletNo"].ToString();
                                          palletStatusColor = dvAnyLaneContent[y - 1]["PalletStatusColor"].ToString();
                                          QAStatusColor = dvAnyLaneContent[y - 1]["QAStatusColor"].ToString();
                                          strFIFOorLotNo = dvAnyLaneContent[y - 1]["FIFOorLotNo"].ToString();

                                          if (PalletNo == "")
                                          {
                                              PalletNo = strDefaultPalletNo;
                                              strFIFOorLotNo = strDefaultPalletNo;
                                          }

                                          if (strODno == "")
                                          {

                                              strODno = strDefaultPalletNo;
                                          }

                                          if (palletStatusColor == "")
                                          {
                                              if (strContainerNo == "-")
                                              {
                                                  palletStatusColor = "White";
                                              }
                                              else
                                              {
                                                  palletStatusColor = strDefaultStatusColor;
                                              }
                                          }

                                          if (QAStatusColor == "")
                                          {
                                              QAStatusColor = strDefaultStatusColor;
                                          }
                                      }
                                      else
                                      {
                                          PalletNo = strDefaultPalletNo;
                                          strODno = strDefaultPalletNo;
                                          if (LaneTypeDisplay == "LB" && strContainerNo == "-")
                                          {
                                              palletStatusColor = "White";
                                              QAStatusColor = "White";
                                          }
                                          else
                                          {

                                              palletStatusColor = strDefaultStatusColor;
                                              QAStatusColor = strDefaultStatusColor;
                                              strFIFOorLotNo = strDefaultPalletNo;
                                          }


                                      }

                                      if (y == 1)
                                      {
                                          Response.Write("<tr>");

                                      }


                                      Response.Write("<td>");
                                      Response.Write("<table cellspacing = 0 style= border-color:Gray;border-style:solid;border-width:thin;color:#F8F8FF;font-size:small>");
                                      if (LaneTypeDisplay != "CY")
                                      {
                                          Response.Write("<tr>");
                                          Response.Write("<td style= text-align:center;font-size:x-small;border-color:Gray;color:black;border-bottom-style:solid;border-width:thin;height:" + CellHeight + ">");
                                          //Response.Write(strODno);
                                          Response.Write(PalletNo);
                                          Response.Write("</td>");
                                          Response.Write("</tr>");

                                      }
                                      else
                                      {
                                          strODno = PalletNo;
                                      }

                                      Response.Write("<tr>");
                                      Response.Write("<td style = font-size:x-small;height:" + CellHeight + ";text-align:center;background-color:" + palletStatusColor + ";width:" + CellSize + ">");

                                      Response.Write("<strong>");
                                      Response.Write(strODno);
                                      //Response.Write(PalletNo);
                                      Response.Write("</strong>");

                                      Response.Write("</td>");
                                      Response.Write("</tr>");

                                      if (LaneTypeDisplay != "CY")
                                      {
                                          Response.Write("<tr>");
                                          Response.Write("<td style= border-color:Gray;border-top-style:solid;border-width:thin;height:" + CellHeight + ";background-color:" + QAStatusColor + ";color:black;text-align:center>");
                                          Response.Write("<strong>");
                                          Response.Write(strFIFOorLotNo);
                                          Response.Write("</strong>");
                                          Response.Write("</td>");
                                          Response.Write("</tr>");
                                      }


                                      Response.Write("</table>");
                                      Response.Write("</td>");

                                      //if (y == 1)
                                      //{
                                      //    Response.Write("</tr>");
                                      //}
                                      if (y % maxLine == 0)
                                      {
                                          Response.Write("</tr>");
                                          Response.Write("<tr>");
                                      }

                                  }
                                  Response.Write("</table>");



                                  Response.Write("</td>");
                                  Response.Write("</tr>");

                                  Response.Write("</td>");
                                  Response.Write("</tr>");

                                  Response.Write("<table>");
                                  Response.Write("</br>");

                              }







                              Response.Write("</table>");
                              Response.Write("</td>");

                              if (intNewline % 2 == 0)
                              {
                                  Response.Write("</tr>");
                              }
                              intNewline = intNewline + 1;



                          }



                      }
                      //----------------------------------U N A L L O C A T E D  P A L L E T/ C O N T A I N E R----------------------------------


                      if (dvUnallocatedContent.Count > 0)
                      {

                          Response.Write("<tr>");
                          Response.Write("<td style= border-color:black;border-style:solid;border-width:thin;vertical-alignment:top;text-align:left>");
                          Response.Write("<table cellspacing = 2>");


                          string strODNoUnallocated = "";
                          string unallocatedPalletname = "";
                          string unallocatedPalletID = "";
                          string unallocatedPalletPalletNo = strDefaultPalletNo;
                          string palletStatusColorUA = strDefaultStatusColor;
                          string QAStatusColorUA = strDefaultStatusColor;
                          string strCellSize = "";
                          string strCellHeight = "";
                          int maxPalletLocUA = 0;
                          int maxLineUA = 10;

                          string strHeader = "";
                          if (ddWH.Text == "CY")
                          {
                              strHeader = "UNALLOCATED CONTAINER";
                          }
                          else
                          {
                              strHeader = "UNALLOCATED PALLET";
                          }

                          Response.Write("<table style= border-width:thin; width: 400px>");

                          Response.Write("<tr>");
                          Response.Write("<td style = height:30px;vertical-align:top;font-size:x-large;color:black;>");
                          Response.Write("<center><strong>" + strHeader + "</strong></center>");

                          Response.Write("</td>");
                          Response.Write("</tr>");


                          Response.Write("</tr>");


                          Response.Write("<tr>");
                          Response.Write("<td>");
                          Response.Write("<tr>");
                          Response.Write("<td>");





                          Response.Write("<table>");
                          for (int y = 1; y <= dvUnallocatedContent.Count; y++)
                          {
                              maxLineUA = Convert.ToInt32(dvUnallocatedContent[0]["maxLine"].ToString());
                              if (y - 1 <= dvUnallocatedContent.Count)
                              {

                                  unallocatedPalletPalletNo = dvUnallocatedContent[y - 1]["PalletNo"].ToString();
                                  strODNoUnallocated = dvUnallocatedContent[y - 1]["ODNo"].ToString();
                                  palletStatusColorUA = dvUnallocatedContent[y - 1]["PalletStatusColor"].ToString();
                                  QAStatusColorUA = dvUnallocatedContent[y - 1]["QAStatusColor"].ToString();
                                  strCellSize = dvUnallocatedContent[y - 1]["CELLSIZE"].ToString();
                                  strCellHeight = dvUnallocatedContent[y - 1]["CELLHEIGHT"].ToString();
                                  strFIFOorLotNo = dvUnallocatedContent[y - 1]["FIFOorLotNo"].ToString();
                                  if (unallocatedPalletPalletNo == "")
                                  {
                                      unallocatedPalletPalletNo = strDefaultPalletNo;
                                      strFIFOorLotNo = strDefaultPalletNo;

                                  }
                                  if (palletStatusColorUA == "")
                                  {
                                      palletStatusColorUA = strDefaultStatusColor;
                                  }
                                  if (QAStatusColorUA == "")
                                  {
                                      QAStatusColorUA = strDefaultStatusColor;
                                  }
                              }
                              else
                              {
                                  unallocatedPalletPalletNo = strDefaultPalletNo;
                                  palletStatusColorUA = strDefaultStatusColor;
                                  QAStatusColorUA = strDefaultStatusColor;
                                  strFIFOorLotNo = strDefaultPalletNo;

                              }

                              if (y == 1)
                              {
                                  Response.Write("<tr>");

                              }


                              Response.Write("<td>");
                              Response.Write("<table cellspacing = 0  style= border-color:Gray;border-style:solid;border-width:thin;>");


                              //Response.Write("<td>");
                              //Response.Write("<table cellspacing = 0 style= border-color:Gray;border-style:solid;border-width:thin;color:#F8F8FF;font-size:small>");

                              Response.Write("<tr>");
                              Response.Write("<td style= width:" + strCellSize + ";height:" + strCellHeight + ";text-align:center;font-size:x-small;border-color:Gray;color:black;height:" + CellHeight + ">");
                              Response.Write(unallocatedPalletPalletNo);
                              //Response.Write(strODNoUnallocated);
                              Response.Write("</td>");
                              Response.Write("</tr>");



                              Response.Write("<tr>");
                              Response.Write("<td style = width:" + strCellSize + ";height:" + strCellHeight + ";font-size:x-small;background-color:" + palletStatusColorUA + ";color:#F8F8FF;border-color:Gray;border-top-style:solid;border-width:thin;text-align:center>");


                              Response.Write("<strong>");
                              Response.Write(strODNoUnallocated);

                              //Response.Write(unallocatedPalletPalletNo);
                              Response.Write("</strong>");

                              Response.Write("</td>");
                              Response.Write("</tr>");
                              Response.Write("<tr>");
                              Response.Write("<td style= width:" + strCellSize + ";height:" + strCellHeight + ";border-color:Gray;border-top-style:solid;border-width:thin;background-color:" + QAStatusColorUA + ">");
                              Response.Write("<strong>");
                              Response.Write(strFIFOorLotNo);
                              Response.Write("</strong>");
                              Response.Write("</td>");
                              Response.Write("</tr>");
                              Response.Write("</table>");
                              Response.Write("</td>");

                              //if (y == 1)
                              //{
                              //    Response.Write("</tr>");
                              //}
                              if (y % maxLineUA == 0)
                              {
                                  Response.Write("</tr>");
                                  Response.Write("<tr>");
                              }

                          }
                      }








                      Response.Write("</table>");



                      Response.Write("</td>");
                      Response.Write("</tr>");

                      Response.Write("</td>");
                      Response.Write("</tr>");

                      Response.Write("<table>");
                      //Response.Write("</br>");









                      Response.Write("</table>");



                      Response.Write("</td>");
                      Response.Write("<tr>");


                      Response.Write("</tr>");
                      Response.Write("</table>");
                  }
              }
              catch (Exception ex)
              {
                  MsgBox1.alert(ex.Message.ToString());
              }
          
          %>
    </div>
    <div id = "dvPrint" runat = "server">
    
    
        <asp:GridView ID="grdExport" runat="server">                   
        </asp:GridView>
    
        
    </div>

<cc1:msgbox id="MsgBox1" runat="server"></cc1:msgbox>
</asp:Content>

