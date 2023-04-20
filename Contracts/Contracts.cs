namespace Contracts;

public record MatchWaitingForGame(int MatchId);
public record GameCreated(Guid GameId, int MatchId, string IpAddress, int Port);


