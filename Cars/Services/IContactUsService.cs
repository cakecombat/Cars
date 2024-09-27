using Cars.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.Services
{
    public interface IContactUsService
    {
        Task<IEnumerable<ContactUs>> GetAllMessages();
        Task AddMessage(ContactUs message);
        Task AddContactUsMessage(ContactUsApiModel contactUsMessage); 
    }

}
