using Cafe.Domain.Enums;
using Cafe.Domain.Factories.Beverage;
using Cafe.Domain.Models;

namespace Cafe.Infrastructure.Factories
{
    public class BeverageFactory : IBeverageFactory
    {
        public IBeverage Create(BeverageCreationData beverageData)
        {
            return beverageData switch
            {
                { Beverage: BeverageType.Espresso } => new Espresso(),
                { Beverage: BeverageType.Tea } => new Tea(),
                { Beverage: BeverageType.HotChocolate } => new HotChocolate(),
                {
                    Beverage: BeverageType.Milk,
                    BaseBeverage: not null
                } => new MilkAddOn(beverageData.BaseBeverage),
                {
                    Beverage: BeverageType.Syrup,
                    BaseBeverage: not null,
                    SyrupFlavour: not SyrupFlavourType.None,
                } => new SyrupAddOn(beverageData.BaseBeverage, beverageData.SyrupFlavour),
                {
                    Beverage: BeverageType.ExtraShot,
                    BaseBeverage: not null
                } => new ExtraShotDecorator(beverageData.BaseBeverage),
                _ => throw new ArgumentException("Invalid beverage type or missing base beverage for add-on."),
            };
        }
    }
}