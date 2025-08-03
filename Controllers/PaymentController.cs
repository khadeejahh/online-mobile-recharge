using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Phone_Project.Models;
using Mobile_Phone_Project.Areas.Identity.Data;

namespace Mobile_Phone_Project.Controllers
{
    public class PaymentController : Controller
    {
        private readonly MyDbContext _context;

        public PaymentController(MyDbContext context)
        {
            _context = context;
        }

        // 👉 GET: Show payment form
        public async Task<IActionResult> Create(int packageId)
        {
            var package = await _context.RechargePackages.FindAsync(packageId);
            if (package == null) return NotFound();

            var model = new PaymentFormViewModel
            {
                PackageId = package.Id,
                PackageName = package.PackageName,
                Price = package.Price
            };

            return View(model);
        }

        // 👉 POST: Submit payment
        [HttpPost]
        public async Task<IActionResult> Create(PaymentFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var package = await _context.RechargePackages.FindAsync(model.PackageId);
            if (package == null) return NotFound();

            var transactionId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            var payment = new Payment
            {
                PackageId = package.Id,
                FullName = model.FullName,
                CNIC = model.CNIC,
                PhoneNumber = model.PhoneNumber,
                Amount = package.Price,
                PaymentStatus = "Paid",
                TransactionId = transactionId,
                CreatedAt = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            var receipt = new PaymentReceiptViewModel
            {
                FullName = payment.FullName,
                CNIC = payment.CNIC,
                PhoneNumber = payment.PhoneNumber,
                PackageName = package.PackageName,
                Price = package.Price,
                TransactionId = payment.TransactionId,
                CreatedAt = payment.CreatedAt
            };

            return View("Receipt", receipt);
        }

      
        [HttpGet]
        public async Task<IActionResult> AllPayments()
        {
            var payments = await _context.Payments
                .Include(p => p.Package) 
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View("AdminPayments", payments); 
        }

    }
}
