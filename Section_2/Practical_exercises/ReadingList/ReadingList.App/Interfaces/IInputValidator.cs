using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.App.Interfaces
{
    public interface IInputValidator
    {
        public int ValidateMenuOption(string input, int minOption, int maxOption);
    }
}
