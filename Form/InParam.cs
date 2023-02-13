using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Runtime.Serialization;

namespace FGWHSEClient.Form
{

    public class InParam
    {
        [DataMember(Name = "to_location")]
        public string to_location { get; set; }

        [DataMember(Name = "scanned_by")]
        public string scanned_by { get; set; }

        [DataMember(Name = "remarks")]
        public string remarks { get; set; }

        [DataMember(Name = "returned_parts")]
        public List<parts> returned_parts { get; set; }
    }

    [DataContract()]
    public class OutParam
    {
        [DataMember(Name = "succeeded")]
        public int succeeded;

        [DataMember(Name = "failed")]
        public int failed;

        [DataMember(Name = "message")]
        public string message;

        [DataMember(Name = "returned_parts")]
        public List<return_parts> returned_parts { get; set; }
    }

    [DataContract()]
    public class parts
    {
        [DataMember(Name = "ref_no")]
        public string ref_no { get; set; }

        [DataMember(Name = "lot_no")]
        public string lot_no { get; set; }

        [DataMember(Name = "part_code")]
        public string part_code { get; set; }

        [DataMember(Name = "box_no")]
        public int box_no { get; set; }

        [DataMember(Name = "split_no")]
        public int split_no { get; set; }

        [DataMember(Name = "remarks")]
        public string remarks { get; set; }

        [DataMember(Name = "qty")]
        public decimal qty { get; set; }
    }


    public class return_parts
    {
        [DataMember(Name = "result")]
        public string result { get; set; }

        [DataMember(Name = "message")]
        public string message { get; set; }

        [DataMember(Name = "ref_no")]
        public string ref_no { get; set; }

        [DataMember(Name = "lot_no")]
        public string lot_no { get; set; }

        [DataMember(Name = "part_code")]
        public string part_code { get; set; }

        [DataMember(Name = "box_no")]
        public string box_no { get; set; }

        [DataMember(Name = "split_no")]
        public string split_no { get; set; }

        [DataMember(Name = "remarks")]
        public string remarks { get; set; }

        [DataMember(Name = "qty")]
        public decimal qty { get; set; }
    }
}