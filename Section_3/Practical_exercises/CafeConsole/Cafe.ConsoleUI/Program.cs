using Cafe.Application.Assemblers;
using Cafe.Application.DTOs;
using Cafe.Application.Mappers;
using Cafe.Application.PricingServices;
using Cafe.Application.Services;
using Cafe.ConsoleUI.Controllers;
using Cafe.ConsoleUI.Interfaces;
using Cafe.ConsoleUI.UI;
using Cafe.Domain.Events;
using Cafe.Domain.Factory;
using Cafe.Infrastructure.Factories;
using Cafe.Infrastructure.Observers;

namespace Cafe.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
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
            IMapper<OrderPlaced, Receipt> orderToReceiptMapper = new OrderPlacedToReceiptMapper();

            //Pricing services
            IPricingStrategyFactory pricingStrategyFactory = new PricingStrategyFactory();
            ICostCalculator costCalculator = new CostCalculator(pricingStrategyFactory);

            //Assemblers
            IBeverageFactory beverageFactory = new BeverageFactory();
            IBeverageAssembler beverageAssembler = new BeverageAssembler(beverageFactory);

            //Order services
            IOrderService orderService = new OrderService(beverageAssembler, costCalculator, orderEventPublisher, orderToReceiptMapper);

            //Controller
            var menuController = new MenuController(inputValidator, displayer, menuSelectionParser, orderService);

            menuController.Run();
        }
    }
}
