using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Models.Interfaces
{
    public interface IDimmable
    {
        public int Brightness { get; }
        public void SetBrightness(int value);
    }
}
