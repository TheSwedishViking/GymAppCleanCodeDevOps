using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace GymSwipe.UserTests
{
    public class ExerciseTestsAPI:IClassFixture<UserApiFixture>
    {
        private readonly HttpClient _client;
        public ExerciseTestsAPI(UserApiFixture fixture)
        {
            _client = fixture.GetClient();
        }

        [Fact]
        public async Task RequestAllExercies_ValidateThatCollectionIsNotEmpty_ReturnsExpected_LoadsOfExercises()
        {
            var response = await _client.GetAsync("api/Exercise/get-all-exercises");
            response.EnsureSuccessStatusCode();

            var dtos = await response.Content.ReadFromJsonAsync<List<ExerciseDTO>>();

            Assert.NotEmpty(dtos);
        }
        [Fact]
        public async Task Request_ChestExercieses_ReturnsExpected_Collection()
        {
            var expected = "Chest";
            var response = await _client.GetAsync("api/Exercise/get-chest-exercises");
            response.EnsureSuccessStatusCode();

            var dtos = await response.Content.ReadFromJsonAsync<List<ExerciseDTO>>();

            Assert.NotEmpty(dtos);
            Assert.All(dtos, c => Assert.Contains(c.TargetAreaNames, n => n.Contains(expected, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
