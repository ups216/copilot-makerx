# ChatGPT and GitHub Copilot Programming Sample

This is a sample project that uses [ChatGPT](https://chat.openai.com/) and [GitHub Copilot](https://copilot.github.com/) to generate code for a simple C# project.

## ChatGPT Prompts

-   How do I implement a REST API with C#?
-   Implement a REST API in C# with a /match endpoint that takes a player as a string parameter and returns a GameMatch with the 2 matched players.
    The method should try to find an open match for the player.
    If there is no open match, it should create a new match with the specified player, but if there is an open match it should assign the player to that open match.
-   use minimal APIs instead of a Controller
-   instead of using IsOpen to track the state, use an enum MatchState with values like: WaitingForOpponent, MatchFound and GameReady
-   add an Id and MatchState to the GameMatch. MatchState with values like: WaitingForOpponent, MatchFound and GameReady. The MatchState should be set to WaitingForOpponent when a new GameMatch is created
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
dotnet add package MassTransit.RabbitMQ
dotnet add package Moq
```

## Demo Steps

1. Create MatchMaker Project and use ChatGPT to add /match endpoint
2. Add MatchMaker.UnitTest Project and use ChatGPT to generate some unit tests, then use GitHub Copilot to fix these tests
3. Add Contracts Project and add two records MatchWaitingForGame and GameCreated
4. Add GameManager Project and MassTransit.RabbitMQ Package and implement MatchWaitingForGameConsumer
5. Add UpdateMatch method into MatchService so we can update an exisitng match's IpAddress and Port and state.
6. Add MassTransit.RabbitMQ Package to MatchMaker Project and implement GameCreatedConsumer, invoke UpdateMatch method to update an existing match
7. Update MatchPlayerAsync method in MatchService to publish a MatchWaitingForGame message
8. Update UnitTests to use Moq instead of invoking the service directly

## References

-   [MassTransit](https://masstransit-project.com/)
-   [RabbitMQ](https://www.rabbitmq.com/)
-   [GitHub Copilot](https://copilot.github.com/)
-   [ChatGPT](https://chat.openai.com/)
-   [Minimal APIs](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-6-preview-4/#minimal-apis)

## Preqrequisites

-   [DotNet 7](https://dotnet.microsoft.com/download/dotnet/7.0)
-   [Visual Studio Code](https://code.visualstudio.com/)
-   [Docker](https://www.docker.com/)
-   [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
-   [Docker Extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-docker)
-   [Rest Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)
-   [RabbitMQ Management UI](https://www.rabbitmq.com/management.html)

## Thansk to

-   Julio Casal's youtube videos
    -   https://www.youtube.com/watch?v=a9-m9EYijHk&t=1639s
    -   https://www.youtube.com/watch?v=_5OrL7AOrmM
