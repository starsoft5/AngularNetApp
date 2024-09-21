using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebApplication1.Models;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly HeavensPlaceContext _heavensPlaceContext;

        public ProductController(HeavensPlaceContext dbContext)
        {
            _heavensPlaceContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IList<Product> list = _heavensPlaceContext.Products.OrderBy(p => p.Name).ToList();
            return Ok(list);
        }

    
        [HttpGet("IsInvoiceNoDuplicate")]
        public bool IsInvoiceNoDuplicate(string invoiceNo)
        {
            if (ProductHelper.IsInvoiceNoDuplicate(invoiceNo, _heavensPlaceContext))
                return true;
            
            return false;
        }


        [HttpPost]
        public IActionResult Post([FromBody] ProductClient productClient)
        {
            ProductHelper.CreateInvoiceWithOrders(productClient, _heavensPlaceContext);
            return Ok();
        }

        

    }
}
