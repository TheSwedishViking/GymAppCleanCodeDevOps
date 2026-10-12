using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Models;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace GymSwipe.UserTests.MoqTests
{
    public class MoqUserTests
    {


        private readonly ITestOutputHelper _output;

        public MoqUserTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task TestUserPlaylist()
        {
            // Arrange

            var repoMock = new Mock<IGymPlaylistRepository>();

            repoMock.Setup(p => p.GetPlaylist(1))
                .ReturnsAsync(new GymPlaylist { Id = 1, Name = "Test Playlist" });

            var dateMock = new Mock<IDateHandler>();

            // Act
            var service = new GymPlaylistService(repoMock.Object, dateMock.Object);

            var result = await service.GetPlaylist(1);

            // Assert
            Assert.Equal("Test Playlist", result.Name);
        }

        [Fact]
        public async Task GetPlaylist_ReturnsPlaylistWithExercises()
        {
            // Arrange
            var playlist = new GymPlaylist
            {
                Id = 1,
                Name = "Mocked Playlist",
                Exercises = new List<PlaylistExercise>
                {
                    new PlaylistExercise
                    {
                        Id = 1,
                        ExerciseId = 1,
                        PlaylistId = 1,
                        PlaylistOrder = 1,
                        Exercise = new Exercise { Id = 1, Name = "Mocked Exercise" }
                    }
                }
            };
            _output.WriteLine(playlist.Exercises.FirstOrDefault().Exercise.Name);

            var repoGym = new Mock<IGymPlaylistRepository>();
            var dateMock = new Mock<IDateHandler>();
            repoGym.Setup(p => p.GetPlaylist(1)).ReturnsAsync(playlist);

            var service = new GymPlaylistService(repoGym.Object, dateMock.Object);

            // Act
            var result = await service.GetPlaylist(1);

            Assert.Equal("Mocked Exercise", result.Exercises.First().Exercise.Name);
        }



    }


}
