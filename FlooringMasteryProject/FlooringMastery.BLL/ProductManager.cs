using FlooringMastery.BLL.OrderRules;
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
    public class ProductManager
    {
        IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public AddOrderResponse CheckProduct(string productType)
        {
            AddOrderResponse response = new AddOrderResponse();
            ProductManager productRepo = ProductManagerFactory.Create();
            AddOrderRules rules = new AddOrderRules();

            response.Product = _productRepository.GetProduct(productType.ToLower());

            if(response.Product == null)
            {
                response.Success = false;
                response.Message = $"We do not sell product type \"{productType}\".";
                return response;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public Products GetProductInfo(string productType)
        {
            Products returnProduct = _productRepository.GetProduct(productType.ToLower());

            return returnProduct;
        }

        public List<Products> GetProductsToDisplay()
        {
            List<Products> returnProducts = _productRepository.LoadAllProducts();

            return returnProducts;
        }
    }
}
