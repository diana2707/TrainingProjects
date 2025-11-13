using Cafe.Application.Assemblers;
using Cafe.Application.DTOs;
using Cafe.Domain.Enums;
using Cafe.Domain.Factory;
using Cafe.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Tests.ApplicationTests.AssemblersTests
{
    public class BeverageAssemblerTests
    {
        [Fact]
        public void Assemble_ShouldReturnCorrectBeverageCost()
        {
            // Arrange
            var beverageFactory = new Mock<IBeverageFactory>();
            var beverageAssembler = new BeverageAssembler(beverageFactory.Object);

            var beverageDetails = new BeverageDetails
            {
                BaseBeverage = BeverageType.Espresso,
                AddOns = new BeverageType[] { BeverageType.Syrup },
                SyrupFlavour = SyrupFlavourType.Vanilla
            };

            // setup for base beverage
            beverageFactory
                .Setup(f => f.Create(BeverageType.Espresso, null, SyrupFlavourType.None))
                .Returns(new Espresso());

            // setup for add-on
            beverageFactory
                .Setup(f => f.Create(BeverageType.Syrup, It.IsAny<IBeverage>(), SyrupFlavourType.Vanilla))
                .Returns<BeverageType, IBeverage, SyrupFlavourType>((type, baseBeverage, flavour) =>
                    new SyrupAddOn(baseBeverage, flavour));

            var finalBeverage = beverageAssembler.Assemble(beverageDetails);

            Assert.Equal(3.00m, finalBeverage.Cost());
        }
    }
}
