using Microsoft.AspNetCore.Mvc;
using AspNetApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class GymClassesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public GymClassesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetClasses()
    {
        var classes = await _context.GymClasses
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.StartTime,
                c.TrainerName,
                c.SpotsLeft
            })
            .ToListAsync();

        return Ok(classes);
    }

    [Authorize]
    [HttpPost("{id}/book")]
    public async Task<IActionResult> BookClass(int id)
    {
        var gymClass = await _context.GymClasses.FindAsync(id);
        if (gymClass == null) return NotFound();

        if (gymClass.SpotsLeft <= 0)
            return BadRequest("No spots left for this class.");

        gymClass.SpotsLeft--;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Class booked successfully",
            SpotsLeft = gymClass.SpotsLeft,
            User = User.Identity?.Name ?? "Guest"
        });
    }

}
