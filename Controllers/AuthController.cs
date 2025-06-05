using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ODataController
{
    [HttpPost]
    [Route("odata/Login")]
    public IActionResult Login([FromBody] LoginView model)
    {
        if (ModelState.IsValid)
        {
            // Replace with actual authentication logic
            if (model.Email == "test@example.com" && model.Password == "password")
            {
                return Ok("Login successful!"); // Return a token or success message
            }
            return Unauthorized("Invalid credentials");
        }
        return BadRequest(ModelState);
    }

    [HttpPost]
    [Route("odata/Register")]
    public IActionResult Register([FromBody] RegisterView model)
    {
        if (ModelState.IsValid)
        {
            // Replace with actual registration logic
            if (model.Email == "test@example.com") // Example: Check if email already exists
            {
                return BadRequest("Email is already registered.");
            }

            // Simulate successful registration
            return Ok("Registration successful!");
        }
        return BadRequest(ModelState);
    }
}