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

        [HttpPost("join")]
        public async Task<ActionResult<VirtualQueue>> JoinQueue([FromBody] VirtualQueue request)
        {
            try
            {
                var attractionId = request.AttractionId;
                var userId = request.UserId;

                Console.WriteLine("Received request:");
        Console.WriteLine($"AttractionId: {request.AttractionId}, UserId: {request.UserId}");


                var userQueueCount = await _context.VirtualQueue
                .Where(q => q.UserId == userId)
                .CountAsync();

            // Check if the user has already joined two attractions
            if (userQueueCount >= 2)
            {
                return BadRequest("Je kan maar bij 2 attracties tegelijkertijd wachten");
            }

                var attraction = await _context.Attracties.FindAsync(attractionId);

                if (attraction == null)
                    return NotFound("Attractie niet gevonden");

                // Check if the user is already in the queue for the same attraction
                var existingQueueEntry = await _context.VirtualQueue
                    .FirstOrDefaultAsync(q => q.UserId == userId && q.AttractionId == attractionId);

                if (existingQueueEntry != null)
                    return BadRequest("Je staat al in de rij voor deze attractie");

                var queueCountForAttraction = await _context.VirtualQueue
                .Where(q => q.AttractionId == attractionId)
                .CountAsync();

                //is attractie vol
                if (queueCountForAttraction < attraction.Capaciteit)
                {
                    var entryTime = DateTime.Now;
                    var queueEntry = new VirtualQueue
                    {
                        UserId = userId,
                        AttractionId = attractionId
                    };

                    _context.VirtualQueue.Add(queueEntry);
                    await _context.SaveChangesAsync();

                    return Ok(queueEntry);
                }
                else
                {
                    return BadRequest("De rij is helaas vol");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
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
                return NotFound();

            return Ok(queue);
        }

        [HttpDelete("leave")]
        public async Task<IActionResult> LeaveQueue([FromBody] VirtualQueue leaveRequest)
        {
            try
            {
                var userId = leaveRequest.UserId;
                var attractionId = leaveRequest.AttractionId;

                var queueEntry = await _context.VirtualQueue
                    .FirstOrDefaultAsync(q => q.UserId == userId && q.AttractionId == attractionId);

                if (queueEntry == null)
                    return NotFound("User is not in the queue for this attraction.");

                _context.VirtualQueue.Remove(queueEntry);
                await _context.SaveChangesAsync();

                return Ok($"User {userId} heeft de rij verlaten van de {attractionId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
    }
