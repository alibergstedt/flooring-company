using FlooringMastery.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public static class Menu
    {
        public static void Start()  //made static because there will only be one menu start program running
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("Flooring Program Menu");

                Console.WriteLine("\n1. Display Order Details");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");
                Console.WriteLine("5. Quit");

                Console.WriteLine("\n*********************************");
                Console.Write("\nEnter Selection: ");

                string userinput = Console.ReadLine();

                switch(userinput)
                {
                    case "1":
                        OrderLookupWorkflow lookupWorkflow = new OrderLookupWorkflow();
                        lookupWorkflow.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow addOrderWorkflow = new AddOrderWorkflow();
                        addOrderWorkflow.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow editOrderWorkflow = new EditOrderWorkflow();
                        editOrderWorkflow.Execute();
                        break;
                    case "4":
                        RemoveOrderWorkflow removeOrderWorflow = new RemoveOrderWorkflow();
                        removeOrderWorflow.Execute();
                        break;
                    case "5":
                        return;
                }
            }
        }
    }
}
