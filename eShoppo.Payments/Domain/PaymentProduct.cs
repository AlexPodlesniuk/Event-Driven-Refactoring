using BuildingBlocks;

namespace eShoppo.Payments.Domain;

public class PaymentProduct : AggregateRoot
{
    public PaymentProduct(string id, int maxPaymentTime) : base(id)
    {
        MaxPaymentTime = maxPaymentTime;
    }

    public int MaxPaymentTime { get; init; }
}