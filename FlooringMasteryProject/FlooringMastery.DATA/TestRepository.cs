using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.DATA
{
    public class TestRepository : IOrderRepository
    {
        private static Orders _order = new Orders
        {
            OrderDate = 01012017,
            OrderNumber = 1234,
            CustomerName = "John Doe",
            State = "Ohio",
            TaxRate = 7.25m,
            ProductType = "Carpet",
            Area = 100.00m,
            CostPerSquareFoot = 5.20m,
            LaborCostPerSquareFoot = 2.75m,
            MaterialCost = _order.Area * _order.CostPerSquareFoot,
            LaborCost = _order.Area * _order.LaborCostPerSquareFoot,
            Tax = (_order.MaterialCost + _order.LaborCost) * (_order.TaxRate / 100),
            Total = (_order.MaterialCost + _order.LaborCost + _order.Tax)
        };

        public Orders LoadOrders(int OrderDate)
        {
            if(OrderDate != _order.OrderDate)
            {
                return null;
            }
            else
            {
                return _order;
            }

        }

        public void SaveOrders(Orders order)
        {
            _order = order;
        }
    }
}
