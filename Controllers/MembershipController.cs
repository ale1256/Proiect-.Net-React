using Microsoft.AspNetCore.Mvc;
using AspNetApp.Models;

namespace AspNetApp.Controllers
{
    public class MembershipController : Controller
    {
        public IActionResult Index()
        {
            var options = new List<Membership>
            {
                new Membership { Id = 1, PlanName = "Basic", Description = "Access during staffed hours", PricePerMonth = 29.99M, DurationInMonths = 1 },
                new Membership { Id = 2, PlanName = "Premium", Description = "24/7 access + monthly trainer session", PricePerMonth = 49.99M, DurationInMonths = 1 },
                new Membership { Id = 3, PlanName = "Elite", Description = "Unlimited access + trainer + nutrition", PricePerMonth = 79.99M, DurationInMonths = 1 },
            };

            return View(options);
        }

        [HttpPost]
        public IActionResult Buy(int id)
        {
            TempData["Message"] = $"You successfully bought membership ID #{id}";
            return RedirectToAction("Index");
        }
    }
}
