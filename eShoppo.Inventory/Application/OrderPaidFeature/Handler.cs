using BuildingBlocks;
using eShoppo.Inventory.Domain;
using eShoppo.Orders.Contracts;
using eShoppo.Payments.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Inventory.Application.OrderPaidFeature;

internal class Handler : IConsumer<OrderPaid>
{
    private readonly IRequestClient<FindOrderRequest> _requestClient;
    private readonly Repository<StockItemRequest> _stockItemRequestRepository;
    private readonly ILogger<Handler> _logger;


    public Handler(IRequestClient<FindOrderRequest> requestClient, Repository<StockItemRequest> stockItemRequestRepository, ILogger<Handler> logger)
    {
        _requestClient = requestClient;
        _stockItemRequestRepository = stockItemRequestRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderPaid> context)
    {
        var order = await _requestClient.GetResponse<FindOrderResponse>(new FindOrderRequest(context.Message.OrderId));
        var stockRequest = await _stockItemRequestRepository.GetById(order.Message.OrderId) ?? throw new StockRequestIsNotFoundException(order.Message.OrderId);
        
        stockRequest.ConfirmBooking();
        await _stockItemRequestRepository.Save(stockRequest);
        _logger.LogInformation($"Order {order.Message.OrderNumber} paid {order.Message.TotalPrice}, stock items will be decreased from inventory");
    }
}