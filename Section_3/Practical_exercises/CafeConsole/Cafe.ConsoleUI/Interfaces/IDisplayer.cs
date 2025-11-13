using Cafe.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.ConsoleUI.Interfaces
{
    public interface IDisplayer
    {
        public void DisplayAppTitle();
        public void DisplayMainMenu();
        public void DisplayAddOnsMenu();
        public void DisplayPricingPolicyMenu();
        public void DisplayReceipt(Receipt receipt);
        public void DisplayContinueOrderingMenu();
        public void DisplayErrorMessage(string message);
        public string GetUserInput(string userPrompt);
        public void DisplayExitMessage();
        public void Clear();
    }
}
