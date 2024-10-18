using Contract_Monthly_ClaimSystem__CMCS_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace Contract_Monthly_ClaimSystem__CMCS_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
                   // Fetch recent claims (modify the query as needed)
            var recentClaims = _context.Claims
                .OrderByDescending(c=> c.ClaimID) // Use ClaimID instead of Id
                .Take(5) // Get the 5 most recent claims
                .ToList();

            // Create a view model to hold the data for the home page
            var viewModel = new HomeViewModel
            {
                RecentClaims = recentClaims,
                WelcomeMessage = "Welcome to the Claim Submission System!"
            };

            return View(viewModel); // Pass the view model to the view
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
