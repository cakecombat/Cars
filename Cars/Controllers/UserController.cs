using Cars.Models;
using Cars.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cars.Controllers
{
    public class UserController : Controller
    {
        private readonly ICarService _carService;
        private readonly IInquiryService _inquiryService;
        private readonly ICarRequestService _carRequestService;
        private readonly IContactUsService _contactUsService; // Declare the service

        public UserController(ICarService carService, IInquiryService inquiryService, ICarRequestService carRequestService, IContactUsService contactUsService) // Inject the service
        {
            _carService = carService;
            _inquiryService = inquiryService;
            _carRequestService = carRequestService;
            _contactUsService = contactUsService; // Assign the service
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
            return View(inquiry);
        }

        // POST: /User/InquiryForm
        [HttpPost]
        public async Task<IActionResult> SubmitInquiry(Inquiry inquiry)
        {
            if (ModelState.IsValid)
            {
                await _inquiryService.SubmitInquiry(inquiry);
                return RedirectToAction("CarListing");
            }
            return View("InquiryForm", inquiry);
        }

        [HttpPost]
        [Route("api/Car/car-requests")]
        public async Task<IActionResult> SubmitCarRequest([FromBody] CarRequestApiModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _carRequestService.SubmitCarRequest(request);

            if (result != null)
            {
                return Ok(new { message = "Car request submitted successfully" });
            }

            return BadRequest(new { message = "Failed to submit car request" });
        }

        [HttpPost]
        [Route("api/ContactUs")]
        public async Task<IActionResult> SubmitContactUs([FromBody] ContactUsApiModel contactUsMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactUs = new ContactUs
            {
                Name = contactUsMessage.Name,
                Email = contactUsMessage.Email,
                Message = contactUsMessage.Message,
                SubmittedAt = DateTime.UtcNow
            };

            await _contactUsService.AddMessage(contactUs);
            return Ok(new { message = "Contact message submitted successfully" });
        }

    }
}
