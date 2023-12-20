using BuildingBlocks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using eShoppo.Orders.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly)
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Repository>();
builder.Services.AddSingleton<IEventPublisher, MediatrPublisher>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/orders/submit", async (ISender sender, [FromBody] SubmitOrder command) => await sender.Send(command));

app.Run();