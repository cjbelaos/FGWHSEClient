using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FGWHSEClient.Classes
{
    public class ObjectDOSDet
    {
        public string PlantID { get; set; }
        public string MaterialCode { get; set; }
        public string SupplierCode { get; set; }
        public DateTime DateInput { get; set; }
        public string Value { get; set; }
    }
}