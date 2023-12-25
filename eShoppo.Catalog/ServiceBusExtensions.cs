using BuildingBlocks;
using MassTransit;

namespace eShoppo.Catalog;

public static class ServiceBusExtensions
{
    public static IBusRegistrationConfigurator RegisterCatalogConsumers(
        this IBusRegistrationConfigurator busRegistrationConfigurator)
    {
        var consumerTypes = ConsumerTypeFinder.FindConsumerTypesInAssembly(typeof(ProductTypeReference).Assembly);
        busRegistrationConfigurator.AddConsumers(consumerTypes);
        return busRegistrationConfigurator;
    }
}