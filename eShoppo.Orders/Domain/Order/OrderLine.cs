using eShoppo.Orders.Contracts;

namespace eShoppo.Orders.Domain.Order;

public record OrderLine(OrderItem Item, decimal Price);