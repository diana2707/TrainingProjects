using Cafe.Domain.Enums;

namespace Cafe.Application.Mappers
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source, PricingPolicyType pricingPolicy);
    }
}