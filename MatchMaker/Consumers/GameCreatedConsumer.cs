using Contracts;
using MassTransit;
using MatchMaker.Entities;
using MatchMaker.Services;

namespace MatchMaker.Consumers;

public class GameCreatedConsumer: IConsumer<GameCreated>
{
    private readonly IMatchService _matchService;
    // add a logger
    private readonly ILogger<GameCreatedConsumer> _logger;

    public GameCreatedConsumer(IMatchService matchService, ILogger<GameCreatedConsumer> logger)
    {
        _matchService = matchService;
        _logger = logger;
    }

    public Task Consume(ConsumeContext<GameCreated> context)
    {
        var message = context.Message;

        _logger.LogInformation("GameCreated message received : {GameId} {MatchId} at {IpAddress}:{Port}", message.GameId, message.MatchId, message.IpAddress, message.Port);

        _matchService.UpdateMatch(
            message.MatchId, 
            message.IpAddress, 
            message.Port, 
            MatchState.GameReady);

        _logger.LogInformation("Game Ready for : {GameId} {MatchId} at {IpAddress}:{Port}", message.GameId, message.MatchId, message.IpAddress, message.Port);
        
        
        return Task.CompletedTask;
    }
}