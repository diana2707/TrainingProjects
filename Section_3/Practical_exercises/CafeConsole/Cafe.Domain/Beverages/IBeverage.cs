using Cafe.Domain.Enums;

namespace Cafe.Domain.Models
{
    public interface IBeverage
    {
        BeverageType Type { get; }
        string Name { get; }
        decimal Cost();
        string Describe();
    }
}
