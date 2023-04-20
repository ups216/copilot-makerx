# ChatGPT and GitHub Copilot Programming Sample

## ChatGPT Prompts

-   How do I implement a REST API with C#?
-   Implement a REST API in C# with a /match endpoint that takes a player as a string parameter and returns a GameMatch with the 2 matched players.
    The method should try to find an open match for the player.
    If there is no open match, it should create a new match with the specified player, but if there is an open match it should assign the player to that open match.
-   use minimal APIs instead of a Controller
-   instead of using IsOpen to track the state, use an enum MatchState with values like: WaitingForOpponent, MatchFound and GameReady
-   Please output the MatchState code as well
-   also, you are missing {player} in your api route
-   if the player is already in a match, return that match
-   Serialize the MatchState as a string instead of a number
-   If an openMatch is found, switch the MatchState to MatchFound
-   Move the matching logic into another class and use DI to inject it into the endpoint
-   MatchPlayerAsync missing the await
-   No service for type 'MatchMaker.Services.IMatchService' has been registered.
-   Generate unit tests for MatchService
-   instead of using Moq, just invoke the service directly
-   how to run a RabbitMQ docker containers with management UI?
-   Implement a c# console that asks for a player,
    invoke the http://localhost:5231/match/{player} endpoint
    keep pulling the endpoint until the MatchState is GameReady
-   generate a .gitignore for c#
-   improve it for VSCode on Mac

## Commands

Create Projects

```shell
dotnet new web -n MatchMaker
dotnet new xunit -n MatchMaker.UnitTests
dotnet new classlib -n Contracts
dotnet new worker -n GameManager
dotnet new console -n Client
```

Add Reference

```shell
dotnet add reference ../Contracts/Contracts.csproj
```

Add Package

```shell
dotnet add pakcage MassTransit.RabbitMQ
dotnet add package Moq
```
