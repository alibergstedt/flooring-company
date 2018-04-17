using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class OrderLookupResponse : Response
    {
        public Orders Order { get; set; }
        public List<Orders> GetOrders { get; set; }
    }
}
