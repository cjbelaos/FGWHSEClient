using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FGWHSEClient.Classes
{
    public class ObjectDOSVendorReply
    {
        public string SupplierCode { get; set; }
        public string MaterialNumber { get; set; }
        public string Plant { get; set; }
        public string Problem_Cat { get; set; }
        public string PIC { get; set; }
        public string Reason { get; set; }
        public string CounterMeasure { get; set; }
        public string User { get; set; }
    }
}