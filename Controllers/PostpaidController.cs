using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Phone_Project.Areas.Identity.Data;
using Mobile_Phone_Project.Models;

namespace Mobile_Phone_Project.Controllers
{

    public class PostpaidController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public PostpaidController(MyDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Show form to create postpaid bill
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostpaidBill bill)
        {
         

            bill.CreatedAt = DateTime.Now;
            bill.IsPaid = false;
            bill.UserId = _userManager.GetUserId(User); 

            _context.PostpaidBills.Add(bill);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MyBills()
        {
            var userId = _userManager.GetUserId(User);
            var bills = await _context.PostpaidBills
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return View(bills);
        }
        // Payment form
        // GET: /Postpaid/Pay/1
        [HttpGet]
        public async Task<IActionResult> Pay(int id)
        {
            var bill = await _context.PostpaidBills.FindAsync(id);
            if (bill == null || bill.IsPaid) return NotFound();

            return View(bill); // 👈 this will load the form view
        }

        // POST: /Postpaid/Pay
        [HttpPost]
        public async Task<IActionResult> Pay(PostpaidBill model)
        {
            var bill = await _context.PostpaidBills.FindAsync(model.Id);
            if (bill == null || bill.IsPaid) return NotFound();

            bill.IsPaid = true;
            bill.PaidAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction("MyBills");
        }

        public async Task<IActionResult> Receipt(int id)
        {
            var bill = await _context.PostpaidBills.FindAsync(id);
            return View(bill);
        }
        // ADMIN: View  Postpaid Bil
        public async Task<IActionResult> AllBills()
        {
            var allBills = await _context.PostpaidBills
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return View(allBills);
        }

    }
}

    
