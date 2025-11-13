using Cafe.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs
{
    public class BeverageDetails
    {
        public BeverageType BaseBeverage { get; init; }
        public BeverageType[] AddOns { get; init; } = [];
        public SyrupFlavourType SyrupFlavour { get; init; } = SyrupFlavourType.None;
    }
}
