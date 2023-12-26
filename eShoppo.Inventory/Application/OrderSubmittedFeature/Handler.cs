using BuildingBlocks;
using eShoppo.Inventory.Domain;
using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Inventory.Application.OrderSubmittedFeature;

internal class Handler : IConsumer<OrderSubmitted>
{ 
    private readonly Repository<InventoryProduct> _productRepository;
    private readonly Repository<StockItemRequest> _stockItemRequestRepository;
    private readonly ILogger<Handler> _logger;

    public Handler(Repository<InventoryProduct> productRepository, Repository<StockItemRequest> stockItemRequestRepository, ILogger<Handler> logger)
    {
        _productRepository = productRepository;
        _stockItemRequestRepository = stockItemRequestRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        var order = context.Message;
        var stockRequest = new StockItemRequest(order.OrderId);
        foreach (var orderItem in order.OrderItems)
        {
            var product = await _productRepository.GetById(orderItem.ProductId);
            stockRequest.AddItem(product.Sku, orderItem.Quantity);
            
            _logger.LogInformation($"Put on hold {orderItem.Quantity} items of {product.Sku})");
        }

        await _stockItemRequestRepository.Save(stockRequest);
    }
}