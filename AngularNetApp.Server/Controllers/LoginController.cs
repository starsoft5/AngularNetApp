using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly HeavensPlaceContext _heavensPlaceContext;
        public LoginController(HeavensPlaceContext dbContext)
        {
            _heavensPlaceContext = dbContext;
        }

        [HttpGet]
        public int? Get([FromBody] Login login)
        {
            Member user = _heavensPlaceContext.Members.FirstOrDefault(e => e.Email == login.Email);

            if (user != null)
            {
                return 1;
                 
            }
            return -1;
        }

        [HttpPost]
        public int? Post([FromBody] Login login)
        {
            Member user = _heavensPlaceContext.Members.FirstOrDefault(e => e.Email == login.Email);

            if (user != null)
            {
                bool userFound = PasswordHasher.VerifyPassword(login.Password, string.Concat(user?.PasswordSalt, ".", user?.PasswordHash));

                if (userFound)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            return -1;
        }
    }
}
