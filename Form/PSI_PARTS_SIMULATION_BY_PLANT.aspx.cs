using System;
using System.Data;
using Newtonsoft.Json;
using FGWHSEClient.DAL;
using System.Web.Services;
using FGWHSEClient.Classes;
using System.Collections.Generic;

namespace FGWHSEClient.Form
{
    public partial class PSI_PARTS_SIMULATION_BY_PLANT : System.Web.UI.Page
    {
        static string UserID;
        static DataTable dtSupplierCode;
        static DataTable dtPlantCode;
        static bool isForViewing = false;
        public string strPageSubsystem = "";
        public string strPageSubsystem2 = "";
        public string strAccessLevel = "";

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    strPageSubsystem = "FGWHSE_040";
                    strPageSubsystem2 = "FGWHSE_062";
                    isForViewing = checkAuthority(strPageSubsystem2);
                    if (!checkAuthority(strPageSubsystem) && Session["UserName"].ToString() != "GUEST")
                    {
                        Response.Write("<script>");
                        Response.Write("alert('You are not authorized to access the page.');");
                        Response.Write("window.location = 'Default.aspx';");
                        Response.Write("</script>");
                    }
                }
                UserID = Session["UserID"].ToString();
                dtPlantCode = new DALPSI_PLANT().PSI_GET_PLANTS_BY_USERID(UserID);
                dtSupplierCode = new DALPSI_PLANT().PSI_GET_VENDORS_BY_USERID(UserID);
            }
            catch (Exception ex)
            {
                Response.Write("<script>");
                Response.Write("alert('" + ex.Message.ToString() + "');");
                Response.Write("</script>");
            }
        }

        private bool checkAuthority(string strPageSubsystem)
        {
            bool isValid = false;
            try
            {
                if (Session["Subsystem"] != null)
                {
                    DataView dvSubsystem = new DataView();
                    dvSubsystem = (DataView)Session["Subsystem"];

                    if (dvSubsystem.Count > 0)
                    {
                        dvSubsystem.Sort = "Subsystem";

                        int iRow = dvSubsystem.Find(strPageSubsystem);

                        if (iRow >= 0)
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }

                        string strRole = dvSubsystem.Table.Rows[iRow]["Role"].ToString();

                        if (strRole != "")
                        {
                            strAccessLevel = strRole;
                        }

                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                isValid = false;
                return isValid;
            }
        }

        [WebMethod]
        public static string CheckIfForViewing()
        {
            return JsonConvert.SerializeObject(isForViewing);
        }

        [WebMethod]
        public static string GetPlantsByUserId()
        {
            return JsonConvert.SerializeObject(dtPlantCode);
        }

        [WebMethod]
        public static string GetVendorsByUserId()
        {
            return JsonConvert.SerializeObject(dtSupplierCode);
        }

        [WebMethod]
        public static string GetPartsCodeByPlantandVendors(string strPLANT, string strVENDORS)
        {
            DALPSI_PLANT DPSI = new DALPSI_PLANT();
            return JsonConvert.SerializeObject(DPSI.PSI_GET_PARTSCODE_BY_PLANT_AND_VENDOR(strPLANT, strVENDORS));
        }

        [WebMethod]
        public static string GetPartsSimulationByPlant(string strPLANT, string strPARTS, string strVENDORS)
        {
            DALPSI_PLANT DPSI = new DALPSI_PLANT();
            DataTable dt = DPSI.PSI_GET_PARTSSIMULATION_BY_PLANT(strPLANT, strPARTS, strVENDORS);
            if (dt.Rows.Count > 0)
            {
                var StartDate = dt.Rows[0][209].ToString();
                for (var i = 11; i <= 208;)
                {
                    for (int j = 0; j <= 197; j++)
                    {
                        if (i == 11)
                        {
                            dt.Columns[i].ColumnName = StartDate;
                        }
                        else
                        {
                            DateTime Date = Convert.ToDateTime(StartDate);
                            dt.Columns[i].ColumnName = Date.AddDays(j).ToString("MM/dd/yyyy");
                        }
                        i++;
                    }
                }
                dt.Columns.Remove("MODEL");
                dt.Columns.Remove("FIRSTCOLUMNNAME");
            }
            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string SaveData(List<ObjectPartsSimulationByPlant> PS)
        {
            DALPSI_PLANT DPSI = new DALPSI_PLANT();
            string Message = "";
            foreach (var item in PS)
            {
                string strPLANT = item.Plant;
                string strMATERIALNUMBER = item.MaterialNumber;
                string strMAINVENDOR = item.MainVendor;
                string strFIRSTCOLUMNNAME = item.FirstColumnName;
                string strPAST = item.Past;
                string strDAY1 = item.DAY1;
                string strDAY2 = item.DAY2;
                string strDAY3 = item.DAY3;
                string strDAY4 = item.DAY4;
                string strDAY5 = item.DAY5;
                string strDAY6 = item.DAY6;
                string strDAY7 = item.DAY7;
                string strDAY8 = item.DAY8;
                string strDAY9 = item.DAY9;
                string strDAY10 = item.DAY10;
                string strDAY11 = item.DAY11;
                string strDAY12 = item.DAY12;
                string strDAY13 = item.DAY13;
                string strDAY14 = item.DAY14;
                string strDAY15 = item.DAY15;
                string strDAY16 = item.DAY16;
                string strDAY17 = item.DAY17;
                string strDAY18 = item.DAY18;
                string strDAY19 = item.DAY19;
                string strDAY20 = item.DAY20;
                string strDAY21 = item.DAY21;
                string strDAY22 = item.DAY22;
                string strDAY23 = item.DAY23;
                string strDAY24 = item.DAY24;
                string strDAY25 = item.DAY25;
                string strDAY26 = item.DAY26;
                string strDAY27 = item.DAY27;
                string strDAY28 = item.DAY28;
                string strDAY29 = item.DAY29;
                string strDAY30 = item.DAY30;
                string strDAY31 = item.DAY31;
                string strDAY32 = item.DAY32;
                string strDAY33 = item.DAY33;
                string strDAY34 = item.DAY34;
                string strDAY35 = item.DAY35;
                string strDAY36 = item.DAY36;
                string strDAY37 = item.DAY37;
                string strDAY38 = item.DAY38;
                string strDAY39 = item.DAY39;
                string strDAY40 = item.DAY40;
                string strDAY41 = item.DAY41;
                string strDAY42 = item.DAY42;
                string strDAY43 = item.DAY43;
                string strDAY44 = item.DAY44;
                string strDAY45 = item.DAY45;
                string strDAY46 = item.DAY46;
                string strDAY47 = item.DAY47;
                string strDAY48 = item.DAY48;
                string strDAY49 = item.DAY49;
                string strDAY50 = item.DAY50;
                string strDAY51 = item.DAY51;
                string strDAY52 = item.DAY52;
                string strDAY53 = item.DAY53;
                string strDAY54 = item.DAY54;
                string strDAY55 = item.DAY55;
                string strDAY56 = item.DAY56;
                string strDAY57 = item.DAY57;
                string strDAY58 = item.DAY58;
                string strDAY59 = item.DAY59;
                string strDAY60 = item.DAY60;
                string strDAY61 = item.DAY61;
                string strDAY62 = item.DAY62;
                string strDAY63 = item.DAY63;
                string strDAY64 = item.DAY64;
                string strDAY65 = item.DAY65;
                string strDAY66 = item.DAY66;
                string strDAY67 = item.DAY67;
                string strDAY68 = item.DAY68;
                string strDAY69 = item.DAY69;
                string strDAY70 = item.DAY70;
                string strDAY71 = item.DAY71;
                string strDAY72 = item.DAY72;
                string strDAY73 = item.DAY73;
                string strDAY74 = item.DAY74;
                string strDAY75 = item.DAY75;
                string strDAY76 = item.DAY76;
                string strDAY77 = item.DAY77;
                string strDAY78 = item.DAY78;
                string strDAY79 = item.DAY79;
                string strDAY80 = item.DAY80;
                string strDAY81 = item.DAY81;
                string strDAY82 = item.DAY82;
                string strDAY83 = item.DAY83;
                string strDAY84 = item.DAY84;
                string strDAY85 = item.DAY85;
                string strDAY86 = item.DAY86;
                string strDAY87 = item.DAY87;
                string strDAY88 = item.DAY88;
                string strDAY89 = item.DAY89;
                string strDAY90 = item.DAY90;
                string strDAY91 = item.DAY91;
                string strDAY92 = item.DAY92;
                string strDAY93 = item.DAY93;
                string strDAY94 = item.DAY94;
                string strDAY95 = item.DAY95;
                string strDAY96 = item.DAY96;
                string strDAY97 = item.DAY97;
                string strDAY98 = item.DAY98;
                string strDAY99 = item.DAY99;
                string strDAY100 = item.DAY100;
                string strDAY101 = item.DAY101;
                string strDAY102 = item.DAY102;
                string strDAY103 = item.DAY103;
                string strDAY104 = item.DAY104;
                string strDAY105 = item.DAY105;
                string strDAY106 = item.DAY106;
                string strDAY107 = item.DAY107;
                string strDAY108 = item.DAY108;
                string strDAY109 = item.DAY109;
                string strDAY110 = item.DAY110;
                string strDAY111 = item.DAY111;
                string strDAY112 = item.DAY112;
                string strDAY113 = item.DAY113;
                string strDAY114 = item.DAY114;
                string strDAY115 = item.DAY115;
                string strDAY116 = item.DAY116;
                string strDAY117 = item.DAY117;
                string strDAY118 = item.DAY118;
                string strDAY119 = item.DAY119;
                string strDAY120 = item.DAY120;
                string strDAY121 = item.DAY121;
                string strDAY122 = item.DAY122;
                string strDAY123 = item.DAY123;
                string strDAY124 = item.DAY124;
                string strDAY125 = item.DAY125;
                string strDAY126 = item.DAY126;
                string strDAY127 = item.DAY127;
                string strDAY128 = item.DAY128;
                string strDAY129 = item.DAY129;
                string strDAY130 = item.DAY130;
                string strDAY131 = item.DAY131;
                string strDAY132 = item.DAY132;
                string strDAY133 = item.DAY133;
                string strDAY134 = item.DAY134;
                string strDAY135 = item.DAY135;
                string strDAY136 = item.DAY136;
                string strDAY137 = item.DAY137;
                string strDAY138 = item.DAY138;
                string strDAY139 = item.DAY139;
                string strDAY140 = item.DAY140;
                string strDAY141 = item.DAY141;
                string strDAY142 = item.DAY142;
                string strDAY143 = item.DAY143;
                string strDAY144 = item.DAY144;
                string strDAY145 = item.DAY145;
                string strDAY146 = item.DAY146;
                string strDAY147 = item.DAY147;
                string strDAY148 = item.DAY148;
                string strDAY149 = item.DAY149;
                string strDAY150 = item.DAY150;
                string strDAY151 = item.DAY151;
                string strDAY152 = item.DAY152;
                string strDAY153 = item.DAY153;
                string strDAY154 = item.DAY154;
                string strDAY155 = item.DAY155;
                string strDAY156 = item.DAY156;
                string strDAY157 = item.DAY157;
                string strDAY158 = item.DAY158;
                string strDAY159 = item.DAY159;
                string strDAY160 = item.DAY160;
                string strDAY161 = item.DAY161;
                string strDAY162 = item.DAY162;
                string strDAY163 = item.DAY163;
                string strDAY164 = item.DAY164;
                string strDAY165 = item.DAY165;
                string strDAY166 = item.DAY166;
                string strDAY167 = item.DAY167;
                string strDAY168 = item.DAY168;
                string strDAY169 = item.DAY169;
                string strDAY170 = item.DAY170;
                string strDAY171 = item.DAY171;
                string strDAY172 = item.DAY172;
                string strDAY173 = item.DAY173;
                string strDAY174 = item.DAY174;
                string strDAY175 = item.DAY175;
                string strDAY176 = item.DAY176;
                string strDAY177 = item.DAY177;
                string strDAY178 = item.DAY178;
                string strDAY179 = item.DAY179;
                string strDAY180 = item.DAY180;
                string strDAY181 = item.DAY181;
                string strDAY182 = item.DAY182;
                string strDAY183 = item.DAY183;
                string strDAY184 = item.DAY184;
                string strDAY185 = item.DAY185;
                string strDAY186 = item.DAY186;
                string strDAY187 = item.DAY187;
                string strDAY188 = item.DAY188;
                string strDAY189 = item.DAY189;
                string strDAY190 = item.DAY190;
                string strDAY191 = item.DAY191;
                string strDAY192 = item.DAY192;
                string strDAY193 = item.DAY193;
                string strDAY194 = item.DAY194;
                string strDAY195 = item.DAY195;
                string strDAY196 = item.DAY196;
                string strDAY197 = item.DAY197;
                string strDAY198 = item.DAY198;
                string strUSER = item.User;

                Message = DPSI.PSI_SAVE_PARTS_SIMULATION_BY_PLANT(strPLANT, strMATERIALNUMBER, strMAINVENDOR, strFIRSTCOLUMNNAME, strPAST, strDAY1, strDAY2, strDAY3, strDAY4, strDAY5, strDAY6, strDAY7, strDAY8, strDAY9, strDAY10, strDAY11, strDAY12, strDAY13, strDAY14, strDAY15, strDAY16, strDAY17, strDAY18, strDAY19, strDAY20, strDAY21, strDAY22, strDAY23, strDAY24, strDAY25, strDAY26, strDAY27, strDAY28, strDAY29, strDAY30, strDAY31, strDAY32, strDAY33, strDAY34, strDAY35, strDAY36, strDAY37, strDAY38, strDAY39, strDAY40, strDAY41, strDAY42, strDAY43, strDAY44, strDAY45, strDAY46, strDAY47, strDAY48, strDAY49, strDAY50, strDAY51, strDAY52, strDAY53, strDAY54, strDAY55, strDAY56, strDAY57, strDAY58, strDAY59, strDAY60, strDAY61, strDAY62, strDAY63, strDAY64, strDAY65, strDAY66, strDAY67, strDAY68, strDAY69, strDAY70, strDAY71, strDAY72, strDAY73, strDAY74, strDAY75, strDAY76, strDAY77, strDAY78, strDAY79, strDAY80, strDAY81, strDAY82, strDAY83, strDAY84, strDAY85, strDAY86, strDAY87, strDAY88, strDAY89, strDAY90, strDAY91, strDAY92, strDAY93, strDAY94, strDAY95, strDAY96, strDAY97, strDAY98, strDAY99, strDAY100, strDAY101, strDAY102, strDAY103, strDAY104, strDAY105, strDAY106, strDAY107, strDAY108, strDAY109, strDAY110, strDAY111, strDAY112, strDAY113, strDAY114, strDAY115, strDAY116, strDAY117, strDAY118, strDAY119, strDAY120, strDAY121, strDAY122, strDAY123, strDAY124, strDAY125, strDAY126, strDAY127, strDAY128, strDAY129, strDAY130, strDAY131, strDAY132, strDAY133, strDAY134, strDAY135, strDAY136, strDAY137, strDAY138, strDAY139, strDAY140, strDAY141, strDAY142, strDAY143, strDAY144, strDAY145, strDAY146, strDAY147, strDAY148, strDAY149, strDAY150, strDAY151, strDAY152, strDAY153, strDAY154, strDAY155, strDAY156, strDAY157, strDAY158, strDAY159, strDAY160, strDAY161, strDAY162, strDAY163, strDAY164, strDAY165, strDAY166, strDAY167, strDAY168, strDAY169, strDAY170, strDAY171, strDAY172, strDAY173, strDAY174, strDAY175, strDAY176, strDAY177, strDAY178, strDAY179, strDAY180, strDAY181, strDAY182, strDAY183, strDAY184, strDAY185, strDAY186, strDAY187, strDAY188, strDAY189, strDAY190, strDAY191, strDAY192, strDAY193, strDAY194, strDAY195, strDAY196, strDAY197, strDAY198, strUSER);
            }
            return JsonConvert.SerializeObject(Message);
        }
    }
}