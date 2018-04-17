using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public class ConsoleIO
    {
        public static void DisplayOrderDetails(List<Orders> orders)
        {
            foreach(var order in orders)
            {
                Console.WriteLine("***************************************");
                Console.WriteLine($"\nCustomer Name: {order.CustomerName}");
                Console.WriteLine($"Order Number: {order.OrderNumber}");
                Console.WriteLine($"State: {order.State}");
                Console.WriteLine($"Product: {order.ProductType}");
                Console.WriteLine($"Materials: {order.MaterialCost}c");
                Console.WriteLine($"Cost per Sq. Ft.: {order.CostPerSquareFoot}c");
                Console.WriteLine($"Labor Cost per Sq. Ft.: {order.LaborCostPerSquareFoot}c");
                Console.WriteLine($"Labor: {order.LaborCost}c");
                Console.WriteLine($"Tax: {order.Tax}c");
                Console.WriteLine($"Total: {order.Total}c");
                Console.WriteLine("\n***************************************");
            }
        }

        public static void DisplaySingleOrder(Orders order)
        {
                Console.WriteLine("***************************************");
                Console.WriteLine($"\nCustomer Name: {order.CustomerName}");
                Console.WriteLine($"Order Number: {order.OrderNumber}");
                Console.WriteLine($"State: {order.State}");
                Console.WriteLine($"Product: {order.ProductType}");
                Console.WriteLine($"Materials: {order.MaterialCost}c");
                Console.WriteLine($"Cost per Sq. Ft.: {order.CostPerSquareFoot}c");
                Console.WriteLine($"Labor Cost per Sq. Ft.: {order.LaborCostPerSquareFoot}c");
                Console.WriteLine($"Labor: {order.LaborCost}c");
                Console.WriteLine($"Tax: {order.Tax}c");
                Console.WriteLine($"Total: {order.Total}c");
                Console.WriteLine("\n***************************************");
        }

        public static void DisplayProductsAvailable(List<Products> productsList)
        {
            Console.WriteLine("Available Products and Pricing");
            foreach (var products in productsList)
            {
                Console.WriteLine("***************************************");
                Console.WriteLine($"\nProductType :{products.ProductType}");
                Console.WriteLine($" Cost Per Square Foot :{products.CostPerSquareFoot}");
                Console.WriteLine($" Labor Cost Per Square Foot :{products.LaborCostPerSquareFoot}");
                Console.WriteLine("\n***************************************");
            }
        }
    }
}
