using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetEngage.Api.Data;
using BetEngage.Api.Models;

namespace BetEngage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEvent>>> GetEvents()
        {
            return await _context.UserEvents.ToListAsync();
        }

        //POST
        [HttpPost]
        public async Task<ActionResult<UserEvent>> PostEvent(UserEvent input)
        {
            input.TimeStamp = DateTime.UtcNow;
            _context.UserEvents.Add(input);
            await _context.SaveChangesAsync();

            // User gets a reward if he has created more than 3 events in the last hour
            var oneHourAgo = DateTime.UtcNow.AddHours(-1);
            var recentEvents = await _context.UserEvents
                               .Where(e => e.UserId == input.UserId && e.TimeStamp >= oneHourAgo).CountAsync();

            if (recentEvents >= 3)
            {
                var reward = new Reward
                {
                    UserId = input.UserId,
                    Description = "Reward! 3 events in the last hour",
                    TimeStamp = DateTime.UtcNow
                };

                _context.Rewards.Add(reward);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetEvents), new { id = input.Id }, input);
        }

        // GET
        [HttpGet("rewards")]
        public async Task<ActionResult<IEnumerable<Reward>>> GetRewards()
        {
            return await _context.Rewards.ToListAsync();
        }
    }
}