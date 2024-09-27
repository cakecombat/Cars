using Cars.Models;

namespace Cars.Services
{
    public interface ICarRequestService
    {
        Task<List<CarRequest>> GetAllCarRequests();

        Task<CarRequestApiModel> SubmitCarRequest(CarRequestApiModel request);
    }
}