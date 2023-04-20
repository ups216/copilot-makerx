using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchMaker.Entities;

namespace MatchMaker.Services;

public interface IMatchService
{
    Task<GameMatch> MatchPlayerAsync(string player);
    void UpdateMatch(int matchId, string ipAddress, int port, MatchState state);
}