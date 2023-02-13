using System;
using FGWHSEClient.LdapService;
using FGWHSEClient.LogInService;

namespace com.eppi.utils
{
    /// <summary>
    /// Summary description for ServiceLocator.
    /// </summary>
    public class ServiceLocator
    {
        private static LdapService ldapService = null;
        private static LoginService loginService = null;

        public ServiceLocator()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static LdapService GetLdapService()
        {
            if (ldapService == null)
            {
                ldapService = new LdapService();
                ldapService.Url = System.Configuration.ConfigurationSettings.AppSettings["wsLdapServiceString"];

                if (Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["webClientProtocolTimeout"]) == -1)
                {
                    ldapService.Timeout = System.Threading.Timeout.Infinite;
                }
                else
                {
                    ldapService.Timeout = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["webClientProtocolTimeout"]);
                }

                ldapService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }

            return ldapService;
        }

        public static LoginService GetLoginService()
        {
            if (loginService == null)
            {
                loginService = new LoginService();
                loginService.Url = System.Configuration.
                    ConfigurationSettings.AppSettings["wsLoginServiceString"];

                if (Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["webClientProtocolTimeout"]) == -1)
                {
                    loginService.Timeout = System.Threading.Timeout.Infinite;
                }
                else
                {
                    loginService.Timeout = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["webClientProtocolTimeout"]);
                }

                loginService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }

            return loginService;
        }
    }
}
