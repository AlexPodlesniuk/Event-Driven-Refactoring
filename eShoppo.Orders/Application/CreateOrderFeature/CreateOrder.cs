using eShoppo.Orders.Contracts;
using MediatR;

namespace eShoppo.Orders.Application.CreateOrderFeature;

public record CreateOrder(string CustomerId, List<OrderItem> Items) : IRequest<string>;