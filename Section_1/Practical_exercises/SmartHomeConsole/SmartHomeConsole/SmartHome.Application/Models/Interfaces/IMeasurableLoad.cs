using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Models.Interfaces
{
    public interface IMeasurableLoad
    {
        public double CurrentWatts { get; }
        public double TotalWh { get; }
        public void ResetEnergy();
    }
}
