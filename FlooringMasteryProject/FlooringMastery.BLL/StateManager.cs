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
    public class StateManager
    {
        private IStateRepository _stateRepository;

        public StateManager(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public AddOrderResponse ValidateState(string stateAbbreviation)
        {
            AddOrderResponse response = new AddOrderResponse();

            StateManager stateRepo = StateManagerFactory.Create();

            if (stateAbbreviation.Length != 2 || !stateAbbreviation.All(char.IsLetter))
            {
                response.Success = false;
                response.Message = "You have entered an invalid State format (must be 2-letter abbreviation).";
                return response;
            }

            response.State = _stateRepository.GetState(stateAbbreviation.ToLower());

            if (response.State == null)
            {
                response.Success = false;
                response.Message = $"We do not sell to {stateAbbreviation}. Please enter state:";
            }

            else
            {
                response.Success = true;
            }
            
            return response;
        }

        public States GetStateInfo(string stateAbbreviation)
        {
            States returnTax = _stateRepository.GetState(stateAbbreviation.ToLower());

            return returnTax;
        }
    }
}
