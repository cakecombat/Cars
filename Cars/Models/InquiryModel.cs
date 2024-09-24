using System.ComponentModel.DataAnnotations;

namespace Cars.Models
{
    public class Inquiry
    {
        public int Id { get; set; }
        public int CarId { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? UserEmail { get; set; }
        [Required(ErrorMessage = "Message is required.")]
        public string? Message { get; set; }

        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
