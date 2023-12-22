using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Notifications.Application.OrderSubmittedFeature;

public class Handler : IConsumer<OrderSubmitted>
{
    private readonly IRequestClient<FindOrderRequest> _requestClient;
    private readonly ILogger<Handler> _logger;

    public Handler(IRequestClient<FindOrderRequest> requestClient, ILogger<Handler> logger)
    {
        _requestClient = requestClient;
        _logger = logger;
    }


    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        var order = await _requestClient.GetResponse<OrderDto>(new FindOrderRequest(context.Message.OrderId));
        _logger.LogInformation($"Notification about order submitting is sent to user {order.Message.CustomerId}");
    }
}