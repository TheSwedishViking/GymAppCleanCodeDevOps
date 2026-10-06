using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Models;
using Moq;
using Xunit;

namespace GymSwipe.UserTests.MoqTests
{
    public class MoqUserTests : UserApiFixture
    {
        public UserApiFixture _fixture;
        public MoqUserTests(UserApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task TestUserPlaylist()
        {
            // Arrange

            var repoMock = new Mock<IGymPlaylistRepository>();

            repoMock.Setup(p => p.GetPlaylist(1))
                .ReturnsAsync(new GymPlaylist { Id = 1, Name = "Test Playlist" });

            // Act
            var service = new GymPlaylistService(repoMock.Object);
            var result = await service.GetPlaylist(1);

            // Assert
            Assert.Equal("Test Playlist", result.Name);
        }

        public async Task MockExcercisesInGymPlaylist()
        {
            // Arrange
            var repoGym = new Mock<IGymPlaylistRepository>();
            repoGym.Setup(p => p.GetPlaylist(1))
                .ReturnsAsync(new GymPlaylist { Id = 1, Name = "Mocked Playlist" });



            // Act


            // Assert
        }

    }


}
