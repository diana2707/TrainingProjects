using System;
using System.Collections.Generic;
using ReadingList.Domain.Enums;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadingList.Domain;

namespace ReadingList.App.Interfaces
{
    public interface IInputValidator
    {
        public Result<CommandType> ValidateCommand(string command);
    }
}
