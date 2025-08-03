using Microsoft.AspNetCore.Mvc;
using Mobile_Phone_Project.Models;
using Mobile_Phone_Project.Areas.Identity.Data;

namespace Mobile_Phone_Project.Controllers
{
    public class RechargeTransactionController : Controller
    {
        private readonly MyDbContext _context;

        public RechargeTransactionController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopUpTransaction model)
        {
            if (!User.Identity.IsAuthenticated && model.TransactionType == "PostPaid")
            {
                ModelState.AddModelError("", "You must be logged in to perform a PostPaid transaction.");
                return View(model);
            }

            if (!ModelState.IsValid)
                return View(model);

            // Save the transaction
            model.TransactionDate = DateTime.Now;
            _context.TopUpTransactions.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Receipt", new { id = model.Id });
        }

        public async Task<IActionResult> Receipt(int id)
        {
            var data = await _context.TopUpTransactions.FindAsync(id);
            if (data == null) return NotFound();

            return View(data);
        }
    }
}
