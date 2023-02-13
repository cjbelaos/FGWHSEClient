using System;
using log4net;
using log4net.Config;

namespace com.eppi.utils
{
    /// <summary>
    /// Class for BQICS data inquiry web service logger.
    /// </summary>
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        public Logger()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static ILog GetInstance()
        {
            try
            {
                XmlConfigurator.Configure();
            }
            catch (Exception e)
            {
                throw e;
            }
            return log;
        }
    }
}
