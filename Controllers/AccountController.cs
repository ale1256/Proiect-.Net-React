using Microsoft.AspNetCore.Mvc;
using AspNetApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterView model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Check if user/email exists
        if (_context.Clients.Any(c => c.Email == model.Email))
        {
            ModelState.AddModelError("", "Email already registered");
            return View(model);
        }

        // Generate salt
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash password with salt
        var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: model.Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        // Store salt and hash as strings
        var client = new Client
        {
            Name = model.FullName,
            Email = model.Email,
            PasswordHash = hashedPassword,
            PasswordSalt = Convert.ToBase64String(salt)
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginView model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var client = _context.Clients.FirstOrDefault(c => c.Email == model.Email);
        if (client == null)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        // Get salt and hash
        var saltBytes = Convert.FromBase64String(client.PasswordSalt);
        var enteredPasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: model.Password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        if (enteredPasswordHash != client.PasswordHash)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        // At this point, authentication is successful
        // You can add cookie-based authentication here (optional)

        return RedirectToAction("Index", "Home");
    }
}
