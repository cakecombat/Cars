using Cars.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ContactUsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] + "/api/ContactUs";
        }

        public async Task<IEnumerable<ContactUs>> GetAllMessages()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ContactUs>>(content);
            }
            return new List<ContactUs>();
        }

        public async Task AddMessage(ContactUs message)
        {
            var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_apiBaseUrl, content);
        }

        // Implement the new method
        public async Task AddContactUsMessage(ContactUsApiModel contactUsMessage)
        {
            var content = new StringContent(JsonConvert.SerializeObject(contactUsMessage), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_apiBaseUrl, content);
        }
    }
}
