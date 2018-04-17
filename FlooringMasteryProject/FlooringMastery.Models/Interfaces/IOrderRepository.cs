using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Orders> LoadAllOrders(string OrderDate);
        Orders GetSingleOrder(string OrderDate, int orderNumber);
        void AddOrder(Orders order);
        void EditOrder(Orders Order);
        void RemoveOrder(Orders orderNumber);
    }
}
