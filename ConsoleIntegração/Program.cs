using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Tooling;
using ConsoleIntegração.Model;

namespace ConsoleIntegração
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrganizationService service = ConnectionFactory.GetCrmService();

            Account account = new Account(service);
            account.UpdateAccountDesconto();
                       

        }


    }
}

