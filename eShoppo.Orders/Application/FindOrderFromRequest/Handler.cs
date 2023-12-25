using BuildingBlocks;
using eShoppo.Orders.Contracts;
using eShoppo.Orders.Domain.Order;
using MassTransit;

namespace eShoppo.Orders.Application.FindOrderFromRequest;

public class Handler : IConsumer<FindOrderRequest>
{
    private readonly Repository<Order> _repository;

    public Handler(Repository<Order> repository)
    {
        _repository = repository;
    }
    
    public async Task Consume(ConsumeContext<FindOrderRequest> context)
    {
        var order = await _repository.GetById(context.Message.OrderId);
        await context.RespondAsync(new OrderDto(order!.Id, order.OrderNumber, order.CustomerId, order.TotalItems, order.OrderItems.Select(oi => new OrderItemDto(oi.Item.ProductId, oi.Item.Quantity))));
    }
}