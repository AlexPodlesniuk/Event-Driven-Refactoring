using BuildingBlocks;

namespace eShoppo.Orders.Contracts;

public record OrderCancelled(string OrderId,  string OrderNumber, string CustomerId, DateTime SubmittedOn) : IIntegrationEvent;