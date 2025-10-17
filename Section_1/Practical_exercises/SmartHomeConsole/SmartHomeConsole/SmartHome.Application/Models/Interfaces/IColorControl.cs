using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Models.Interfaces
{
    public interface IColorControl
    {
        public string Color { get; }
        public void SetColor(string color);
    }
}
