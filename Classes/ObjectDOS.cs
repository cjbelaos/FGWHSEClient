using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FGWHSEClient.Classes
{
    public class ObjectDOS
    {
        public string PlantID { get; set; }
        public string Category { get; set; }
        public string Cat_Name { get; set; }
        public string ModelName { get; set; }
        public string MaterialCode { get; set; }
        public string Description { get; set; }
        public string SupplierCode { get; set; }
        public string EPPIStock { get; set; }
        public string EPPI_DOS { get; set; }
        public string DOS_Level { get; set; }
        public string Supplier_Stock { get; set; }
        public string DOS_Stock { get; set; }
        public string Total_Stock { get; set; }
        public string Problem_Cat { get; set; }
        public string Reason { get; set; }
        public string PIC { get; set; }
        public string CounterMeasure { get; set; }
        public List<ObjectDOSDet> Det { get; set; }
    }
}