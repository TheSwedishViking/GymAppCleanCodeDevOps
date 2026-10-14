using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Enums;
using GymSwipe.Infrastructure.Repos;
using GymSwipe.UserTests.FixtureFolder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests
{
    public class ExerciseTestsLocalClassFixture : IClassFixture<StandardLocalInMemoryFixture>
    {
        private readonly StandardLocalInMemoryFixture _fixture;
        private readonly IServiceScope _scope;
        private readonly IExerciseService _sut;
        public ExerciseTestsLocalClassFixture(StandardLocalInMemoryFixture fixture)
        {
            _fixture = fixture;
           _scope = _fixture.ServiceProvider.CreateScope();
           _sut = _scope.ServiceProvider.GetRequiredService<IExerciseService>();
        }
      
        public void Dispose() => _scope.Dispose();
        [Fact]
        public async Task TryAndGetStaticExcerises_ReturnsExpectedTestaData_FormatDTO_ToConvertToFrontendModels()
        {
            //Act
            var exs = await _sut.GetAllExercisesAsync();

            //Assert
            Assert.NotEmpty(exs);
        }
        [Fact]
        public async Task IfTheresNoMatchingId_ToAnExercise_ThrowException()
        {
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>( () => _sut.GetExerciseById(12312313)) ;

            Assert.Contains("not", ex.Message);
        }
        [Fact]
        public async Task GetExercises_ThatTargetsTheChest_WithAProvidedId_ReturnsExpectedExercises()
        {
            var expected = "Chest";
            var chests = await _sut.GetExercisesByCategoryIdAsync(1);

            Assert.NotEmpty(chests);
            //Check if every exercies has any targetarea name that contains chest
            Assert.All(chests, c =>Assert.Contains(c.TargetAreaNames, n=>n.Name.Contains(expected, StringComparison.OrdinalIgnoreCase)));
        }

        [Fact]
        public async Task InvalidId_ReturnsExpected_Empty()
        {
            var empty = await _sut.GetExercisesByCategoryIdAsync(0);

            Assert.Empty(empty);

        }
        [Fact]
        public async Task DontAllow_DuplicateExercies_StringComparison_ExpectedFalse()
        {
            var exercise = new ExerciseDTO
            {
                Name = "Push up",
                VideoSourceLink = "https://www.youtube.com/",
                TargetAreaNames = new List<TargetAreaDTO>
                {
                    new TargetAreaDTO
                    {
                        Name="Chest"
                    }
                }
            };
            bool expected = false;

            var act = await _sut.GetExerciseByName(exercise.Name);

            bool actual =  await _sut.SaveExercise(exercise);

            Assert.Equal(actual, expected);
        }
        [Fact]
        public async Task GetNamesOfExercises_ByName_ReturnsTrue()
        {
            string name = "Push up";

            bool expected = true;

            var actual = await _sut.GetExerciseByName(name);

            Assert.Equal(name, actual.Name);
        }
        [Theory]
        [InlineData("Push up", true)]
        [InlineData("PUSH UP", true)]
        [InlineData("pUsH up", true)]
        public async Task GetNameOfExercises_HandlesStringCase_TriesToFindExercise_NonExistingExercise_ReturnsNull(string name,  bool shouldExist)
        {
            var result = await _sut.GetExerciseByName(name);

            if (shouldExist)
            {
                Assert.NotNull(result);
                Assert.Equal("Push up", result.Name);
            }
            else
            {
                Assert.Null(result);
            }
        }
        [Theory]
        [InlineData("backflip")]
        [InlineData("sdaasdssdads")]
        public async Task GetExerciseByName_NonExistingExercise_ReturnsNull(string invalid)
        {
            //Expected result is an execption
            var ex = await _sut.GetExerciseByName(invalid);

            Assert.Null(ex);
        }




    }
}
