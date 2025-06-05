using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using AspNetApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// 1. Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3. Add CORS policy for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// 4. Add OData + MVC
builder.Services.AddControllersWithViews()
    .AddOData(opt =>
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<Person>("People");

        // Custom OData action
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

// 5. Add HttpClient
builder.Services.AddHttpClient();

var app = builder.Build();

// 6. Middleware

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();

// ✅ Serve static files from React build
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "client", "build")),
    RequestPath = ""
});

app.UseRouting();

app.UseCors("AllowReactApp");

app.UseAuthorization();

// ✅ API routes (OData, MVC)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ Fallback to React's index.html for any non-API routes
app.MapFallbackToFile("index.html");

app.Run();
