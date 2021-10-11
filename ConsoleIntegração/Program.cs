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


// Contacts contact = new Contacts(service);

            //  contact.RetrieveMultipleContactsByAccountLinq(new Guid("75576fa0-2b26-ec11-b6e6-00224837234e"));

            /* foreach(string contactName in contactNames)
             {
                 Console.WriteLine(contactName);
             }
            */
            //  Console.ReadKey();
            /*  Contacts contact = new Contacts(service);
              EntityCollection contactsCRM = contact.RetrieveMultipleContactsByAccount(new Guid("75576fa0-2b26-ec11-b6e6-00224837234e"));

              foreach (Entity contactCRM in contactsCRM.Entities)
              {
                  string emailAdress = contactCRM.Contains("emailaddress1") ? contactCRM["emailaddress1"].ToString() : "Contato nao possui e-mail";
                  //string telephoneDaConta = ((AliasedValue)contactCRM["conta.telephone1"]).Value.ToString();
                  string telephoneDaConta = contactCRM.Contains("conta.telephone1") ? ((AliasedValue)contactCRM["conta.telephone1"]).Value.ToString() : "Contato não possui telefone";

                  EntityReference parenteCustomerId = (EntityReference)contactCRM["parentcustomerid"];
                  OptionSetValue tipoDaConta = (OptionSetValue)((AliasedValue)contactCRM["conta.fyi_tipodaconta"]).Value;
                  int totalDeOportunidades = (int)((AliasedValue)contactCRM["conta.fyi_totaldeoportunidades"]).Value;
                  Money valorTotalDeOportunidade = (Money)((AliasedValue)contactCRM["conta.fyi_valortotaldeoportunidades"]).Value;

                  Console.WriteLine(telephoneDaConta);
                  Console.WriteLine(contactCRM["fullname"].ToString());
                  Console.WriteLine(emailAdress);

                  Console.WriteLine("O nome da conta é ");
                  Console.WriteLine(parenteCustomerId.Name);
                  Console.WriteLine(parenteCustomerId.Id);
                  Console.WriteLine(parenteCustomerId.LogicalName);


                  Console.WriteLine($"O tipo da conta é {tipoDaConta.Value}");
                  Console.WriteLine($"O total de oportunidades é {totalDeOportunidades}");
                  Console.WriteLine($"O valor total de oportunidades é {valorTotalDeOportunidade.Value}");
                  Console.ReadKey();
              }
             */

            // Account account = new Account(service);
            //account.DeleteAccount();

            // ConviteDoEvento convite = new ConviteDoEvento(service);
            //convite.ExecuteMultipleRequestConviteDoEvento();