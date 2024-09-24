using Cars.Models;
using Newtonsoft.Json;
using System.Text;

namespace Cars.Services
{
    public class CarService : ICarService
    {
        private readonly HttpClient _httpClient;

        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch all cars from /api/Car
        public async Task<IEnumerable<Car>> GetAllCars()
        {
            var response = await _httpClient.GetAsync("/api/Car");
            response.EnsureSuccessStatusCode();

            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Car>>(jsonData);
        }

        // Fetch a car by its ID from /api/Car/{id}
        public async Task<Car> GetCarById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Car/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Car>(jsonData);
            }
            return null; // Return null if car is not found
        }
        public async Task<bool> AddCar(Car car)
        {
            var content = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Car", content);
            return response.IsSuccessStatusCode;
        }
    }
}