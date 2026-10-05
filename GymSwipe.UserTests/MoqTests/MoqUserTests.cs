using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using Moq;
using Xunit;

namespace GymSwipe.UserTests.MoqTests
{
    public class MoqUserTests
    {
        [Fact]
        public void TestUserPlaylist()
        {
            // Arrange
            var playlist = new GymPlaylist
            {
                Name = "Test Playlist",
                Excercise = new PlaylistExcercise()

            };

            var mockRepository = new Mock<IPlaylistRepository>();




            //ExerciseId = 1,
            //        Id = 1,
            //        PlaylistId = 1,
            //        PlaylistOrder = 1



            // Act



            // Assert
        }
    }
}
