using Cars.Models;

namespace Cars.Services
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<Car> GetCarById(int id);
        Task<bool> AddCar(Car car);
    }
}
