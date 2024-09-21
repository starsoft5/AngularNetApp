using Microsoft.AspNetCore.Mvc;
//using System.Web.Http.Cors;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DependentController : Controller
    {
        private readonly HeavensPlaceContext _heavensPlaceContext;

        public DependentController(HeavensPlaceContext dbContext)
        {
            _heavensPlaceContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Dependent>> Get()
        {
            var dependents = _heavensPlaceContext.Dependents.ToList();
            return dependents;
        }

        [HttpPost]
        public void Post([FromBody] Dependent data)
        {
            Dependent dependent = new Dependent {   Name = data.Name, MemberId = data.MemberId };
            _heavensPlaceContext.Dependents.Add(dependent);
            _heavensPlaceContext.SaveChanges();
        }
    }
}
