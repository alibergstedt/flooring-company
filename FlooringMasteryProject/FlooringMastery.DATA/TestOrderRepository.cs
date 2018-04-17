using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System.IO;

namespace FlooringMastery.DATA
{
    public class TestOrderRepository : IOrderRepository
    {
        private static string _filePath;

        public TestOrderRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Orders> LoadAllOrders(string orderDate)
        {
            List<Orders> orders = new List<Orders>();

            if(File.Exists(_filePath + "Orders_" + orderDate + ".txt"))
            {
                using (StreamReader sr = new StreamReader(_filePath + "Orders_" + orderDate + ".txt"))
                {

                    sr.ReadLine();
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Orders newOrder = new Orders();

                        string[] columns = line.Split(',');

                        newOrder.OrderNumber = int.Parse(columns[0]);
                        newOrder.CustomerName = columns[1];
                        newOrder.State = columns[2];
                        newOrder.TaxRate = decimal.Parse(columns[3]);
                        newOrder.ProductType = columns[4];
                        newOrder.Area = decimal.Parse(columns[5]);
                        newOrder.CostPerSquareFoot = decimal.Parse(columns[6]);
                        newOrder.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                        newOrder.MaterialCost = decimal.Parse(columns[8]);
                        newOrder.LaborCost = decimal.Parse(columns[9]);
                        newOrder.Tax = decimal.Parse(columns[10]);
                        newOrder.Total = decimal.Parse(columns[11]);

                        newOrder.OrderDate = orderDate;
                        orders.Add(newOrder);
                    }
                }
            }
            else
            {
                return orders;
            }
            return orders;
        }

        public Orders GetSingleOrder(string orderDate, int orderNumber)
        {
            Orders result = null;
            result = LoadAllOrders(orderDate).FirstOrDefault(p => p.OrderNumber == orderNumber);
            return result;
        }

        public void AddOrder(Orders order)
        {
            AddOrderResponse response = new AddOrderResponse();

            List<Orders> orderToAdd = LoadAllOrders(order.OrderDate);

            int newOrderNumber = orderToAdd.Count + 1;
            order.OrderNumber = newOrderNumber;

            orderToAdd.Add(order);

            string fileName = $"Orders_{order.OrderDate:MMddyyyy}.txt";
            SaveOrders(orderToAdd, fileName);
        }

        public void EditOrder(Orders updatedOrder)
        {
            var orderList = LoadAllOrders(updatedOrder.OrderDate);

            foreach (var orders in orderList)
            {
                if (orders.OrderNumber == updatedOrder.OrderNumber)
                {
                    orderList.Remove(orders);
                    break;
                }
            }
            orderList.Add(updatedOrder);

            string fileName = $"Orders_{updatedOrder.OrderDate:MMddyyyy}.txt"; 
            SaveOrders(orderList, fileName);

        }

        public void RemoveOrder(Orders order)
        {
            
            var orderList = LoadAllOrders(order.OrderDate);
            
            foreach(var orders in orderList)
            {
                if(orders.OrderNumber == order.OrderNumber)
                {
                    orderList.Remove(orders);
                    break;
                }
            }
            
            string fileName = $"Orders_{order.OrderDate:MMddyyyy}.txt";
            SaveOrders(orderList, fileName);
            
        }

        private string CreateCsvForOrder(Orders order)
        {
            return $"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}";
        }

        public void SaveOrders(List<Orders> orderToAdd, string fileName)
        {
            if (File.Exists(_filePath + fileName))
            {
                File.Delete(_filePath + fileName);
            }
            using (StreamWriter sw = new StreamWriter(_filePath + fileName))
            {
                sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                foreach (var o in orderToAdd)
                {
                    string line = CreateCsvForOrder(o);

                    sw.WriteLine(line);
                }
            }
        }
    }
}
