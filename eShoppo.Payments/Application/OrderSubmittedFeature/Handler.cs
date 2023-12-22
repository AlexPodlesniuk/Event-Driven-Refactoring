using BuildingBlocks;
using eShoppo.Orders.Contracts;
using eShoppo.Payments.Domain;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Payments.Application.OrderSubmittedFeature;

public class Handler : IConsumer<OrderSubmitted>
{
    private readonly IRequestClient<FindOrderRequest> _requestClient;
    private readonly Repository<PaymentPromise> _paymentPromiseRepository;
    private readonly ILogger<Handler> _logger;


    public Handler(IRequestClient<FindOrderRequest> requestClient, Repository<PaymentPromise> paymentPromiseRepository, ILogger<Handler> logger)
    {
        _requestClient = requestClient;
        _paymentPromiseRepository = paymentPromiseRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        var order = await _requestClient.GetResponse<OrderDto>(new FindOrderRequest(context.Message.OrderId));
        var paymentPromise = new PaymentPromise(order.Message.OrderId)
        {
            ExpiredAt = DateTime.UtcNow.AddMinutes(1)
        };

        await _paymentPromiseRepository.Save(paymentPromise);
        _logger.LogInformation($"Payment promise created for order {context.Message} and will be expired at {paymentPromise.ExpiredAt}");
    }
}