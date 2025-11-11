using Cafe.Domain.Models;

namespace Cafe.Domain.Factory
{
    public class IBeverageFactory
    {
        public IBeverage Create(string key, IBeverage beverage = null, string syrupFlavor = null)
        {
            return key.ToLower() switch
            {
                "tea" => new Tea(),
                "espresso" => new Espresso(),
                "hot chocolate" => new HotChocolate(),
                "milk" when beverage != null => new MilkDecorator(beverage),
                "syrup" when beverage != null && syrupFlavor != null => new SyrupDecorator(beverage, syrupFlavor),
                "extra shot" when beverage != null => new ExtraShotDecorator(beverage),
                _ => throw new ArgumentException("Invalid beverage type", nameof(key)),
            };
        }
    }
}
