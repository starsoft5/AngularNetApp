using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobPortalController : ControllerBase
    {
        private readonly HeavensPlaceContext _heavensPlaceContext;

        public JobPortalController(HeavensPlaceContext dbContext)
        {
            _heavensPlaceContext = dbContext;
        }
        

        [HttpPost]
        public void Post([FromBody] Member data)
        {
            var passwordAndSalt = PasswordHasher.HashPassword(data.PasswordHash).Split(".");  
            Member member = new Member { Email = data.Email, PasswordHash = passwordAndSalt[0].ToString(), PasswordSalt = passwordAndSalt[1].ToString() };

            _heavensPlaceContext.Members.Add(member);
            _heavensPlaceContext.SaveChanges();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Member>> Get()
        {
            //var items = new List<string> { "Item 1", "Item 2", "Item 3" };
            //return Ok(items);
            var memberList  = _heavensPlaceContext.Members.OrderBy(member => member.Email).ToList();
            //_httpContextAccessor.HttpContext.Session.Set("Options", memberList);
            return memberList;
        }

         

        [HttpDelete]
        public void Delete(int id)
        {
           var member = _heavensPlaceContext.Members.Find(id);
            _heavensPlaceContext.Remove(member!);
            _heavensPlaceContext.SaveChanges(); 
        }

        [HttpPut]
        public void Update([FromBody] Login data)
        {
            Member member = _heavensPlaceContext.Members.Find(data.Id);
            if (member == null)
                return;
            member.Email = data.Email;
            _heavensPlaceContext.Update(member);
            _heavensPlaceContext.SaveChanges();
        }

      /*  [HttpGet]
        public IActionResult GetReport()
        {
            // Load and configure the Crystal Report
            var report = new ReportDocument();
            report.Load(@"F:\TEMP31\CrystalReportsWebSite1\CrystalReport1.rpt");

            // Set parameters or datasource if needed
            // report.SetParameterValue("ParameterName", value);
            // report.SetDataSource(dataSource);

            // Export report to PDF
            using (var stream = new MemoryStream())
            {
                report.ExportToDisk(ExportFormatType.PortableDocFormat, stream.ToString());
                return File(stream.ToArray(), $"application/pdf", "CrystalReport1.pdf");
            }
        } */

    }

    
}


public class FromBodyMember
{
    public int? Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }  
}

public class FromBodyData
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    //public Address? Address { get; set; }
}

public class Address
{
   public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
}


