using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using System.IO;

namespace FlooringMastery.DATA
{
    public class ProductRepository : IProductRepository
    {
        private string _productFilePath = @"C:\Users\bergs\swcguild\BitBucket\allison-bergstedt-individual-work\Final\FlooringMasteryProject\Data\Products.txt";

        public Products GetProduct(string productName)
        {
            var returnProduct = LoadAllProducts().Where(p => p.ProductType.ToLower() == productName.ToLower()).FirstOrDefault();

            return returnProduct;
        }

        public List<Products> LoadAllProducts()
        {
            List<Products> products = new List<Products>();

            using (StreamReader sr = new StreamReader(_productFilePath))
            {
                sr.ReadLine();
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Products newProductChoice = new Products();

                    string[] columns = line.Split(',');

                    newProductChoice.ProductType = columns[0];
                    newProductChoice.CostPerSquareFoot = decimal.Parse(columns[1]);
                    newProductChoice.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                    products.Add(newProductChoice);
                }
            }

            return products;
        }
    }
}
