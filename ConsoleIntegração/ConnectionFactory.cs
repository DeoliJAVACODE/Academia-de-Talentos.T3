using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace ConsoleIntegração
{
    class ConnectionFactory
    {
        public static IOrganizationService GetCrmService()
        {

            string connectionString =
            "AuthType=OAuth;" +
            "Username=admin@andersondeoli.onmicrosoft.com;" +
            "Password=Gateway22;" +
            "Url=https://org522deef4.crm2.dynamics.com/;" +
            "AppId=868e5c1b-9656-4d46-b2ad-d28c4e68d3fb;" +
            "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;
    }
    }


 
}
 

/*  class ConnectionFactory
    {
        public static IOrganizationService GetCrmService()
        {

            string connectionString =
            "AuthType=OAuth;" +
            "Username=admin@andersondeoli.onmicrosoft.com;" +
            "Password=Gateway22;" +
            "Url=https://org03a12d08.crm2.dynamics.com/;" +
            "AppId=5cc84e77-c70d-44ff-9ef4-5ac310e68919;" +
            "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;
        }
    }
  */