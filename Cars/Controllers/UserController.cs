using Cars.Models;
using Cars.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    public class UserController : Controller
    {
        private readonly ICarService _carService;
        private readonly IInquiryService _inquiryService;

        public UserController(ICarService carService, IInquiryService inquiryService)
        {
            _carService = carService;
            _inquiryService = inquiryService;
        }

        // GET: /User/CarListing
        [HttpGet]
        public async Task<IActionResult> CarListing()
        {
            var cars = await _carService.GetAllCars();
            return View(cars);
        }

        // GET: /User/CarDetails/{id}
        [HttpGet]
        public async Task<IActionResult> CarDetails(int id)
        {
            var car = await _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // GET: /User/InquiryForm/{carId}
        [HttpGet]
        public IActionResult InquiryForm(int carId)
        {
            var inquiry = new Inquiry
            {
                CarId = carId
            };
            return View(inquiry); // InquiryForm.cshtml will allow the user to input their inquiry details
        }

        // POST: /User/InquiryForm
        [HttpPost]
        public async Task<IActionResult> SubmitInquiry(Inquiry inquiry)
        {
            if (ModelState.IsValid)
            {
                await _inquiryService.SubmitInquiry(inquiry);
                return RedirectToAction("CarListing"); // Redirect to the car listing after a successful inquiry
            }
            return View("InquiryForm", inquiry); // Redisplay the form if validation fails
        }
    }
}

