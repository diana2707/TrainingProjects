using Cafe.Application.DTOs;
using Cafe.Domain.Factories.Beverage;
using Cafe.Domain.Models;

namespace Cafe.Application.Assemblers
{
    public class BeverageAssembler : IBeverageAssembler
    {
        private readonly IBeverageFactory _beverageFactory;

        public BeverageAssembler(IBeverageFactory beverageFactory)
        {
            _beverageFactory = beverageFactory;
        }

        public IBeverage Assemble(BeverageDetails beverageDetails)
        {
            IBeverage baseBeverage = _beverageFactory.Create(new BeverageCreationData { Beverage = beverageDetails.BaseBeverage });

            IBeverage finalBeverage = beverageDetails.AddOns.Aggregate(baseBeverage, (finalBeverage, addOn)
                => _beverageFactory.Create(
                    new BeverageCreationData
                    {
                        Beverage = addOn,
                        BaseBeverage = finalBeverage,
                        SyrupFlavour = beverageDetails.SyrupFlavour
                    }));

            return finalBeverage;
        }
    }
}