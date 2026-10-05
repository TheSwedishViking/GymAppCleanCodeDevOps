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
            Mock<Domain.Models.GymPlaylist> mockPlaylist = new Mock<Domain.Models.GymPlaylist>();

            Moq.Language.Flow.IReturnsResult<GymPlaylist> returnsResult = mockPlaylist.Setup(x => x.Excercise).Returns(new PlaylistExcercise() {
                ExerciseId = 1,
                Id = 1 ,
                PlaylistId = 1 ,
                PlaylistOrder = 1 });


        







        mockPlaylist.Setup(x => x.Name).Returns("Test Playlist");

            // Act
                
                

            // Assert
        }
    }
}
