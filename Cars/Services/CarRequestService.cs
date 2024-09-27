using Cars.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cars.Services
{
    public class CarRequestService : ICarRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CarRequestService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"] + "/api/Car/car-requests";
        }

        public async Task<List<CarRequest>> GetAllCarRequests()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CarRequest>>(jsonResponse);
            }
            return null;
        }

        public async Task<CarRequestApiModel> SubmitCarRequest(CarRequestApiModel request)
        {
            // Use the API model directly here
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CarRequestApiModel>(jsonResponse);
            }

            return null;
        }


    }
}