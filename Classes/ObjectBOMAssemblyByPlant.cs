using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FGWHSEClient.Classes
{
    public class ObjectBOMAssemblyByPlant
    {

        public string Assy_Level { get; set; }
        public string Child_ItemCode { get; set; }
        public string Child_Level { get; set; }
        public string Child_SupplierCode { get; set; }
        public string DOS_Level { get; set; }
        public string FG_Parent { get; set; }
        public string FG_Parent_Supplier { get; set; }
        public string Parent_ItemCode { get; set; }
        public string Parent_Level { get; set; }
        public string Parent_Supplier { get; set; }
        public string Plant { get; set; }
        public string Usage { get; set; }
        public string User { get; set; }
        public string Valid_From { get; set; }
        public string Valid_To { get; set; }
    }
}