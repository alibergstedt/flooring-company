using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.DATA
{
    public class ProdProductRepository : IProductRepository
    {
        public Products GetProduct(string productName)
        {
            throw new NotImplementedException();
        }

        public List<Products> LoadAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
