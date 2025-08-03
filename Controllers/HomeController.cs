using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Phone_Project.Areas.Identity.Data;
using Mobile_Phone_Project.Models;

namespace Mobile_Phone_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;
        private readonly SignInManager<UserModel> _signInManager;

        public HomeController(
            ILogger<HomeController> logger,
            MyDbContext context,
            SignInManager<UserModel> signInManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var packages = await _context.RechargePackages
                .Where(p => p.IsActive)
                .ToListAsync();
            var feedbacks = await _context.Feedbacks
       .Where(f => f.IsApproved)
       .OrderByDescending(f => f.CreatedDate)
       .Take(6)
       .ToListAsync();

            var model = new HomeIndexViewModel
            {
                Packages = packages,
                Feedbacks = feedbacks
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

    }
}
