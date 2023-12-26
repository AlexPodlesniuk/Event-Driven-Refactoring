using BuildingBlocks;

namespace eShoppo.Orders.Contracts;

public record OrderPaid(string OrderId, string OrderNumber, string CustomerId, DateTime SubmittedOn) : IIntegrationEvent;