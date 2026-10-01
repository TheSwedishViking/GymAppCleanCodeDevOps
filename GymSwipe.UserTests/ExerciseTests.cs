using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Enums;
using GymSwipe.Infrastructure.Repos;
using GymSwipe.UserTests.FixtureFolder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests
{
    public class ExerciseTests : IClassFixture<ExerciseFixture>
    {
        private readonly ExerciseFixture _fixture;
        public ExerciseTests(ExerciseFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task TryAndGetStaticExcerises_ReturnsExpectedTestaData_FormatDTO_ToConvertToFrontendModels()
        {
            //Arrange
            using var scrope = _fixture.ServiceProvider.CreateScope();
            var sut = scrope.ServiceProvider.GetRequiredService<IExerciseService>();

            //Act
            var exs = await sut.GetAllExercisesAsync();

            //Assert
            Assert.NotEmpty(exs);
        }
        [Fact]
        public async Task GetExercises_ThatTargetsTheChest_WithAProvidedId_ReturnsExpectedExercises()
        {
            using var scope = _fixture.ServiceProvider.CreateScope();
            var sut = scope.ServiceProvider.GetRequiredService<IExerciseService>();


            var chests = await sut.GetExerciseByIdAsync(1);

            Assert.NotEmpty(chests);
            //Check if every exercies has any targetarea name that contains chest
            Assert.All(chests, c =>Assert.Contains(c.TargetAreaNames, n=>n.Contains("Chest", StringComparison.OrdinalIgnoreCase)));
        }

        [Fact]
        public async Task GetExercises_WithAEnum_ProvidesExpectedExercies_AccordingToEnumName()
        {
            using var scope = _fixture.ServiceProvider.CreateScope();
            var sut = scope.ServiceProvider.GetRequiredService<IExerciseService>();

            var chests = await sut.GetExerciseByEnum(AreaEnum.Chest);

            Assert.NotEmpty(chests);
        }
        [Fact]
        public async Task Requesting_ExerciesWithAndInvalid_Id_LikeZero_Or_Enum_AreaEnum_Inavlid_ReturnsEmpty()
        {
            using var scope = _fixture.ServiceProvider.CreateScope();
            var sut = scope.ServiceProvider.GetRequiredService<IExerciseService>();

            var empty = await sut.GetExerciseByIdAsync(0);

            Assert.Empty(empty);

            var enumEmpty = await sut.GetExerciseByEnum(AreaEnum.Invalid);

            Assert.Empty(enumEmpty);
        }

    }
}
