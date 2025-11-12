using Cafe.Domain.Enums;
using Cafe.Domain.Models;

namespace Cafe.Domain.Factory
{
    public interface IBeverageFactory
    {
        public IBeverage Create(BeverageType bevarageType, IBeverage beverage = null, SyrupFlavourType syrupFlavor = 0);
    }
}
