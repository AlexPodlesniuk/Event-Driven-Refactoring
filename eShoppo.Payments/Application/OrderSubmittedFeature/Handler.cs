using BuildingBlocks;
using eShoppo.Orders.Contracts;
using eShoppo.Payments.Domain;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Payments.Application.OrderSubmittedFeature;

internal class Handler : IConsumer<OrderSubmitted>
{
    private readonly Repository<PaymentProduct> _productRepository;
    private readonly Repository<PaymentPromise> _paymentPromiseRepository;
    private readonly ILogger<Handler> _logger;

    public Handler(Repository<PaymentProduct> productRepository, Repository<PaymentPromise> paymentPromiseRepository, ILogger<Handler> logger)
    {
        _productRepository = productRepository;
        _paymentPromiseRepository = paymentPromiseRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        var order = context.Message;
        var product = await _productRepository.GetById(order.OrderItems.First().ProductId);
        var paymentPromise = new PaymentPromise(order.OrderId, order.TotalPrice)
        {
            ExpiredAt = DateTime.UtcNow.AddMinutes(product.MaxPaymentTime)
        };

        await _paymentPromiseRepository.Save(paymentPromise);
        _logger.LogInformation($"Payment promise created for order {context.Message.OrderNumber} and will be expired at {paymentPromise.ExpiredAt}");
    }
}