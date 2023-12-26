using BuildingBlocks;
using eShoppo.Payments.Contracts;

namespace eShoppo.Payments.Domain;

public class PaymentPromise(string id, decimal total) : AggregateRoot(id)
{
    public decimal Total { get; } = total;
    public DateTime ExpiredAt { get; init; }
    public PaymentPromiseStatus Status { get; private set; } = PaymentPromiseStatus.WaitingForPayment;

    public bool WaitingForPayment => Status == PaymentPromiseStatus.WaitingForPayment;
    public void MarkAsPaid()
    {
        Status = PaymentPromiseStatus.Paid;
        RaiseEvent(new PaymentSuccessful(id, DateTime.UtcNow));
    }
    
    public void MarkAsExpired()
    {
        Status = PaymentPromiseStatus.Expired;
        RaiseEvent(new PaymentFailed(id, DateTime.UtcNow));
    }
}

public enum PaymentPromiseStatus
{
    Paid,
    WaitingForPayment,
    Expired
}