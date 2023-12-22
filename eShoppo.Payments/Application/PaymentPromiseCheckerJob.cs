using BuildingBlocks;
using eShoppo.Payments.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eShoppo.Payments.Application;

public class PaymentPromiseCheckerJob : BackgroundService
{
    private readonly Repository<PaymentPromise> _repository;
    private readonly ILogger<PaymentPromiseCheckerJob> _logger;

    public PaymentPromiseCheckerJob(Repository<PaymentPromise> repository, ILogger<PaymentPromiseCheckerJob> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var paymentPromises = await _repository.GetAll();
            foreach (var paymentPromise in paymentPromises)
            {
                if (!paymentPromise.WaitingForPayment) continue;
                if (paymentPromise.ExpiredAt > DateTime.UtcNow) continue;
                
                paymentPromise.MarkAsExpired();
                await _repository.Save(paymentPromise);
            }
            await Task.Delay(60 * 1000, stoppingToken);
        }
    }
}