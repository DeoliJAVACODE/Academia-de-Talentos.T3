using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIntegração.Model
{
    public class Contacts
    {
        public IOrganizationService Service { get; set; }
        public string TableName = "contact";
        public Contacts(IOrganizationService service)
        {
            this.Service = service;
        }

        public EntityCollection RetrieveMultipleContactsByAccount(Guid accountId)
        {
            QueryExpression queryContacts = new QueryExpression(this.TableName);
            queryContacts.ColumnSet.AddColumns("fullname","emailaddress1", "parentcustomerid");
            queryContacts.Criteria.AddCondition("parentcustomerid", ConditionOperator.Equal, accountId);

            queryContacts.AddLink("account", "parentcustomerid", "accountid", JoinOperator.Inner);
            queryContacts.LinkEntities[0].Columns.AddColumns("telephone1", "fyi_tipodaconta", "fyi_totaldeoportunidades", "fyi_valortotaldeoportunidades");
            queryContacts.LinkEntities[0].EntityAlias = "conta";

            return this.Service.RetrieveMultiple(queryContacts);
        }

     /*   public List<string> RetrieveMultipleContactsByAccountLinq(Guid accountid)
        {
            var context = new OrganizationServiceContext(this.Service);

            var resultado = (from contact in context.CreateQuery("contact")
                             where ((EntityReference)contact["parentcustomerid"]).Id == accountid
                             select contact["fullname"].ToString()).ToList();
            return resultado;
                             
        }*/

        public object RetrieveMultipleContactsByAccountLinq(Guid accountid)
        {
            var context = new OrganizationServiceContext(this.Service);

            var resultado = (from contact in context.CreateQuery("contact")
                             join account in context.CreateQuery("account")
                                          on contact["parentcustomerid"] equals account["accountid"]
                             where ((EntityReference)contact["parentcustomerid"]).Id == accountid
                             select new {
                                 ContactName = contact["fullname"].ToString(),
                                 ContatoEmail = contact.Contains("emailaddress1") ? contact["emailaddress1"].ToString() : string.Empty
                             }).ToList();
            foreach(var contact in resultado)
            {
                Console.WriteLine(contact.ContactName);
                Console.WriteLine(contact.ContatoEmail);

            }
            return resultado;

        }
    }
}
