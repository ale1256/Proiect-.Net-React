using Microsoft.AspNetCore.Mvc;
using AspNetApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/auth")]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly string _key;

    public AccountController(ApplicationDbContext context, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _key = jwtSettings.Value.Key;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterView model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Invalid data" });

        if (_context.Clients.Any(c => c.Email == model.Email))
            return BadRequest(new { message = "Email already registered" });

        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: model.Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        var client = new Client
        {
            Name = model.FullName,
            Email = model.Email,
            PasswordHash = hashedPassword,
            PasswordSalt = Convert.ToBase64String(salt)
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Registration successful" });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginView model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Invalid data" });

        var client = _context.Clients.FirstOrDefault(c => c.Email == model.Email);
        if (client == null)
            return Unauthorized(new { message = "Invalid email or password." });

        var saltBytes = Convert.FromBase64String(client.PasswordSalt);
        var enteredPasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: model.Password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        if (enteredPasswordHash != client.PasswordHash)
            return Unauthorized(new { message = "Invalid email or password." });

        // Create JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var keyBytes = Encoding.UTF8.GetBytes(_key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, client.Name ?? client.Email),
                new Claim(ClaimTypes.NameIdentifier, client.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new
        {
            message = "Login successful",
            clientId = client.Id,
            name = client.Name,
            token = tokenString
        });
    }
}
