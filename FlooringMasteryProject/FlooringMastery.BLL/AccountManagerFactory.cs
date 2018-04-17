using FlooringMastery.DATA;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public static class AccountManagerFactory
    {
        public static AccountManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            string filePath = ConfigurationManager.AppSettings["FilePath"].ToString();
            switch (mode)
            {
                case "TestRepo":
                    return new AccountManager(new TestOrderRepository(filePath));
                case "ProdRepo":
                    return new AccountManager(new ProdOrderRepository(filePath));
                default:
                    throw new Exception("Mode value in app config is not valid. See IT");
            }
        }
    }
}
