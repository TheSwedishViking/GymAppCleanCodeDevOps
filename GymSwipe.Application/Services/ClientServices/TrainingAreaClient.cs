using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.ClientInterfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services.ClientServices
{
    public class TrainingAreaClient : ITrainingAreasClient
    {
        private readonly HttpClient _http;
        public TrainingAreaClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<TargetAreaDTO> GetTargetAreaDTOById(int id)
        {
            var respone = await _http.GetFromJsonAsync<TargetAreaDTO>($"api/TrainingArea/get-area-by-id/{id}");
            if(respone == null)
            {
                throw new Exception("Response empty or null!");
            }
            return respone;
        }

        public async Task<List<TargetAreaDTO>> GetTargetAreaDTOsAsync()
        {
            var response = await _http.GetFromJsonAsync<List<TargetAreaDTO>>("api/TrainingArea/get-all-areas");
            if(response == null)
            {
                throw new Exception("Response empty or null!");
            }
            return response;
        }

        public async Task<string> GetTargetAreaImage(int id)
        {
            var response = await _http.GetAsync($"api/TrainingArea/get-image/{id}");
            response.EnsureSuccessStatusCode();
            if (response == null)
            {
                throw new Exception("Response empty or null!");
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
