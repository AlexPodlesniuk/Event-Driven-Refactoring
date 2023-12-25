using BuildingBlocks;
using eShoppo.Catalog.Contracts;
using eShoppo.Inventory.Domain;
using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Inventory.Application.OrderSubmittedFeature;

internal class Handler : IConsumer<OrderSubmitted>
{
    private readonly IRequestClient<FindOrderRequest> _orderRequestClient;
    private readonly IRequestClient<FindProductRequest> _productRequestClient;
    private readonly Repository<StockItemRequest> _stockItemRequestRepository;
    private readonly ILogger<Handler> _logger;


    public Handler(IRequestClient<FindOrderRequest> orderRequestClient, IRequestClient<FindProductRequest> productRequestClient, Repository<StockItemRequest> stockItemRequestRepository, ILogger<Handler> logger)
    {
        _orderRequestClient = orderRequestClient;
        _productRequestClient = productRequestClient;
        _stockItemRequestRepository = stockItemRequestRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        var orderResponse = await _orderRequestClient.GetResponse<FindOrderResponse>(new FindOrderRequest(context.Message.OrderId));
        var order = orderResponse.Message;
        var stockRequest = new StockItemRequest(order.OrderId);
        foreach (var orderItem in order.OrderItems)
        {
            var productResponse = await _productRequestClient.GetResponse<FindProductResponse>(new FindProductRequest(orderItem.ProductId));
            stockRequest.AddItem(productResponse.Message.Sku, orderItem.Quantity);
            
            _logger.LogInformation($"Put on hold {orderItem.Quantity} items of {productResponse.Message.Sku})");
        }

        await _stockItemRequestRepository.Save(stockRequest);
    }
}