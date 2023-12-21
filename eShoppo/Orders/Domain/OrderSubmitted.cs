using BuildingBlocks;
using MediatR;

namespace eShoppo.Orders.Domain;

public record OrderSubmitted(string OrderId, DateTime SubmittedOn) : IDomainEvent;