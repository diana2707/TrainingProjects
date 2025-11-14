using Cafe.Domain.Enums;

namespace Cafe.Domain.Models
{
    public interface IBeverage
    {
        BeverageType Name { get; }

        decimal Cost();

        string Describe();
    }
}