namespace eShoppo.Orders.Domain.Order;

public record OrderItem(string ProductId, int Quantity, decimal Price = 9.99m);