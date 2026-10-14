using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Mappers;
using GymSwipe.ApplicationLayer.Interfaces.Repositories;
using GymSwipe.ApplicationLayer.Interfaces.TimeDateFormat;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.ApplicationLayer.Services.Services;
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
        public async Task GetPlaylist_ReturnsPlaylistWithExercises_ValidatesBoth_DomainAndDTO()
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
            var mapDomainMock = new Mock<IMapDomainService>();
            var mapDTOMock = new Mock<IMapDTOService>();
            var timeMock = new Mock<ITimeSetter>();
            repoGym.Setup(p => p.GetPlaylist(1)).ReturnsAsync(playlist);


            var playlistDTO = new GymPlaylistDTO
            {
                Id = 1,
                Name = "Mocked Playlist",
                Excercise = new List<PlaylistExerciseDTO>
                {
                    new PlaylistExerciseDTO
                    {
                        ExerciseId = 1,
                        PlaylistId = 1,
                        PlaylistOrder = 1,
                        Name = "Mocked Exercise"
                    }
                }
            };

            var service = new GymPlaylistService(repoGym.Object, mapDTOMock.Object, mapDomainMock.Object, timeMock.Object, dateMock.Object);


            // Arrange mock

            mapDTOMock.Setup(m => m.MapPlaylistDTO(playlist)).ReturnsAsync(playlistDTO);

            // Act
            var domainResult = await service.GetPlaylist(1);

            Assert.Equal("Mocked Exercise", domainResult.Excercise.First().Name);

            var dtoResult = await mapDTOMock.Object.MapPlaylistDTO(playlist);

            Assert.Equal("Mocked Playlist", dtoResult.Name);
        }


    }


}
