using System.Text.Json;
using eShoppo.Catalog.Contracts;
using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Notifications.Application.OrderSubmittedFeature;

public class Handler : IConsumer<OrderSubmitted>
{
    private readonly IRequestClient<FindOrderRequest> _ordersRequestClient;
    private readonly IRequestClient<FindProductRequest> _productRequestClient;
    private readonly ILogger<Handler> _logger;


    public Handler(IRequestClient<FindOrderRequest> ordersRequestClient, IRequestClient<FindProductRequest> productRequestClient, ILogger<Handler> logger)
    {
        _ordersRequestClient = ordersRequestClient;
        _productRequestClient = productRequestClient;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        var order = await _ordersRequestClient.GetResponse<OrderDto>(new FindOrderRequest(context.Message.OrderId));
        var message = JsonSerializer.Serialize(order.Message.OrderItems);
        _logger.LogInformation($"Notification about order {order.Message.OrderNumber} submitting is sent to user {order.Message.CustomerId}: \nContent:{message}");
    }
}