using Cafe.Application.Assemblers;
using Cafe.Application.DTOs;
using Cafe.Domain.Enums;
using Cafe.Domain.Factories.Beverage;
using Cafe.Domain.Models;
using Moq;

namespace Cafe.Tests.ApplicationTests.AssemblersTests
{
    public class BeverageAssemblerTests
    {
        [Fact]
        public void Assemble_ShouldReturnCorrectBeverageCost()
        {
            var beverageFactory = new Mock<IBeverageFactory>();
            var beverageAssembler = new BeverageAssembler(beverageFactory.Object);

            var beverageDetails = new BeverageDetails
            {
                BaseBeverage = BeverageType.Espresso,
                AddOns = new[] { BeverageType.Syrup },
                SyrupFlavour = SyrupFlavourType.Vanilla
            };

            // Setup for base beverage
            beverageFactory
                .Setup(factory => factory.Create(It.Is<BeverageCreationData>(data =>
                    data.Beverage == BeverageType.Espresso &&
                    data.BaseBeverage == null &&
                    data.SyrupFlavour == SyrupFlavourType.None)))
                .Returns(new Espresso());

            // Setup for add-on
            beverageFactory
                .Setup(factory => factory.Create(It.Is<BeverageCreationData>(data =>
                    data.Beverage == BeverageType.Syrup &&
                    data.BaseBeverage is Espresso &&
                    data.SyrupFlavour == SyrupFlavourType.Vanilla)))
                .Returns<BeverageCreationData>(data =>
                    new SyrupAddOn(data.BaseBeverage, data.SyrupFlavour));

            var finalBeverage = beverageAssembler.Assemble(beverageDetails);

            Assert.Equal(3.00m, finalBeverage.Cost());
        }
    }
}