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
            var attractions = await _context.Attracties.ToListAsync();
            return Ok(attractions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attractie>> GetAttraction(int id)
        {
            var attraction = await _context.Attracties.FindAsync(id);

            if (attraction == null)
            {
                return NotFound();
            }

            return Ok(attraction);
        }
        [HttpGet("attractions")]
        public async Task<ActionResult<IEnumerable<Attractie>>> counter()
        {
            var attractions = await _context.Attracties
                .Select(attraction => new Attractie
                {
                    AttractieId = attraction.AttractieId,
                    Naam = attraction.Naam,
                    Capaciteit = attraction.Capaciteit,
                    Duur = attraction.Duur,
                    Beschrijving = attraction.Beschrijving,
                    AfbeeldingUrl = attraction.AfbeeldingUrl,
                    Duration = attraction.Duration,
                    VirtualQueueCount = _context.VirtualQueue
                        .Where(q => q.AttractionId == attraction.AttractieId)
                        .Count()
                })
                .ToListAsync();

            return Ok(attractions);
        }

    }
}
