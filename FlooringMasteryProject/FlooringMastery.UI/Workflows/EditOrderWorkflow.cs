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
    public class EditOrderWorkflow
    {
        //order date cannot change
        private string _newName;
        private States _taxState = new States();
        private Products _productType = new Products();
        private List<Products> _productsList = new List<Products>();
        private string _newArea;
        private Orders _newOrder;
        private Orders _originalOrder;
        private string _orderNumber;


        public void Execute()
        {
            AccountManager accountManager = AccountManagerFactory.Create();
            AddOrderResponse response = new AddOrderResponse();

            Console.Clear();
            Console.WriteLine("Edit an order");
            Console.WriteLine("----------------------");

            AskForOrderDateandNumber();
            AskForCustomerName();
            AskToEditState();
            AskToEditProductType();
            AskToEditArea();

            Console.Clear();
            _newOrder = accountManager.DisplayEditedOrder(_originalOrder, _newArea, _taxState, _productType, _newName);
            ConsoleIO.DisplaySingleOrder(_newOrder);

            Console.WriteLine("Would you like to save the changes to your order? Y/N");
            string editOrder = Console.ReadLine();
            if (editOrder.Equals("Y"))
            {
                EditOrderResponse updatedOrder = accountManager.EditOrderResponse(_newOrder);
                Console.WriteLine("Your order was saved!");
                // not sure if I'm saving correctly here
            }
            else
            {
                Menu.Start();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void AskForOrderDateandNumber()
        {
            AccountManager accountManager = AccountManagerFactory.Create();
            AddOrderResponse response = new AddOrderResponse();
            OrderLookupResponse lookupResponse = new OrderLookupResponse();

            Console.Clear();
            // Query user for order date
            Console.Write("\nEnter an order date (MMddyyyy): ");

            string dateInput = Console.ReadLine();

            lookupResponse = accountManager.CheckDateFormat(dateInput);

            if (lookupResponse.Success == false)
            {
                Console.WriteLine(lookupResponse.Message);
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                AskForOrderDateandNumber();
            }
            else
            {
                lookupResponse = accountManager.LookupOrders(dateInput);

                if (lookupResponse.Success == false)
                {
                    Console.WriteLine(lookupResponse.Message);
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Menu.Start();
                }
                else
                {
                    //Query user for order number
                    Console.Write("\nEnter an order number: ");
                    int orderNumber = int.Parse(Console.ReadLine());

                    OrderLookupResponse checkNumberResponse = accountManager.CheckOrderNumber(dateInput, orderNumber.ToString());

                    if (checkNumberResponse.Success == true)
                    {
                        _originalOrder = accountManager.GetSingleOrder(dateInput, orderNumber);
                    }
                    else
                    {
                        Console.WriteLine($"There are no order placed on that order date for {orderNumber}.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        AskForOrderDateandNumber();
                    }
                }
            }
        }

        private void AskForCustomerName()
        {
            Console.Clear();
            ConsoleIO.DisplaySingleOrder(_originalOrder);
            while(true)
            {
                Console.Write("Enter new customer name or just press enter to continue: ");
                string customerName = Console.ReadLine();

                if (customerName == "")
                {
                    _newName = _originalOrder.CustomerName;
                    break;
                }
                else
                {
                    _newName = customerName;
                    break;
                }
            }
        }

        private void AskToEditState()
        {
            StateManager stateManager = StateManagerFactory.Create(); //calls state repo
            AddOrderResponse response = new AddOrderResponse();

            Console.Clear();
            ConsoleIO.DisplaySingleOrder(_originalOrder);
            
            while(true)
            {
                Console.Write("Enter new state or just press enter to continue: ");
                string state = Console.ReadLine();

                if (state == "" || state == null)
                {
                    _taxState = stateManager.GetStateInfo(_originalOrder.State);
                    break;
                }
                else
                {
                    response = stateManager.ValidateState(state.ToLower());
                    if (response.Success == false)
                    {
                        Console.WriteLine("The state entered is not valid. Press any key to continue.");
                        continue;
                    }
                    else
                    {
                        _taxState = stateManager.GetStateInfo(state.ToLower());
                        break;
                    }
                }
            }
        }

        private void AskToEditProductType()
        {
            ProductManager productManager = ProductManagerFactory.Create(); //calls product repo
            AddOrderResponse response = new AddOrderResponse();

            _productsList = productManager.GetProductsToDisplay();
            Console.Clear();
            ConsoleIO.DisplayProductsAvailable(_productsList);
            ConsoleIO.DisplaySingleOrder(_originalOrder);
            
            while(true)
            {
                Console.Write("Enter new product or just press enter to continue: ");
                string productType = Console.ReadLine();
                
                if (productType == "")
                {
                    _productType = productManager.GetProductInfo(_originalOrder.ProductType);
                    break;
                }
                else
                {
                    response = productManager.CheckProduct(productType);
                    if (response.Success == false)
                    {
                        Console.WriteLine("An error occurred with the product entered.  Contact IT.");
                        continue;
                    }
                    else
                    {
                        _productType = productManager.GetProductInfo(productType);
                        break;
                    }
                }
            }
        }

        private void AskToEditArea()
        {
            AccountManager manager = AccountManagerFactory.Create();
            AddOrderResponse response = new AddOrderResponse();

            Console.Clear();
            ConsoleIO.DisplaySingleOrder(_originalOrder);
            while (true)
            {
                Console.Write("Enter new area or just press enter to continue: ");
                string area = Console.ReadLine();

                if (area == "")
                {
                    _newArea = _originalOrder.Area.ToString();
                    break;
                }
                else
                {
                    response = manager.CheckArea(area);
                    if (response.Success == false)
                    {
                        Console.WriteLine("An error occurred with the area entered. Contact IT.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        _newArea = area;
                        break;
                    }
                }
            }
        }
    }
}
