using System.Text.Json.Serialization;
using BuildingBlocks;
using eShoppo.Catalog;
using eShoppo.Catalog.Application.CreateProductFeature;
using eShoppo.Catalog.Contracts;
using eShoppo.Inventory;
using eShoppo.Notifications;
using eShoppo.Orders;
using eShoppo.Orders.Application.CreateOrderFeature;
using eShoppo.Orders.Application.FindOrderByIdFeature;
using eShoppo.Orders.Application.FindOrderHistoryByIdFeature;
using eShoppo.Orders.Application.SubmitOrderFeature;
using eShoppo.Orders.Contracts;
using eShoppo.Payments;
using eShoppo.Payments.Application;
using eShoppo.Payments.Application.PayOrderFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(InventoryTypeReference).Assembly);
    config.RegisterServicesFromAssembly(typeof(NotificationTypeReference).Assembly);
    config.RegisterServicesFromAssembly(typeof(OrdersTypeReference).Assembly);
    config.RegisterServicesFromAssembly(typeof(PaymentsTypeReference).Assembly);
    config.RegisterServicesFromAssembly(typeof(ProductTypeReference).Assembly);
});

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();
    config.AddConsumers(typeof(InventoryTypeReference).Assembly);
    config.AddConsumers(typeof(NotificationTypeReference).Assembly);
    config.AddConsumers(typeof(OrdersTypeReference).Assembly);
    config.AddConsumers(typeof(PaymentsTypeReference).Assembly);
    config.AddConsumers(typeof(ProductTypeReference).Assembly);
    
    config.AddRequestClient<FindOrderRequest>();
    config.AddRequestClient<FindProductRequest>();
    
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://guest:guest@localhost/")); 
        
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(typeof(Repository<>));
builder.Services.AddSingleton<IEventPublisher, BusPublisher>();
builder.Services.AddHostedService<PaymentPromiseCheckerJob>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/orders/{orderId}", async (ISender sender, string orderId) => await sender.Send(new FindOrderById(orderId)));
app.MapGet("/orders/{orderId}/history", async (ISender sender, string orderId) => await sender.Send(new FindOrderHistoryById(orderId)));

app.MapPost("/orders/create", async (ISender sender, [FromBody] CreateOrder command) => await sender.Send(command));
app.MapPost("/orders/submit", async (ISender sender, [FromBody] SubmitOrder command) => await sender.Send(command));

app.MapPost("/payments/submit", async (ISender sender, [FromBody] PayOrder command) => await sender.Send(command));

app.MapPost("/catalog/create", async (ISender sender, [FromBody] CreateProduct command) => await sender.Send(command));

app.Run();