using BuildingBlocks;
using MassTransit;

namespace eShoppo.Notifications;

public static class ServiceBusExtensions
{
    public static IBusRegistrationConfigurator RegisterNotificationConsumers(
        this IBusRegistrationConfigurator busRegistrationConfigurator)
    {
        var consumerTypes = ConsumerTypeFinder.FindConsumerTypesInAssembly(typeof(NotificationTypeReference).Assembly);
        busRegistrationConfigurator.AddConsumers(consumerTypes);
        return busRegistrationConfigurator;
    }
}