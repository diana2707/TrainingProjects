using Cafe.Application.DTOs;
using Cafe.Application.Mappers;
using Cafe.Application.Services;
using Cafe.ConsoleUI.Controllers;
using Cafe.ConsoleUI.Interfaces;
using Cafe.ConsoleUI.UI;
using Cafe.Domain.Events;
using Cafe.Infrastructure.Observers;

namespace Cafe.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDisplayer displayer = new Displayer();
            IInputValidator inputValidator = new InputValidator();
            IMenuSelectionParser menuSelectionParser = new MenuSelectionParser();
            IOrderEventSubscriber logger = new ConsoleOrderLogger();
            IOrderEventSubscriber analytics = new InMemoryOrderAnalytics();
            IOrderEventPublisher orderEventPublisher = new SimpleOrderEventPublisher([logger, analytics]);
            IMapper<OrderPlaced, Receipt> orderToReceiptMapper = new OrderPlacedToReceiptMapper();
            IMapper<OrderDetails, OrderPlaced> orderDetailsToPlacedOrderMapper = new OrderDetailsToOrderPlacedMapper();
            IOrderService orderService = new OrderService(orderEventPublisher, orderDetailsToPlacedOrderMapper, orderToReceiptMapper);
            var menuController = new MenuController(inputValidator, displayer, menuSelectionParser, orderService);
            menuController.Run();
        }
    }
}
