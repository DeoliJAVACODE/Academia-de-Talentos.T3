using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIntegração.Model
{
    class ConviteDoEvento
    {
        public IOrganizationService Service { get; set; }

        public ConviteDoEvento(IOrganizationService service)
        {
            this.Service = service;
        }

        public void ExecuteMultipleRequestConviteDoEvento()
        {
            ExecuteMultipleRequest executeMultipleRequest = new Microsoft.Xrm.Sdk.Messages.ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = false,
                    ReturnResponses = false
                }
            };
            for (int i = 0; i < 10; i++)
            {
                Entity conviteDoEvento = new Entity("fyi_convitedoevento");
                conviteDoEvento["fyi_evento"] = new EntityReference("fyi_evento", new Guid("35204f1f-0d27-ec11-b6e6-00224837234e"));
                conviteDoEvento["fyi_cliente"] = new EntityReference("account", new Guid("4eab6858-fe26-ec11-b6e6-00224837234e"));

                CreateRequest creatRequest = new CreateRequest()
                {
                    Target = conviteDoEvento
                };

                executeMultipleRequest.Requests.Add(creatRequest);
            }
            ExecuteMultipleResponse executeMultipleResponse = (ExecuteMultipleResponse)this.Service.Execute(executeMultipleRequest);

            foreach(var responses in executeMultipleResponse.Responses)
                if(responses.Fault != null)
                {
                    Console.WriteLine(responses.Fault.ToString());
                }
        }
    }
}
