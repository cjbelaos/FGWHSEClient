using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FGWHSEClient.Classes
{
    public class FileWriter
    {
        private string filePath;
        private StreamWriter w;
        public FileWriter() {
            filePath = System.Web.Hosting.HostingEnvironment.MapPath("~") + "/Logs/" + DateTime.Now.ToString("MM-dd-yyyy")+".log.txt";
        }
        public void Write(string message) {
            if (!File.Exists(filePath))
            {
                w = File.CreateText(filePath);
            }
            else
            {
                w = File.AppendText(filePath);
            }
            w.WriteLine(message);
            w.Flush();
            w.Close();
        }
    }
}