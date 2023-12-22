using BuildingBlocks;
using eShoppo.Inventory.Domain;
using eShoppo.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace eShoppo.Inventory.Application.OrderSubmittedFeature;

public class Handler : IConsumer<OrderSubmitted>
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

    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        var orderResponse = await _requestClient.GetResponse<OrderDto>(new FindOrderRequest(context.Message.OrderId));
        var order = orderResponse.Message;
        var stockRequest = new StockItemRequest(order.OrderId);
        foreach (var orderItem in order.OrderItems)
        {
            stockRequest.AddItem(orderItem.ProductId, orderItem.Quantity);
        }

        await _stockItemRequestRepository.Save(stockRequest);
        _logger.LogInformation($"Inventory will be booked for order {context.Message} for total items {order.TotalItems}");
    }
}