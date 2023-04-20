using MassTransit;
using Contracts;

namespace GameManager.Consumers;

public class MatchWaitingForGameConsumer : IConsumer<MatchWaitingForGame>
{
    private readonly ILogger<MatchWaitingForGameConsumer> _logger;

    public MatchWaitingForGameConsumer(ILogger<MatchWaitingForGameConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<MatchWaitingForGame> context)
    {
        var receivedMessage = context.Message;

        _logger.LogInformation("MatchWaitingForGameConsumer: {MatchId}", receivedMessage.MatchId);

        await Task.Delay(TimeSpan.FromSeconds(5));

        var outgoingMessage = new GameCreated(
            GameId: Guid.NewGuid(),
            MatchId: receivedMessage.MatchId,
            IpAddress: GenerateIpAddress(),
            Port: GeneratePort());

        _logger.LogInformation("MatchWaitingForGameConsumer: {GameId} {MatchId} {IpAddress} {Port}");

        await context.Publish(outgoingMessage);
    }

    // generate a random ip address
    private string GenerateIpAddress()
    {
        var random = new Random();
        var ipAddress = $"{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";
        return ipAddress;
    }

    // generate a valid random port
    private int GeneratePort()
    {
        var random = new Random();
        var port = random.Next(1024, 65535);
        return port;
    }
}