using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetEngage.Api.Data;
using BetEngage.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BetEngage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class StatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatsController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet("rewards")]
        public async Task<IActionResult> GetRewardStats()
        {
            var totalRewards = await _context.Rewards.CountAsync();
            var rewardsPerUser = await _context.Rewards.GroupBy(r => r.UserId)
                                    .Select(g => new
                                    {
                                        UserId = g.Key,
                                        Count = g.Count()
                                    })
                                    .ToListAsync();

            return Ok(new
            {
                totalRewards = totalRewards,
                RewardsPerUser = rewardsPerUser
            });
        }

        //GET
        [HttpGet("user-activity")]
        public async Task<IActionResult> GetUserActivity()
        {
            var eventsPerUser = await _context.UserEvents.GroupBy(r => r.UserId)
                                        .Select(g => new
                                        {
                                            UserId = g.Key,
                                            Count = g.Count()
                                        })
                                        .ToListAsync();

            return Ok(eventsPerUser);
        }

        //GET
        [HttpGet("top-3-users")]
        public async Task<IActionResult> GetTopUsers()
        {
            var topUsers = await _context.UserEvents.GroupBy(r => r.UserId)
                                    .Select(g => new
                                    {
                                        UserId = g.Key,
                                        Count = g.Count()
                                    })
                                    .OrderByDescending(x => x.Count)
                                    .Take(3)
                                    .ToListAsync();

            return Ok(topUsers);
        }
    }
}