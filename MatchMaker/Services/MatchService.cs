using MatchMaker.Entities;
using MassTransit;
using Contracts;

namespace MatchMaker.Services;

public class MatchService : IMatchService
{
    private readonly List<GameMatch> _matches = new();
    private readonly IBus bus;
    private int _nextMatchId = 1;

    public MatchService(IBus bus)
    {
        this.bus = bus;
    }

    public async Task<GameMatch> MatchPlayerAsync(string player)
    {
        var match = _matches.FirstOrDefault(m => (m.Player1 == player || m.Player2 == player) && m.State != MatchState.GameReady);
        if (match != null)
        {
            return await Task.FromResult(match);
        }

        var openMatch = _matches.FirstOrDefault(m => m.State == MatchState.WaitingForOpponent && m.Player1 != player && m.Player2 == null);
        if (openMatch != null)
        {
            openMatch.Player2 = player;
            openMatch.State = MatchState.MatchFound;

            await bus.Publish(new MatchWaitingForGame(openMatch.Id));

            return openMatch;
        }
        else
        {
            var newMatch = new GameMatch
            {
                Id = _nextMatchId++,
                Player1 = player,
                State = MatchState.WaitingForOpponent
            };
            _matches.Add(newMatch);
            return newMatch;
        }
    }

    // Add a method to update a match ip address, port and MatchState
    public void UpdateMatch(int matchId, string ipAddress, int port, MatchState state)
    {
        var match = _matches.FirstOrDefault(m => m.Id == matchId);

        if (match is null){
            throw new ArgumentException($"Match with id {matchId} not found");
        }

        match.IpAddress = ipAddress;
        match.Port = port;
        match.State = state;
    }
}
