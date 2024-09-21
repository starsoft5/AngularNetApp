using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private readonly HeavensPlaceContext _heavensPlaceContext;
        public RegisterController(HeavensPlaceContext dbContext)
        {
            _heavensPlaceContext = dbContext;
        }

        [HttpGet]
        public bool Get()
        {
            return true;
        }


        [HttpPost]
        public int Post([FromBody] Register member)
        {
            string password = member.Password;
            string hashedPassword = PasswordHasher.HashPassword(password);

            string[] parts = hashedPassword.Split('.');

            string salt = parts[0];
            string hashed = parts[1];           
            
            try
            {
                _heavensPlaceContext.Members.Add(new Member { Email = member.Email, PasswordHash = hashed, PasswordSalt = salt });
                _heavensPlaceContext.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
