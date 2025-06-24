using Microsoft.AspNetCore.Mvc;
using AspNetApp.Models;
using System.Collections.Generic;
using AspNetApp.Models;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]  // matches /api/membership
public class MembershipController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MembershipController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetMemberships()
    {
        var options = new List<Membership>
            {
                new Membership { Id = 1, PlanName = "Basic", Description = "Access during staffed hours", PricePerMonth = 29.99M, DurationInMonths = 1 },
                new Membership { Id = 2, PlanName = "Premium", Description = "24/7 access + monthly trainer session", PricePerMonth = 49.99M, DurationInMonths = 1 },
                new Membership { Id = 3, PlanName = "Elite", Description = "Unlimited access + trainer + nutrition", PricePerMonth = 79.99M, DurationInMonths = 1 },
            };
        return Ok(options);
    }

    [HttpPost("buy")]
    public async Task<IActionResult> BuyMembership([FromBody] BuyRequest request)
    {
        Console.WriteLine($"BuyMembership called with ClientId={request.ClientId}, MembershipId={request.MembershipId}");

        var client = await _context.Clients.FindAsync(request.ClientId);
        if (client == null)
        {
            Console.WriteLine("Client not found.");
            return NotFound(new { message = "Client not found." });
        }

        var membership = await _context.Memberships.FindAsync(request.MembershipId);
        if (membership == null)
        {
            Console.WriteLine("Membership not found.");
            return NotFound(new { message = "Membership not found." });
        }

        var exists = await _context.UserMemberships.AnyAsync(um =>
         um.ClientId == request.ClientId && um.MembershipId == request.MembershipId);

        if (exists)
            return BadRequest(new { message = "Membership already purchased." });

        var userMembership = new UserMembership
        {
            ClientId = request.ClientId,
            MembershipId = request.MembershipId,
            PurchaseDate = DateTime.UtcNow
        };

        _context.UserMemberships.Add(userMembership);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Membership purchased successfully." });
    }

    [HttpGet("my-memberships/{clientId}")]
    public async Task<IActionResult> GetClientMemberships(int clientId)
    {
        var memberships = await _context.UserMemberships
            .Where(um => um.ClientId == clientId)
            .Select(um => new
            {
                um.Id,
                um.Membership.PlanName,
                um.Membership.Description,
                um.Membership.PricePerMonth,
                um.PurchaseDate
            })
            .ToListAsync();

        return Ok(memberships);
    }

}

