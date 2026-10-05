using System.Threading.Tasks;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Models;
using Moq;
using Xunit;

namespace GymSwipe.UserTests.MoqTests
{
    public class MoqUserTests
    {


        [Fact]
        public async Task TestUserPlaylist()
        {
            // Arrange

            var gymPlaylistMock = new Mock<IGymPlaylistRepository>();
            gymPlaylistMock.Setup(p => p.GetPlaylist(1))
                .ReturnsAsync(new GymPlaylist { Id = 1, Name = "Test Playlist" });

            // Act
            var gymPlaylistService = new GymPlaylistService(gymPlaylistMock.Object);
            var result = await gymPlaylistService.GetPlaylist(1);

            // Assert
            Assert.Equal("Test Playlist", result.Name);
        }
    }


}
