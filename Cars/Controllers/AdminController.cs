using Cars.Models;
using Cars.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Cars.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICarService _carService;
        private readonly IInquiryService _inquiryService; // Inject the Inquiry Service
        private readonly string _baseUrl;
        private readonly IContactUsService _contactUsService;
        private readonly ICarRequestService _carRequestService;


        public AdminController(ICarService carService, IInquiryService inquiryService, IConfiguration configuration, IContactUsService contactUsService, ICarRequestService carRequestService)
        {
            _carService = carService;
            _inquiryService = inquiryService; // Initialize Inquiry Service
            _baseUrl = configuration["ApiSettings:BaseUrl"] + "/api/Car";
            _contactUsService = contactUsService;
            _carRequestService = carRequestService;

        }


        // GET: Admin/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginModel model)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{_baseUrl}/login", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }
            }
        }

        // GET: Admin/Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }

        // GET: Admin/Cars
        public async Task<IActionResult> Cars()
        {
            var cars = await _carService.GetAllCars();
            return View(cars);
        }

        // GET: Admin/Car/{id}
        // GET: Admin/CarDetails/
        public async Task<IActionResult> CarDetails(int id)
        {
            var car = await _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound(); 
            }
            return View(car);
        }


        // POST: Admin/DeleteCar/{id}
        [HttpPost]
        public async Task<IActionResult> DeleteCar(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{_baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Cars");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete car.");
                }
            }

            return RedirectToAction("Cars");
        }


        // GET: Admin/AddCar
        public IActionResult AddCar()
        {
            return View();
        }

        // POST: Admin/AddCar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCar([Bind("Make,Model,Price,Picture")] CarInputViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(viewModel.Make), "Make");
                    content.Add(new StringContent(viewModel.Model), "Model");
                    content.Add(new StringContent(viewModel.Price.ToString()), "Price");

                    if (viewModel.Picture != null && viewModel.Picture.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await viewModel.Picture.CopyToAsync(memoryStream);
                            content.Add(new ByteArrayContent(memoryStream.ToArray()), "Picture", viewModel.Picture.FileName);
                        }
                    }

                    var response = await client.PostAsync(_baseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Cars");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Failed to add car. Status code: {response.StatusCode}");
                    }
                }
            }

            return View(viewModel);
        }
        // GET: Admin/Inquiries
        public async Task<IActionResult> Inquiries()
        {
            var inquiries = await _inquiryService.GetAllInquiries();
            return View(inquiries);
        }

        // PUT: Admin/UpdateInquiryStatus
        [HttpPost]
        public async Task<IActionResult> UpdateInquiryStatus(int id, string newStatus)
        {
            if (await _inquiryService.UpdateInquiryStatus(id, newStatus))
            {
                return RedirectToAction("Inquiries");
            }
            else
            {
                ModelState.AddModelError("", "Failed to update inquiry status.");
                return RedirectToAction("Inquiries");
            }
        }
        // GET: Admin/ContactUsMessages
        public async Task<IActionResult> ContactUsMessages()
        {
            var messages = await _contactUsService.GetAllMessages();
            return View(messages);
        }

        // GET: Admin/CarRequests
        public async Task<IActionResult> CarRequests()
        {
            var carRequests = await _carRequestService.GetAllCarRequests();
            return View(carRequests);
        }



    }
}
