using FlooringMastery.DATA;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public static class ProductManagerFactory
    {
        public static ProductManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TestRepo":
                    return new ProductManager(new ProductRepository());
                case "ProdRepo":
                    return new ProductManager(new ProductRepository());
                default:
                    throw new Exception("Mode value in app config is not valid. See IT");
            }
        }
    }
}
