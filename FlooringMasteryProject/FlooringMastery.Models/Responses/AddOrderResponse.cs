using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class AddOrderResponse : Response
    {
        public Orders Orders { get; set; }
        public List<Products> ProductsList { get; set; }
        public States State { get; set; }
        public Products Product { get; set; }
    }
}
