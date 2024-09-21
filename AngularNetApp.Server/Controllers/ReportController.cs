using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Text;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly HeavensPlaceContext _heavensPlaceContext;

        public ReportController(HeavensPlaceContext dbContext)
        {
            _heavensPlaceContext = dbContext;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Member>> Get()
        {
            //var items = new List<string> { "Item 1", "Item 2", "Item 3" };
            //return Ok(items);
            var memberList = _heavensPlaceContext.Members.ToList();
            //_httpContextAccessor.HttpContext.Session.Set("Options", memberList);
            return memberList;
        }


        /*
        [HttpGet]
        public ActionResult<string> Get()
        {

            StringBuilder sb = new StringBuilder();
            
            //var items = new List<string> { "Item 1", "Item 2", "Item 3" };
            //return Ok(items);
            var memberList = _heavensPlaceContext?.Members.ToList();
            //_httpContextAccessor.HttpContext.Session.Set("Options", memberList);
            foreach (var member in memberList)
            {
                sb.AppendLine(member.Firstname);
            }
            return sb.ToString();
        }
        */
    }
}
