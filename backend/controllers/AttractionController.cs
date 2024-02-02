using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttractionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attractie>>> GetAttractions()
        {
            var attractions = await _context.Attracties.Include(a => a.VirtualQueue).ToListAsync();
            return Ok(attractions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attractie>> GetAttraction(int id)
        {
            var attraction = await _context.Attracties.Include(a => a.VirtualQueue) // Include related VirtualQueue
        .FirstOrDefaultAsync(a => a.AttractieId == id);

            if (attraction == null)
            {
                return NotFound();
            }

            return Ok(attraction);
        }
    }
}
