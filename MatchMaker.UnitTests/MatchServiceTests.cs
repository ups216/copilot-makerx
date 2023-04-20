using Xunit;
using MatchMaker.Services;
using MatchMaker.Entities;
using Moq;
using MassTransit;

namespace MatchMaker.Tests
{
    public class MatchServiceTests
    {
        [Fact]
        public async Task MatchPlayerAsync_PlayerIsMatched_ReturnsMatch()
        {
            // Arrange
            var service = new MatchService(Mock.Of<IBus>());
            var player = "Alice";
            var expectedOpponent = "Bob";

            // Act
            var result = await service.MatchPlayerAsync(player);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(player, result.Player1);
            Assert.Equal(expectedOpponent, result.Player2);
            Assert.Equal(MatchState.GameReady, result.State);
        }

        [Fact]
        public async Task MatchPlayerAsync_WithNoOpenMatches_CreatesNewMatch(){
            // Arrange
            var service = new MatchService(Mock.Of<IBus>());
            var player = "Alice";
            var expectedOpponent = "Bob";

            // Act
            var result = await service.MatchPlayerAsync(player);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(player, result.Player1);
            Assert.Equal(expectedOpponent, result.Player2);
            Assert.Equal(MatchState.WaitingForOpponent, result.State);
        }

        [Fact]
        public async Task MatchPlayerAsync_WithOpenMatch_ReturnsOpenMatch(){
            // Arrange
            var service = new MatchService(Mock.Of<IBus>());
            var player1 = "Alice";
            var player2 = "Bob";
            var expectedOpponent = "Charlie";

            // Act
            var result1 = await service.MatchPlayerAsync(player1);
            var result2 = await service.MatchPlayerAsync(player2);
            var result3 = await service.MatchPlayerAsync(expectedOpponent);

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal(player1, result1.Player1);
            Assert.Equal(player1, result2.Player1);
            Assert.Equal(expectedOpponent, result3.Player1);
            Assert.Equal(MatchState.GameReady, result1.State);
            Assert.Equal(MatchState.GameReady, result2.State);
            Assert.Equal(MatchState.WaitingForOpponent, result3.State);
        }
    }
}
