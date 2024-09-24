using Cars.Models;

namespace Cars.Services
{
    public interface IInquiryService
    {
        Task SubmitInquiry(Inquiry inquiry);
        Task<IEnumerable<Inquiry>> GetAllInquiries();
        Task<bool> UpdateInquiryStatus(int id, string newStatus);
    }
}
