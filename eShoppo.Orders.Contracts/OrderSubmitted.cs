using BuildingBlocks;

namespace eShoppo.Orders.Contracts;

public record OrderSubmitted(string OrderId, string OrderNumber, IEnumerable<OrderItem> OrderItems, decimal TotalPrice, string CustomerId, DateTime SubmittedOn) : IIntegrationEvent;