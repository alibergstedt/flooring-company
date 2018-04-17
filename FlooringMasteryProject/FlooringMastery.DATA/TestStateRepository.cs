using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.DATA
{
    public class TestStateRepository : IStateRepository
    {
        public States GetState(string stateAbbreviation)
        {
            throw new NotImplementedException();
        }

        public List<States> LoadAllStates()
        {
            throw new NotImplementedException();
        }
    }
}
