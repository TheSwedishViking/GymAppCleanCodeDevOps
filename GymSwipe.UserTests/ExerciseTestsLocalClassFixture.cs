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
    public class ExerciseTestsLocalClassFixture : IClassFixture<ExerciseFixture>
    {
        private readonly ExerciseFixture _fixture;
        private readonly IServiceScope _scope;
        private readonly IExerciseService _sut;
        public ExerciseTestsLocalClassFixture(ExerciseFixture fixture)
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
        public async Task GetExercises_ThatTargetsTheChest_WithAProvidedId_ReturnsExpectedExercises()
        {
            var expected = "Chest";
            var chests = await _sut.GetExerciseByIdAsync(1);

            Assert.NotEmpty(chests);
            //Check if every exercies has any targetarea name that contains chest
            Assert.All(chests, c =>Assert.Contains(c.TargetAreaNames, n=>n.Contains(expected, StringComparison.OrdinalIgnoreCase)));
        }

        [Fact]
        public async Task GetExercises_WithAEnum_ProvidesExpectedExercies_AccordingToEnumName()
        {
            var chests = await _sut.GetExerciseByEnum(AreaEnum.Chest);

            Assert.NotEmpty(chests);
        }
        [Fact]
        public async Task Requesting_ExerciesWithAndInvalid_Id_LikeZero_Or_Enum_AreaEnum_Inavlid_ReturnsEmpty()
        {
            var empty = await _sut.GetExerciseByIdAsync(0);

            Assert.Empty(empty);

            var enumEmpty = await _sut.GetExerciseByEnum(AreaEnum.Invalid);

            Assert.Empty(enumEmpty);
        }
        [Theory]
        [InlineData(AreaEnum.Shoulders, 4)]
        [InlineData(AreaEnum.Chest, 1)]
        [InlineData(AreaEnum.Biceps, 2)]
        [InlineData(AreaEnum.Triceps, 3)]
        [InlineData(AreaEnum.FrontThighs, 6)]
        public async Task ValidateAndMakeSure_TheEnums_AndDbRegisteredCategories_ShareTheSameValues_ReturnsTrue_OnValid(AreaEnum area, int registedInDbId)
        {
            var enumResult = await _sut.GetExerciseByEnum(area);
            var idResult = await _sut.GetExerciseByIdAsync(registedInDbId);

            Assert.Equal(enumResult.Count(), idResult.Count());

            Assert.All(enumResult, e =>
            {
                var match = Assert.Single(idResult, i => i.Id == e.Id);
                Assert.Equal(e.Name, match.Name);
            });
        }

    }
}
