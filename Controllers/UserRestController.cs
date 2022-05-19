using Microsoft.AspNetCore.Mvc;
using notebook.Model;

namespace notebook.Controllers;

[ApiController]
[Route("[controller]")]
public class UserRestController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<UserRestController> _logger;

    public UserRestController(ILogger<UserRestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new User
        {
         
        })
        .ToArray();
    }
}
