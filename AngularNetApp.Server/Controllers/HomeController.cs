using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

[ApiController]
[Route("api/[controller]")]
public class SelectionController : ControllerBase
{
    private readonly HeavensPlaceContext _context;

    public SelectionController(HeavensPlaceContext context)
    {
        _context = context;
    }

    [HttpGet("options")]
    public IActionResult GetMembers()
    {
        // Fetch options from the database
        var options = _context.Members.ToList();
        return Ok(options);
    }

    [HttpPost("select")]
    public IActionResult SelectOption([FromBody] int optionId)
    {
        // Store the selected option in the session
        HttpContext.Session.SetInt32("SelectedOption", optionId);
        return Ok();
    }

    [HttpGet("selected")]
    public IActionResult GetSelectedOption()
    {
        var selectedOption = HttpContext.Session.GetInt32("SelectedOption");
        return Ok(selectedOption);
    }
}
