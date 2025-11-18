using Cafe.Application.Assemblers;
using Cafe.Application.DTOs;
using Cafe.Application.Mappers;
using Cafe.Application.PricingServices;
using Cafe.Application.Services;
using Cafe.ConsoleUI.Controllers;
using Cafe.ConsoleUI.Interfaces;
using Cafe.ConsoleUI.UI;
using Cafe.Domain.Events;
using Cafe.Domain.Factories.Beverage;
using Cafe.Domain.Factories.PricingFactory;
using Cafe.Infrastructure.Factories;
using Cafe.Infrastructure.Observers;

namespace Cafe.ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //UI
            IDisplayer displayer = new Displayer();
            IInputValidator inputValidator = new InputValidator();
            IMenuSelectionParser menuSelectionParser = new MenuSelectionParser();

            //Observers
            IOrderEventSubscriber logger = new ConsoleOrderLogger();
            IOrderEventSubscriber analytics = new InMemoryOrderAnalytics();
            IOrderEventPublisher orderEventPublisher = new SimpleOrderEventPublisher([logger, analytics]);

            //Mappers
            IPlacedOrderToReceiptMapper orderToReceiptMapper = new OrderPlacedToReceiptMapper();

            //Pricing services
            decimal happyHourDiscountPercentage = 0.2m; // configure as needed
            IPricingStrategyFactory pricingStrategyFactory = new PricingStrategyFactory();
            IPricingService pricingService = new PricingService(pricingStrategyFactory, happyHourDiscountPercentage);

            //Assemblers
            IBeverageFactory beverageFactory = new BeverageFactory();
            IBeverageAssembler beverageAssembler = new BeverageAssembler(beverageFactory);

            //Order services
            IOrderService orderService = new OrderService(beverageAssembler, pricingService, orderEventPublisher, orderToReceiptMapper);

            //Controller
            var menuController = new MenuController(inputValidator, displayer, menuSelectionParser, orderService);

            menuController.Run();
        }
    }
}