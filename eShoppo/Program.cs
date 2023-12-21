using System.Reflection;
using BuildingBlocks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using eShoppo.Orders.Application;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly)
    );

builder.Services.AddMassTransit(config =>
{
    
    config.SetKebabCaseEndpointNameFormatter();
    config.AddConsumers(Assembly.GetExecutingAssembly());
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://guest:guest@localhost/")); 
        
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Repository>();
builder.Services.AddSingleton<IEventPublisher, BusPublisher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/orders/submit", async (ISender sender, [FromBody] SubmitOrder command) => await sender.Send(command));

app.Run();