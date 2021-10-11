using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Tooling;
using ConsoleIntegração.Model;
using Microsoft.Xrm.Sdk.Query;

namespace ConsoleIntegração.Model
{
    class Account
    {
        public string TableName = "account";
        public string ColumName = "opportunity";

        public IOrganizationService Service { get; set; }

        public Account(IOrganizationService service)
        {
            this.Service = service;
            Console.Title = "Aplicando desconto na conta";
        }

        public string QuestionNivelDesconto()
        {
            Console.WriteLine("Qual oportunidade você deseja aplicar o desconto? ");
            string idOportunidade = Console.ReadLine();

            return idOportunidade;

        }

        public EntityCollection SearchOpportunityClientId(string opportunityid)
        {
            QueryExpression queryOpportunity = new QueryExpression("opportunity");
            queryOpportunity.ColumnSet.AddColumns("opportunityid", "parentaccountid");
            queryOpportunity.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, opportunityid);


            return this.Service.RetrieveMultiple(queryOpportunity);
        }

        public EntityCollection SearchAccount(Guid account)
        {
            QueryExpression queryAccount = new QueryExpression("account");
            queryAccount.ColumnSet.AddColumns("accountid", "deoli_nivelcliente");
            queryAccount.Criteria.AddCondition("accountid", ConditionOperator.Equal, account);

            return this.Service.RetrieveMultiple(queryAccount);
        }
        public EntityCollection ProductPrice(string opportunityid)
        {
            QueryExpression queryOpportunity = new QueryExpression("opportunity");
            queryOpportunity.ColumnSet.AddColumns("opportunityid", "opportunityid", "discountamount", "totalamount");
            queryOpportunity.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, opportunityid);


            return this.Service.RetrieveMultiple(queryOpportunity);
        }


        public void UpdateAccountDesconto()
        {
            string idOportunidade = this.QuestionNivelDesconto();

            EntityCollection opportunityCRM = this.SearchOpportunityClientId(idOportunidade);

            foreach (Entity opportunitCRM in opportunityCRM.Entities)
            {
                EntityReference opportunityReference = (EntityReference)opportunitCRM["parentaccountid"];
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(opportunityReference.Name);
                EntityCollection accountsCRM = this.SearchAccount(opportunityReference.Id);

                foreach (Entity accountCRM in accountsCRM.Entities)
                {
                    var account = (OptionSetValue)accountCRM["deoli_nivelcliente"];

                    double percent = 0;

                    switch (account.Value)
                    {
                        case 100000000:
                            percent = 0.03;
                            CalculateDiscount(idOportunidade, percent);
                            break;

                        case 100000001:
                            percent = 0.05;
                            CalculateDiscount(idOportunidade, percent);
                            break;

                        case 100000002:
                            percent = 0.07;
                            CalculateDiscount(idOportunidade, percent);
                            break;

                        case 100000003:
                            percent = 0.1;
                            CalculateDiscount(idOportunidade, percent);
                            break;

                        default:
                            Console.WriteLine("Nível do Cliente não encontrado. Para sair pressione Enter. ");
                            break;
                    }

                }
            }


        }

        private void CalculateDiscount(string idOportunidade, double percent)
        {
            EntityCollection productsPrices = this.ProductPrice(idOportunidade);

            foreach (Entity productPrice in productsPrices.Entities)
            {
                Money productPrices = (Money)productPrice["totalamount"];
                double totaldesconto, total = Convert.ToDouble(productPrices.Value);
                Money productDiscount = (Money)productPrice["discountamount"];
                double d = Convert.ToDouble(productDiscount.Value);
                if (d != 0.00)
                {
                    Console.WriteLine("Disconto já aplicado \n\nPressione enter para sair");
                    Console.ReadKey();
                }

                else
                {
                    totaldesconto = total * percent;
                    total = total - (total * percent);
                    Console.WriteLine();
                    Console.Write("Valor sem desconto: ");
                    Console.WriteLine(productPrices.Value.ToString("F2"));
                    Console.WriteLine();
                   
                    Console.WriteLine("Valor com desconto: " + total.ToString("F2"));
                    Console.WriteLine();
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Deseja aplicar o Desconto? Y / N");
                    string option = Console.ReadLine().ToUpper();

                    while (option != "Y" && option != "N")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Por favor digite a opção novamente. \nDeseja aplicar o Desconto? Y para sim e N para não.");
                        option = Console.ReadLine().ToUpper();
                    }


                    switch (option)
                    {
                        case "Y":
                            Account update = new Account(Service);
                            update.UpdateDiscountOpportunity(totaldesconto, idOportunidade);
                            Console.WriteLine("Desconto aplicado e dados atualizados.");
                            Console.WriteLine("Aperte Enter para sair.");
                            Console.ReadKey();

                            break;
                        case "N":
                            Console.WriteLine("Ação abortada. Obrigado! Pressione Enter para sair");
                            Console.ReadKey();
                            break;

                        default:
                            Console.WriteLine("Erro inesperado!");
                            Console.ReadKey();
                            break;

                    }
                }
            }
        }

        public void UpdateDiscountOpportunity(double discount, string idOpportunity)
        {
            Entity opportunity = new Entity(this.ColumName);
            opportunity["discountamount"] = discount;
            opportunity.Id = new Guid(idOpportunity);
            this.Service.Update(opportunity);
        }


    }
}

