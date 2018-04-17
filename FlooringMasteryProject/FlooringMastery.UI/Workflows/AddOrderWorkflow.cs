using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.Workflows
{
    public class AddOrderWorkflow
    {
        private string _orderDate;
        private string _customerName;
        private decimal _area;
        private States _stateAbbreviation;
        private Products _productType;
        private AddOrderResponse response;
        private AccountManager manager = AccountManagerFactory.Create();
        private ProductManager productManager = ProductManagerFactory.Create();
        private StateManager stateManager = StateManagerFactory.Create();

        public void Execute()
        {
            Console.Clear();
            AddOrderResponse response = new AddOrderResponse();

            Console.WriteLine("Add an order");
            Console.WriteLine("----------------------");

            GetOrderDate();
            GetCustomerName();
            GetState();
            GetProductType();
            GetArea();

            response.Orders = manager.DisplayOrderToAdd(_orderDate, _customerName, _stateAbbreviation, _area, _productType);
            ConsoleIO.DisplaySingleOrder(response.Orders);

            while(true)
            {
                Console.WriteLine("Would you like to place the new order? Y/N");
                string addOrder = Console.ReadLine();
                if (addOrder.ToUpper() == "Y")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Order Cancelled. Press any key to continue...");
                    Console.ReadKey();
                    Menu.Start();
                }

            }

            manager.AddOrderResponse(response.Orders);
            Console.WriteLine("Your order was saved! Press any key to continue.");
            Console.ReadKey();
        }

        private void GetOrderDate()
        {
            Console.Clear();
            while(true)
            {
                Console.Write("\nEnter a date for new order (MMDDYYYY, must be in future): ");
                string orderDate = Console.ReadLine();

                response = manager.CheckOrderDate(orderDate);
                if (response.Success == false)
                {
                    Console.WriteLine(response.Message);
                    continue;
                }
                else
                {
                    _orderDate = orderDate;
                    break;
                }
            }
        }

        private void GetCustomerName()
        {
            Console.Clear();
            while(true)
            {
                Console.Write("\nEnter a customer name for new order: ");
                string customerName = Console.ReadLine();

                response = manager.CheckCustomerName(customerName);

                if (response.Success == false)
                {
                    Console.WriteLine(response.Message);
                    continue;
                }
                else
                {
                    _customerName = customerName;
                    break;
                }
            }

        }

        private void GetState()
        {
            Console.Clear();
            while(true)
            {
                Console.Write("\nEnter a US State for the new order: ");
                string state = Console.ReadLine();

                response = stateManager.ValidateState(state.ToLower());
            
                if (response.Success == false)
                {
                    Console.WriteLine(response.Message);
                    continue;
                }
                else
                {
                    _stateAbbreviation = stateManager.GetStateInfo(state);
                    break;
                }
            }
        }

        private void GetProductType()
        {
            while(true)
            {

                Console.Clear();
                response.ProductsList = productManager.GetProductsToDisplay();

                ConsoleIO.DisplayProductsAvailable(response.ProductsList);

                Console.Write("\nSelect a product to order from the Product List above: ");
                string productType = Console.ReadLine();
                response = productManager.CheckProduct(productType.ToLower());

                if (response.Success == false)
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue: ");
                    Console.ReadLine();
                    continue;
                }
                else
                {
                    _productType = productManager.GetProductInfo(productType.ToLower());
                    break;
                }
            }
        }

        private void GetArea()
        {
            Console.Clear();
            while (true)
            {
                Console.Write("\nEnter the number of square feet for new order: ");
                //decimal area = decimal.Parse(Console.ReadLine());
                decimal returnDecimal;
                string area = Console.ReadLine();
                decimal.TryParse(area, out returnDecimal);

                response = manager.CheckArea(returnDecimal.ToString());

                if (response.Success == false)
                {
                    Console.WriteLine(response.Message);
                    continue;
                }
                else
                {
                    _area = returnDecimal;
                    break;
                }
            }
        }      
    }
}
