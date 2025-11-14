using Cafe.Domain.Enums;
using Cafe.Domain.Factories.Beverage;
using Cafe.Domain.Models;
using Cafe.Infrastructure.Factories;

namespace Cafe.Tests.InfrastructureTests.Factories
{
    public class BeverageFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnCorrectBeverageInstance_WhenGivenType()
        {
            var beverageFactory = new BeverageFactory();

            var espresso = beverageFactory.Create(new BeverageCreationData
            {
                Beverage = BeverageType.Espresso
            });

            var milkAddOn = beverageFactory.Create(new BeverageCreationData
            {
                Beverage = BeverageType.Milk,
                BaseBeverage = espresso
            });

            Assert.IsType<Espresso>(espresso);
            Assert.Equal("Espresso", espresso.Describe());

            Assert.IsType<MilkAddOn>(milkAddOn);
            Assert.Equal("Espresso + Milk", milkAddOn.Describe());
        }

        public void Create_ShouldReturnBeverageWithSyrup_WhenGivenSyrupFlavour()
        {
            var beverageFactory = new BeverageFactory();

            var espresso = beverageFactory.Create(new BeverageCreationData { Beverage = BeverageType.Espresso });
            var syrupAddOn = beverageFactory.Create(new BeverageCreationData
            {
                Beverage = BeverageType.Syrup,
                BaseBeverage = espresso,
                SyrupFlavour = SyrupFlavourType.Vanilla
            });

            Assert.IsType<SyrupAddOn>(syrupAddOn);
            Assert.Equal("Espresso + Vanilla Syrup", syrupAddOn.Describe());
        }

        public void Create_ShouldThrowArgumentException_WhenGivenInvalidType()
        {
            var beverageFactory = new BeverageFactory();
            Assert.Throws<ArgumentException>(() => beverageFactory.Create(new BeverageCreationData
            {
                Beverage = (BeverageType)int.MaxValue
            }));
        }
    }
}