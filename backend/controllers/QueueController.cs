using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QueueController(AppDbContext context)
        {
            _context = context;
        }

    [HttpPost("join/{AttractieId}")]
    public async Task<ActionResult<VirtualQueue>> JoinQueue(int AttractieId)
    {
        try
        {
            var attraction = await _context.Attracties.FindAsync(AttractieId);

            if (attraction == null)
                return NotFound();

            // volgende user ID
            var nextUserId = _context.Users.Max(u => u.Id) + 1;

            // Controleer of de gebruiker al in de wachtrij staat voor dezelfde attractie
            if (attraction.VirtualQueue.Count < attraction.Capaciteit)
            {
                var entryTime = DateTime.Now;
                var queueEntry = new VirtualQueue
                {
                    UserId = nextUserId,
                    AttractionId = AttractieId,
                    EntryTime = entryTime,
                    IsPresent = false
                };

                _context.VirtualQueue.Add(queueEntry);
                await _context.SaveChangesAsync();

                return Ok(queueEntry);
            }
            else
            {
                return BadRequest("The attraction queue is full.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<VirtualQueue>>> GetQueues()
        {
            var queues = await _context.VirtualQueue.ToListAsync();
            return Ok(queues);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VirtualQueue>> GetQueue(int id)
        {
            var queue = await _context.VirtualQueue.FindAsync(id);

            if (queue == null)
            {
                return NotFound();
            }

            return Ok(queue);
        }
    }
}
