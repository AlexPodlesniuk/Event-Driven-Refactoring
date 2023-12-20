using BuildingBlocks;
using MediatR;

namespace eShoppo.Orders.Domain;

public record OrderSubmitted(Order SubmittedOrder) : IDomainEvent, INotification;