using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Facades;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using GymSwipe.UserTests.FixtureFolder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests.Unit
{
    public class AdminCreationTests : IClassFixture<StandardLocalInMemoryFixture>
    {
        private readonly StandardLocalInMemoryFixture _fixture;
        private readonly IServiceScope _scope;
        private readonly IAdminAddExerciseFacade _admingExerciseSut;
        private readonly IAdminAddTrainingAreaFacade _adminAddTrainingAreaSut;
        private readonly IExerciseService _exerciseServiceSut;
        private readonly ITraningAreaService _traningAreaSut;
        public AdminCreationTests(StandardLocalInMemoryFixture fixture)
        {
            _fixture = fixture;
            _scope = _fixture.ServiceProvider.CreateScope();
            _admingExerciseSut = _scope.ServiceProvider.GetRequiredService<IAdminAddExerciseFacade>();
            _adminAddTrainingAreaSut = _scope.ServiceProvider.GetRequiredService<IAdminAddTrainingAreaFacade>();

            _traningAreaSut = _scope.ServiceProvider.GetRequiredService<ITraningAreaService>();
            _exerciseServiceSut = _scope.ServiceProvider.GetRequiredService<IExerciseService>();
        }

        public void Dispose() => _scope.Dispose();

        [Fact]
        public async Task CreateAStandardExerciseWithNormalParamters_ExpectedZeroErrors()
        {
            var chest = await _traningAreaSut.GetTargetAreaByName("Chest");
            //A
            var newEx = new ExerciseDTO
            {
                Name = "Chest Flys",
                TargetAreaNames = new List<TargetAreaDTO>
                {
                   chest
                },
                VideoSourceLink= "https://www.youtube.com/watch?v=gGdGFtwCNBE&list=RDgGdGFtwCNBE&start_radio=1"
            };

            var erros = await _admingExerciseSut.AddExercise(newEx);

            Assert.Empty(erros);
        }
        [Fact]
        public async Task CreateExercise_AddAndThenGet_ReturnsValid()
        {
            var chest = await _traningAreaSut.GetTargetAreaByName("Chest");
            var ex = new ExerciseDTO
            {
                Name = "Kneel",
                TargetAreaNames = new List<TargetAreaDTO>
                {
                   chest,
                }
            };

            var validNoErrorsReturned = await _admingExerciseSut.AddExercise(ex);
            Assert.Empty(validNoErrorsReturned);

            var find = await _exerciseServiceSut.GetExerciseByName("Kneel");
            Assert.NotNull(find);
        }
        [Fact]
        public async Task ImproperExercise_MissingName_WillReturnErros()
        {
            //A
            var newEx = new ExerciseDTO
            {
                TargetAreaNames = new List<TargetAreaDTO>
                {
                    new TargetAreaDTO
                    {
                        Name ="Legs"
                    }
                },
                VideoSourceLink = "https://www.youtube.com/watch?v=gGdGFtwCNBE&list=RDgGdGFtwCNBE&start_radio=1"
            };

            var erros = await _admingExerciseSut.AddExercise(newEx);

            Assert.NotEmpty(erros);
        }

        [Fact]
        public async Task CreateANewTargetArea_ProperInputForFields_ReturnsNoErrors()
        {
            TargetAreaDTO targetAreaDTO = new TargetAreaDTO
            {
                Name = "Quadriceps"
            };

            var erros = await _adminAddTrainingAreaSut.AddTargetArea(targetAreaDTO);

            Assert.Empty(erros);
        }
        [Fact]
        public async Task TryCreateTargetArea_WithNoName_ReturnsErrors()
        {
            TargetAreaDTO targetAreaDTO = new TargetAreaDTO
            {
                Name = ""
            };

            var erros = await _adminAddTrainingAreaSut.AddTargetArea(targetAreaDTO);

            Assert.NotEmpty(erros);
        }
        [Fact]
        public async Task TryCreateArea_AlreadyExistsOne_ReturnsErrors()
        {
            TargetAreaDTO targetAreaDTO = new TargetAreaDTO
            {
                Name = "Chest"
            };

            var erros = await _adminAddTrainingAreaSut.AddTargetArea(targetAreaDTO);

            Assert.NotEmpty(erros);
        }
    }
}
