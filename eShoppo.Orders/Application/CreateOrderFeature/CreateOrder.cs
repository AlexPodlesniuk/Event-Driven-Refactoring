using eShoppo.Orders.Domain.Order;
using MediatR;

namespace eShoppo.Orders.Application.CreateOrderFeature;

public record CreateOrder(string CustomerId, List<OrderItem> Items) : IRequest<string>;