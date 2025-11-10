namespace Cafe.Domain.Models
{
    public interface IBeverage
    {
        string Name { get; }
        decimal Cost();
        string Describe();
    }
}
