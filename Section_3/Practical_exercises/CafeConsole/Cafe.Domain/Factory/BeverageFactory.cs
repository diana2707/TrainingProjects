using Cafe.Domain.Enums;
using Cafe.Domain.Factory;
using Cafe.Domain.Models;

namespace Cafe.Infrastructure.Factories
{
    public class BeverageFactory : IBeverageFactory
    {
        public IBeverage Create(BeverageType bevarageType, IBeverage baseBeverage = null, SyrupFlavourType syrupFlavour = SyrupFlavourType.None)
        {
            return bevarageType switch
            {
                BeverageType.Espresso => new Espresso(),
                BeverageType.Tea => new Tea(),
                BeverageType.HotChocolate => new HotChocolate(),
                BeverageType.Milk when baseBeverage != null => new MilkDecorator(baseBeverage),
                BeverageType.Syrup when baseBeverage != null && syrupFlavour != 0 => new SyrupDecorator(baseBeverage, syrupFlavour),
                BeverageType.ExtraShot when baseBeverage != null => new ExtraShotDecorator(baseBeverage),
                _ => throw new ArgumentException("Invalid beverage type or missing base beverage for add-on."),
            };

        }
    }
}
