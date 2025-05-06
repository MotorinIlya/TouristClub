using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public partial class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHello() => Ok("Hello world");

    [HttpGet("secure")]
    [Authorize]
    public IActionResult GetSecureHello() => Ok("SECURE HELLO WORLD!!! HELPME");
}