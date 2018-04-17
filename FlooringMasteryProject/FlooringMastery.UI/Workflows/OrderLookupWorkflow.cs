using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.Workflows
{
    public class OrderLookupWorkflow
    {
        public void Execute()
        {
            AccountManager manager = AccountManagerFactory.Create();
            AddOrderResponse response = new AddOrderResponse();
            OrderLookupResponse lookupResponse = new OrderLookupResponse();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Lookup order");
                Console.WriteLine("----------------------");
                Console.Write("\nEnter an order date (MMddyyyy): ");

                string dateInput = Console.ReadLine();

                OrderLookupResponse dateLookupResponse = manager.CheckDateFormat(dateInput);

                if (dateLookupResponse.Success == false)
                {
                    Console.WriteLine(dateLookupResponse.Message);
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    continue;
                }

                Console.Write("\nEnter an order number: ");
                int orderNumber = Convert.ToInt32(Console.ReadLine());

                lookupResponse = manager.LookupSingleOrder(dateInput, orderNumber);

                if (lookupResponse.Success == false)
                {
                    Console.WriteLine("An error occurred: ");
                    Console.WriteLine(lookupResponse.Message);
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    ConsoleIO.DisplaySingleOrder(lookupResponse.Order);
                    break;
                }                
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
