using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.UserTests.FixtureFolder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests
{
    public class CaloricTests: IClassFixture<StandardLocalInMemoryFixture>
    {
        private readonly StandardLocalInMemoryFixture _fixture;
        private readonly IServiceScope _scope;
        private readonly ICalorieCountingService _sut;
        public CaloricTests(StandardLocalInMemoryFixture fixture)
        {
            _fixture = fixture;
            _scope = _fixture.ServiceProvider.CreateScope();
            _sut = _scope.ServiceProvider.GetRequiredService<ICalorieCountingService>();
        }

        [Fact]
        public async Task InputStandardParamters_NoMetInput_CalculateWithMetAtFive_ReturnsExpectedRange()
        {
            TimeSpan twentyMinutes = new TimeSpan(0,minutes: 20,0);
            double weight = 82.5;

            var cals =  _sut.CalculateCalories(twentyMinutes, weight, null);

            Assert.InRange(cals, 120, 150);
        }

        [Fact]
        public async Task LessIntenseExercise_ReturnsAnExpectedLowerBurnedCalories()
        {
            TimeSpan tenMinutes = new TimeSpan(0, minutes: 10, 0);
            double weight = 82.5;

            //3.5 for a moderate walk
            var cals = _sut.CalculateCalories(tenMinutes, weight, 3.5);

            Assert.InRange(cals, 40, 60);
        }

        [Fact]
        public async Task IntenseExercise_LikeRunningReturnsHighCaloricBurn_ForExtendedPeriod()
        {
            TimeSpan twentyKilometerTime = new TimeSpan(hours: 1, minutes: 58, 0);
            double weight = 82.5;

            //Faster run, might be Oscars 20km time, who knows
            var cals = _sut.CalculateCalories(twentyKilometerTime, weight, 9.8);

            Assert.InRange(cals, 1400, 1700);
        }
    }
}
