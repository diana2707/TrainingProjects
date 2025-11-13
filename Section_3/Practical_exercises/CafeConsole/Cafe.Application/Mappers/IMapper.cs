using Cafe.Application.DTOs;
using Cafe.Domain.Enums;
using Cafe.Domain.Events;

namespace Cafe.Application.Mappers
{
    public interface IMapper <TSource, TDestination>
    {
        TDestination Map(TSource source, PricingPolicyType pricingPolicy);
    }
}