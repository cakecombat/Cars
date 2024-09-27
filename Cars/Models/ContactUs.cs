namespace Cars.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
    public class ContactUsApiModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }

}
