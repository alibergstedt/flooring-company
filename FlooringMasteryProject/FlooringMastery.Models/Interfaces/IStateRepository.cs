using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IStateRepository
    {
        List<States> LoadAllStates();
        States GetState(string stateAbbreviation);
    }
}
