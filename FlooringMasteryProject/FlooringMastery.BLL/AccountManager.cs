using FlooringMastery.BLL.OrderRules;
using FlooringMastery.DATA;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public class AccountManager
    {
        private IOrderRepository _orderRepository;

        public AccountManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderLookupResponse LookupOrders(string orderDate)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            response.GetOrders = _orderRepository.LoadAllOrders(orderDate);

            /*foreach (var order in response.GetOrders)
            {
                if (order.OrderDate == orderDate)
                {
                    response.Success = true;
                }
            }*/

            if (response.GetOrders.Count == 0)
            {
                response.Success = false;
                response.Message = $"There are no orders placed on {orderDate}.";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public OrderLookupResponse LookupSingleOrder(string DateInput, int OrderNumber)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            response.Order = _orderRepository.GetSingleOrder(DateInput, OrderNumber);

            if(response.Order == null)
            {
                response.Success = false;
                response.Message = $"There were no orders placed on {DateInput} with the order number {OrderNumber}. ";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public Orders GetSingleOrder(string orderDate, int orderNumber)
        {
            int parsedOrderNumber;
            int.TryParse(orderNumber.ToString(), out parsedOrderNumber);
            Orders returnOrder = new Orders();

            returnOrder = _orderRepository.GetSingleOrder(orderDate, parsedOrderNumber);
            return returnOrder;
        }

        public AddOrderResponse AddOrderResponse(Orders order)
        {
            AddOrderResponse response = new AddOrderResponse();

            try
            {
                _orderRepository.AddOrder(order);
                response.Success = true;
                response.Message = "Your order was added.";
            }
            catch
            {
                response.Success = false;
                response.Message = "An error occurred. Your order was not added.";
            }

            return response;
        }

        public EditOrderResponse EditOrderResponse(Orders updatedOrder)
        {
            EditOrderResponse response = new EditOrderResponse();

            _orderRepository.EditOrder(updatedOrder);
            response.Success = true;

            return response;
        }

        public RemoveOrderResponse RemoveOrderResponse(string orderDate, int orderNumber)
        {
            RemoveOrderResponse response = new RemoveOrderResponse();

            response.Orders = _orderRepository.GetSingleOrder(orderDate, orderNumber);

            if (response.Orders == null)
            {
                response.Success = false;
                response.Message = $"{orderNumber} is not a valid order number.";
            }
            else
            {
                _orderRepository.RemoveOrder(response.Orders);
                response.Success = true;
                response.Message = "Your order was deleted. Press any key to return to the main menu:";
            }

            return response;
        }

        public AddOrderResponse CheckOrderDate(string orderDate)
        {
            AddOrderResponse response = new AddOrderResponse();
            AddOrderRules addOrderRules = new AddOrderRules();

            response = addOrderRules.CheckOrderDate(orderDate);
            return response;
        }

        public OrderLookupResponse CheckDateFormat(string date)
        {
            OrderLookupResponse response = new OrderLookupResponse();
            AddOrderRules addOrderRules = new AddOrderRules();

            response = addOrderRules.CheckDateFormat(date);
            return response;
        }

        public OrderLookupResponse CheckOrderNumber(string OrderDate, string OrderNumber)
        {
            int newOrderNumber;
            int.TryParse(OrderNumber, out newOrderNumber);
            OrderLookupResponse response = new OrderLookupResponse();

            response.Order = _orderRepository.GetSingleOrder(OrderDate, newOrderNumber);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "There were no orders matching that order number";
                return response;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public AddOrderResponse CheckCustomerName(string name)
        {
            AddOrderResponse response = new AddOrderResponse();
            AddOrderRules addOrderRules = new AddOrderRules();

            response = addOrderRules.CheckCustomerName(name);

            return response;
        }

        public AddOrderResponse CheckArea(string Area)
        {
            AddOrderResponse response = new AddOrderResponse();
            AddOrderRules addOrderRules = new AddOrderRules();
            decimal newArea;
            decimal.TryParse(Area, out newArea);

            response = addOrderRules.CheckArea(newArea);

            return response;
        }

        public decimal GenerateTotal(decimal tax, decimal laborCost, decimal materialCost)
        {
            decimal returnTotal = (tax + laborCost + materialCost);

            return returnTotal;
        }

        public decimal GenerateTax(decimal taxRate, decimal laborCost, decimal materialCost)
        {
            decimal returnTax = (laborCost + materialCost) * (taxRate / 100);

            return returnTax;
        }

        public decimal GenerateLaborCost(decimal area, decimal laborCostPerSquareFoot)
        {
            decimal returnLaborCost = area * laborCostPerSquareFoot;

            return returnLaborCost;
        }

        public decimal GenerateMaterialCost(decimal area, decimal costPerSquareFoot)
        {
            decimal returnCost = area * costPerSquareFoot;

            return returnCost;
        }

        public int GetNewOrderNumber(string orderDate)
        {
            List<Orders> orders = new List<Orders>();
            int number;
            orders = _orderRepository.LoadAllOrders(orderDate);
            if (orders.Count == 0)
            {
                number = 1;
                return number;
            }
            var latestOrder = orders.OrderByDescending(i => i.OrderNumber).First();
            number = latestOrder.OrderNumber + 1;

            return number;
        }

        public Orders DisplayOrderToAdd(string orderDate, string customerName, States stateAbbreviation, decimal area, Products product)
        {
            Orders order = new Orders();
            order.OrderNumber = GetNewOrderNumber(orderDate);
            order.CustomerName = customerName;
            order.Area = area;
            order.OrderDate = orderDate;
            order.State = stateAbbreviation.StateName;
            order.ProductType = product.ProductType;
            order.TaxRate = stateAbbreviation.TaxRate;
            order.CostPerSquareFoot = product.CostPerSquareFoot;
            order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
            order.MaterialCost = GenerateMaterialCost(area, product.CostPerSquareFoot);
            order.LaborCost = GenerateLaborCost(area, product.LaborCostPerSquareFoot);
            order.Tax = GenerateTax(stateAbbreviation.TaxRate, order.LaborCost, order.MaterialCost);
            order.Total = GenerateTotal(order.Tax, order.LaborCost, order.MaterialCost);

            return order;
        }

        public Orders DisplayEditedOrder(Orders order, string area, States stateInfo, Products product, string customerName)
        {
            decimal areaInput;
            decimal.TryParse(area, out areaInput);

            Orders editOrder = new Orders();
            editOrder.CustomerName = customerName;
            editOrder.OrderDate = order.OrderDate;
            editOrder.OrderNumber = order.OrderNumber;
            editOrder.Area = areaInput;
            editOrder.State = stateInfo.StateName;
            editOrder.ProductType = product.ProductType;
            editOrder.TaxRate = stateInfo.TaxRate;
            editOrder.CostPerSquareFoot = product.CostPerSquareFoot;
            editOrder.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
            editOrder.MaterialCost = GenerateMaterialCost(areaInput, product.CostPerSquareFoot);
            editOrder.LaborCost = GenerateLaborCost(areaInput, product.LaborCostPerSquareFoot);
            editOrder.Tax = GenerateTax(stateInfo.TaxRate, editOrder.LaborCost, editOrder.MaterialCost);
            editOrder.Total = GenerateTotal(editOrder.Tax, editOrder.LaborCost, editOrder.MaterialCost);

            return editOrder;
        }

    }
}
