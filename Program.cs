using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using AspNetApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Add DbContext (PostgreSQL)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3. Configure CORS to allow React frontend at localhost:3000
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 4. Register strongly typed JwtSettings to get the key from appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// 5. Add Controllers with OData support
builder.Services.AddControllersWithViews()
    .AddOData(opt =>
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<Person>("People");

        var loginAction = odataBuilder.Action("Login");
        loginAction.Parameter<string>("Email");
        loginAction.Parameter<string>("Password");
        loginAction.Returns<string>();

        opt.AddRouteComponents("odata", odataBuilder.GetEdmModel())
            .Select()
            .Filter()
            .OrderBy()
            .Expand()
            .Count();
    });

// 6. Add HttpClient if you call external APIs
builder.Services.AddHttpClient();

// 7. Get the JWT key from configuration (assumes appsettings.json contains "Jwt": { "Key": "your_secret_key_here" })
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
if (string.IsNullOrEmpty(jwtSettings?.Key))
{
    throw new Exception("JWT Key is not configured in appsettings.json");
}

// 8. Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,  // In production, set to true and configure valid issuer
        ValidateAudience = false, // In production, set to true and configure valid audience
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

var app = builder.Build();

// Middleware pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Uncomment if you want HTTPS redirection
// app.UseHttpsRedirection();

// Serve React static files from "client/build"
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "client", "build")),
    RequestPath = ""
});

app.UseRouting();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

// Map attribute-routed API controllers (e.g., /api/auth/register)
app.MapControllers();

// Map default MVC route (optional)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Fallback unmatched routes to React SPA index.html
app.MapFallbackToFile("index.html");

app.Run();

// JwtSettings class for strongly typed config binding
public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
}
