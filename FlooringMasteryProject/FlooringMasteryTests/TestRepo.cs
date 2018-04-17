using FlooringMastery.BLL;
using FlooringMastery.DATA;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryTests
{
    [TestFixture]
    public class TestRepo
    {
        public static string _filePath;

        //can lookuporders
        [Test]
        public void CanLoadTestOrderData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            OrderLookupResponse response = manager.LookupOrders("06012013");

            Assert.IsTrue(response.Success);
        }

        //can lookup single order
        [Test]
        public void CanLookUpSingleOrder()
        {
            AccountManager manager = AccountManagerFactory.Create();

            OrderLookupResponse response = manager.LookupSingleOrder("06012013", 1);

            Assert.IsTrue(response.Success);
        }

        //can add order
        [Test]
        public void CanAddOrder()
        {
            AccountManager manager = AccountManagerFactory.Create();
            AddOrderResponse response = new AddOrderResponse();

            Orders order = new Orders()
            {
                OrderDate = "07012017",
                OrderNumber = 1,
                CustomerName = "Jane Doe",
                State = "PA",
                TaxRate = 6.75M,
                ProductType = "Laminate",
                Area = 230.00M,
                CostPerSquareFoot = 1.75M,
                LaborCostPerSquareFoot = 2.10M,
                MaterialCost = 402.50M,
                LaborCost = 483M,
                Tax = 59.77M,
                Total = 945.27M
            };

            response = manager.AddOrderResponse(order);

            Assert.IsTrue(response.Success);
        }

        //can edit order
        [Test]
        public void CanEditOrder()
        {
           AccountManager manager = AccountManagerFactory.Create();
           Orders originalOrder = new Orders()
           {
               OrderDate = "07022017",
               OrderNumber = 1,
               CustomerName = "Jane Doe",
               State = "OH",
               TaxRate = 7.25M,
               ProductType = "Tile",
               Area = 120.00M,
               CostPerSquareFoot = 3.50M,
               LaborCostPerSquareFoot = 4.15M,
               MaterialCost = 420M,
               LaborCost = 498M,
               Tax = 66.555M,
               Total = 984.555M
           };
           Orders editedOrder = new Orders()
           {
               OrderDate = "07022017",
               OrderNumber = 1,
               CustomerName = "John Doe",
               State = "PA",
               TaxRate = 7.25M,
               ProductType = "Tile",
               Area = 120.00M,
               CostPerSquareFoot = 3.50M,
               LaborCostPerSquareFoot = 4.15M,
               MaterialCost = 420M,
               LaborCost = 498M,
               Tax = 66.555M,
               Total = 984.555M
           };
 
           EditOrderResponse response = manager.EditOrderResponse(editedOrder);
 
           Assert.IsTrue(response.Success);
        }


        //can remove order
        [Test]
        public void CanRemoveAnOrder()
        {
            AccountManager manager = AccountManagerFactory.Create();
            Orders order = new Orders()
            {
                OrderDate = "07012017",
                OrderNumber = 1,
                CustomerName = "Jane Doe",
                State = "PA",
                TaxRate = 6.75M,
                ProductType = "Laminate",
                Area = 230.00M,
                CostPerSquareFoot = 1.75M,
                LaborCostPerSquareFoot = 2.10M,
                MaterialCost = 402.50M,
                LaborCost = 483M,
                Tax = 59.77M,
                Total = 945.27M
            };
            RemoveOrderResponse response = manager.RemoveOrderResponse(order.OrderDate, order.OrderNumber);

            Assert.IsTrue(response.Success);
        }

        //can load products
        [Test]
        public void CanLoadProductTest()
        {
            var testRepo = new ProductRepository();
            var productList = testRepo.LoadAllProducts();

            var product = productList.FirstOrDefault(p => p.ProductType == "Carpet");


            Assert.AreEqual(product.ProductType, "Carpet");
        }

        //can load states
        [Test]
        public void CanLoadStateTaxTest()
        {
            var testRepo = new StateRepository();
            var stateTaxList = testRepo.LoadAllStates();

            var state = stateTaxList.FirstOrDefault(s => s.StateName == "Ohio");

            Assert.AreEqual(state.StateName, "Ohio");
        }
    }
}
