using eShoppo.Orders.Domain;
using MediatR;

namespace eShoppo.Orders.Application;

public record SubmitOrder(string OrderId) : IRequest<Order>;