using Cafe.Domain.Models;

namespace Cafe.Domain.Factories.Beverage
{
    public interface IBeverageFactory
    {
        public IBeverage Create(BeverageCreationData data);
    }
}