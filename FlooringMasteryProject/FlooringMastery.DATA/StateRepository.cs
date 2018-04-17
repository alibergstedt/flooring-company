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
    public class StateRepository : IStateRepository
    {
        private string _stateTaxFilePath = @"C:\Users\bergs\swcguild\BitBucket\allison-bergstedt-individual-work\Final\FlooringMasteryProject\Data\Taxes.txt";

        public States GetState(string stateAbbreviation)
        {
            var state = LoadAllStates().FirstOrDefault(s => stateAbbreviation.ToLower() == s.StateAbbreviation.ToLower());
            return state;
        }

        public List<States> LoadAllStates()
        {
            List<States> stateTaxRate = new List<States>();

            using (StreamReader sr = new StreamReader(_stateTaxFilePath))
            {
                sr.ReadLine();
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    States newStateTax = new States();

                    string[] columns = line.Split(',');

                    newStateTax.StateAbbreviation = columns[0];
                    newStateTax.StateName = columns[1];
                    newStateTax.TaxRate = decimal.Parse(columns[2]);

                    stateTaxRate.Add(newStateTax);
                }
            }

            return stateTaxRate;
        }
    }
}
