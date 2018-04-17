using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.BLL.OrderRules
{
    public class EditOrderRules : IEditOrder
    {
        public EditOrderResponse EditOrder(Orders updatedOrder)
        {
            EditOrderResponse response = new EditOrderResponse();

            response.Orders = updatedOrder;
            response.Success = true;

            return response;
        }
    }
}
