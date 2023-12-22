using BuildingBlocks;
using eShoppo.Catalog.Contracts;
using eShoppo.Orders.Contracts;
using eShoppo.Payments.Domain;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Payments.Application.OrderSubmittedFeature;

public class Handler : IConsumer<OrderSubmitted>
{
    private readonly IRequestClient<FindOrderRequest> _orderRequestClient;
    private readonly IRequestClient<FindProductRequest> _productRequestClient;
    private readonly Repository<PaymentPromise> _paymentPromiseRepository;
    private readonly ILogger<Handler> _logger;


    public Handler(IRequestClient<FindOrderRequest> orderRequestClient, IRequestClient<FindProductRequest> productRequestClient, Repository<PaymentPromise> paymentPromiseRepository, ILogger<Handler> logger)
    {
        _orderRequestClient = orderRequestClient;
        _productRequestClient = productRequestClient;
        _paymentPromiseRepository = paymentPromiseRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        var order = await _orderRequestClient.GetResponse<OrderDto>(new FindOrderRequest(context.Message.OrderId));
        var product = await _productRequestClient.GetResponse<ProductDto>(new FindProductRequest(order.Message.OrderItems.First().ProductId));
        var paymentPromise = new PaymentPromise(order.Message.OrderId)
        {
            ExpiredAt = DateTime.UtcNow.AddMinutes(product.Message.MaxPaymentTime)
        };

        await _paymentPromiseRepository.Save(paymentPromise);
        _logger.LogInformation($"Payment promise created for order {context.Message} and will be expired at {paymentPromise.ExpiredAt}");
    }
}