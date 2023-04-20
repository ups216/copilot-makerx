using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchMaker.Entities;

public class GameMatch
{
    public int Id { get; set; }
    public required string Player1 { get; set; }
    public string? Player2 { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MatchState State { get; set; }
    public string? IpAddress { get; set; }
    public int? Port { get; set; }
}

public enum MatchState
{
    WaitingForOpponent = 0,
    MatchFound = 1,
    GameReady = 2
}
