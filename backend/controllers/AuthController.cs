using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;


namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User user)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Gebruikersnaam == user.Gebruikersnaam && u.Wachtwoord == user.Wachtwoord);

        if (existingUser != null)
        {
            return Ok(new { Token = "your_generated_token", UserId = existingUser.Id });
        }
        else
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }
    }

    [HttpGet("gebruikers")]
public async Task<ActionResult<IEnumerable<User>>> GetUsers()
{
    var users = await _context.Users
        .Include(u => u.VirtualQueue) 
        .ToListAsync();

    return Ok(users);
}
}
