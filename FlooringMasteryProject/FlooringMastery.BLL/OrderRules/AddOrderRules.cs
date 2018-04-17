using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace FlooringMastery.BLL.OrderRules
{
    public class AddOrderRules
    {
        public AddOrderResponse CheckOrderDate(string orderDate)
        {
            AddOrderResponse response = new AddOrderResponse();

            //check if blank
            if (orderDate == "")
            {
                response.Success = false;
                response.Message = "Error: The Order Date cannot be blank.";
                return response;
            }

            //Check if date is in correct format
            if (orderDate.Length != 8 || orderDate.All(char.IsLetter))
            {
                response.Success = false;
                response.Message = "Error: The Order Date must be in the format MMddyyyy.";
                return response;
            }

            // Checking if date is in future
            CultureInfo provider = CultureInfo.InvariantCulture;

            if (DateTime.ParseExact(orderDate, "MMddyyyy", provider) <= DateTime.Today)
            {
                response.Success = false;
                response.Message = "Order date must be in the future.";
                return response;
            }

            response.Success = true;
            return response;
        }

        public OrderLookupResponse CheckDateFormat(string orderDate)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            //check if blank
            if (orderDate == "")
            {
                response.Success = false;
                response.Message = "Error: The Order Date cannot be blank.";
                return response;
            }

            //Check if date is in correct format
            if (orderDate.Length != 8 || orderDate.All(char.IsLetter))
            {
                response.Success = false;
                response.Message = "Error: The Order Date must be in the format MMddyyyy.";
                return response;
            }

            response.Success = true;
            return response;
        }

        public AddOrderResponse CheckCustomerName(string customerName)
        {
            AddOrderResponse response = new AddOrderResponse();

            if (customerName == "")
            {
                response.Success = false;
                response.Message = "Customer name cannot be blank";
                return response;
            }

            //Check for special characters
            var regexItem = new Regex("^[a-zA-Z0-9 ]+$");
            if (!regexItem.IsMatch(customerName))
            {
                response.Success = false;
                response.Message = "Customer name cannot contain special characters";
                return response;
            }

            response.Success = true;
            return response;
        }

        public AddOrderResponse CheckArea(decimal area)
        {
            AddOrderResponse response = new AddOrderResponse();

            if (area < 0 || area < 100)
            {
                response.Success = false;
                response.Message = "The area must be a positive number and greater than 100sq ft.";
                return response;
            }

            response.Success = true;
            return response;
        }
    }
}
