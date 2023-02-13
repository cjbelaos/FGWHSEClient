using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FGWHSEClient.Classes
{
    public class ObjectShortageList
    {
        public string Issued { get; set; }
        public string MainVendor { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialNumber { get; set; }
        public string OwnStock { get; set; }
        public string RK { get; set; }
        public string PastDue { get; set; }
        public string PlantCode { get; set; }
        public string PlantID { get; set; }
        public string ShortageListID { get; set; }
        public string Stock { get; set; }
        public string Supplier { get; set; }
        public Dictionary<string,float> Values { get; set; }
    }
}