using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Notifications.Application.OrderPaidFeature;

public class Handler : IConsumer<OrderPaid>
{
    private readonly IRequestClient<FindOrderRequest> _requestClient;
    private readonly ILogger<Handler> _logger;

    public Handler(IRequestClient<FindOrderRequest> requestClient, ILogger<Handler> logger)
    {
        _requestClient = requestClient;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderPaid> context)
    {
        var order = await _requestClient.GetResponse<OrderDto>(new FindOrderRequest(context.Message.OrderId));
        _logger.LogInformation($"Notification about order successful payment is sent to user {order.Message.CustomerId}");
    }
}