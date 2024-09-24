using Cars.Models;
using Cars.Services;
using System.Text;
using Newtonsoft.Json;

namespace Cars.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly HttpClient _httpClient;

        public InquiryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Submit an inquiry to /api/Inquiries
        public async Task SubmitInquiry(Inquiry inquiry)
        {
            var content = new StringContent(JsonConvert.SerializeObject(inquiry), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Inquiries", content);
            response.EnsureSuccessStatusCode(); // Throw if the request was unsuccessful
        }
        public async Task<IEnumerable<Inquiry>> GetAllInquiries()
        {
            var response = await _httpClient.GetAsync("/api/Inquiries");
            response.EnsureSuccessStatusCode();

            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Inquiry>>(jsonData);
        }

        public async Task<bool> UpdateInquiryStatus(int id, string newStatus)
        {
            var content = new StringContent($"\"{newStatus}\"", Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Inquiries/{id}/status", content);
            return response.IsSuccessStatusCode;
        }
    }
}