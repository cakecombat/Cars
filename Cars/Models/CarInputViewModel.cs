using System.ComponentModel.DataAnnotations;

namespace Cars.Models
{
    public class CarInputViewModel
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public IFormFile Picture { get; set; }
    }
}