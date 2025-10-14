using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Models.Interfaces
{
    public interface ITemperatureControl
    {
        double TargetCelsius { get; }
        void SetTarget(double celsius);
    }
}
