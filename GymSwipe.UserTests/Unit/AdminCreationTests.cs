using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
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
        private readonly IAdminAddExerciseFacade _exerciseSut;
        private readonly IAdminAddTrainingAreaFacade _trainingAreaSut;
        public AdminCreationTests(StandardLocalInMemoryFixture fixture)
        {
            _fixture = fixture;
            _scope = _fixture.ServiceProvider.CreateScope();
            _exerciseSut = _scope.ServiceProvider.GetRequiredService<IAdminAddExerciseFacade>();
            _trainingAreaSut = _scope.ServiceProvider.GetRequiredService<IAdminAddTrainingAreaFacade>();
        }

        public void Dispose() => _scope.Dispose();

        [Fact]
        public async Task CreateAStandardExerciseWithNormalParamters_ExpectedZeroErrors()
        {
            //A
            var newEx = new ExerciseDTO
            {
                Name = "Lunge",
                TargetAreaNames = new List<TargetAreaDTO>
                {
                    new TargetAreaDTO
                    {
                        Name ="Legs"
                    }
                },
                VideoSourceLink= "https://www.youtube.com/watch?v=gGdGFtwCNBE&list=RDgGdGFtwCNBE&start_radio=1"
            };

            var erros = await _exerciseSut.AddExercise(newEx);

            Assert.Empty(erros);
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

            var erros = await _exerciseSut.AddExercise(newEx);

            Assert.NotEmpty(erros);
        }

        [Fact]
        public async Task CreateANewTargetArea_ProperInputForFields_ReturnsNoErrors()
        {
            TargetAreaDTO targetAreaDTO = new TargetAreaDTO
            {
                Name = "Quadriceps"
            };

            var erros = await _trainingAreaSut.AddTargetArea(targetAreaDTO);

            Assert.Empty(erros);
        }
        [Fact]
        public async Task TryCreateTargetArea_WithNoName_ReturnsErrors()
        {
            TargetAreaDTO targetAreaDTO = new TargetAreaDTO
            {
                Name = ""
            };

            var erros = await _trainingAreaSut.AddTargetArea(targetAreaDTO);

            Assert.NotEmpty(erros);
        }
        [Fact]
        public async Task TryCreateArea_AlreadyExistsOne_ReturnsErrors()
        {
            TargetAreaDTO targetAreaDTO = new TargetAreaDTO
            {
                Name = "Chest"
            };

            var erros = await _trainingAreaSut.AddTargetArea(targetAreaDTO);

            Assert.NotEmpty(erros);
        }
    }
}
