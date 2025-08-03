using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Phone_Project.Areas.Identity.Data;
using Mobile_Phone_Project.Models;

namespace Mobile_Phone_Project.Controllers
{
    public class TopUpController : Controller
    {
        private readonly MyDbContext _context;

        public TopUpController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> admin()
        {
            var topup = _context.TopUpTransactions;
            return View(await topup.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(TopUpTransaction model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.TopUpTransactions.Add(model);
            _context.SaveChanges();

            ViewBag.Message = "Recharge successful!";
            return View(new TopUpTransaction()); // clear form
        }
    }
}
