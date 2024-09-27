using System.ComponentModel.DataAnnotations;

namespace Cars.Models
{
    public class CarRequest
    {
        public int Id { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public DateTime DateRequested { get; set; }

        public Car Car { get; set; } 
    }
    public class CarRequestApiModel
    {
        public int CarId { get; set; }

        [Required]
        [StringLength(100)]
        public string RequesterName { get; set; }

        [Required]
        [EmailAddress]
        public string RequesterEmail { get; set; }
    }
}
