using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        private Orders order;
        private AddOrderResponse response;
        private OrderLookupResponse lookupResponse;
        private RemoveOrderResponse removeOrderResponse;
        private AccountManager accountManager = AccountManagerFactory.Create();
        private string orderDate;
        private string orderNumber;


        public void Execute()
        {
            Console.Clear();

            Console.WriteLine("Remove an order");
            Console.WriteLine("----------------------");

            GetOrderDateAndOrderNumber();
            RemoveOrder();
        }

        private void GetOrderDateAndOrderNumber()
        {
            while (true)
            {
                // Query user for order date
                Console.Write("\nEnter an order date: ");
                orderDate = Console.ReadLine();

                lookupResponse = accountManager.CheckDateFormat(orderDate);

                if (lookupResponse.Success == false)
                {
                    Console.WriteLine(lookupResponse.Message);
                    continue;
                }
                while (true)
                {
                    //Query user for order number
                    Console.Write("Enter an order number: ");
                    orderNumber = Console.ReadLine();

                    lookupResponse = accountManager.CheckOrderNumber(orderDate, orderNumber);

                    if (lookupResponse.Success == false)
                    {
                        Console.WriteLine(lookupResponse.Message);
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        Menu.Start();
                    }
                    else
                    {
                        order = accountManager.GetSingleOrder(orderDate, Convert.ToInt32(orderNumber));
                        break;
                    }
                }
                break;
            }
        }

        private void RemoveOrder()
        {
            ConsoleIO.DisplaySingleOrder(order);

            while (true)
            {
                Console.WriteLine("Would you like to delete this order? Y/N");
                string removeOrder = Console.ReadLine().ToUpper();
                if (removeOrder.ToUpper() == "Y")
                {
                    break;
                }
                else if (removeOrder.ToUpper() == "N")
                {
                    Console.WriteLine("Remove order was cancelled. Press any key to return to the main manu: ");
                    Console.ReadKey();
                    Menu.Start();
                }
            }
            removeOrderResponse = accountManager.RemoveOrderResponse(orderDate, Convert.ToInt32(orderNumber));
            if (removeOrderResponse.Success == true)
            {
                Console.WriteLine(removeOrderResponse.Message);
                Console.ReadKey();
                Menu.Start();
            }
            else
            {
                Console.WriteLine("Something went wrong. Contact IT");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
    }
}