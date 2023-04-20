using MassTransit;
using MatchMaker.Consumers;
using MatchMaker.Entities;
using MatchMaker.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMatchService, MatchService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<GameCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureServiceEndpoints(context);
    });
});

var app = builder.Build();

app.MapGet("/match/{player}", async (string player, [FromServices] IMatchService matchService) =>
{
    var result = await matchService.MatchPlayerAsync(player);
    return Results.Ok(result);
});

app.Run();

