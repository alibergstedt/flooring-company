using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class AccountAddOrderResponse : Response
    {
        public Orders Orders { get; set; }
    }
}
