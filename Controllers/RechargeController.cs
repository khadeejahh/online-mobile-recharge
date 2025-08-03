using Microsoft.AspNetCore.Mvc;
using Mobile_Phone_Project.Areas.Identity.Data;
using Mobile_Phone_Project.Models;

namespace Mobile_Phone_Project.Controllers
{
    public class RechargeController : Controller
    {
        private readonly MyDbContext _context;

        public RechargeController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
public IActionResult Index()
{
    ViewBag.RechargeOptions = _context.RechargeOptions
        .Where(r => r.Type.ToLower() == "regular") // sirf regular options
        .ToList();

    return View();
}
        [HttpPost]
        public IActionResult SimpleRecharge([FromBody] RechargeRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.MobileNumber) || request.MobileNumber.Length != 10)
                return BadRequest("Invalid mobile number");

            if (request.Amount < 10 || request.Amount > 10000)
                return BadRequest("Invalid recharge amount");

            var rechargeOption = _context.RechargeOptions
                .FirstOrDefault(r => r.Amount == request.Amount && r.Type.ToLower() == "regular");

            if (rechargeOption == null)
                return BadRequest("Recharge option not found");

            var transaction = new RechargeTransaction
            {
                MobileNumber = request.MobileNumber,
                RechargeOptionId = rechargeOption.Id,
                PaymentStatus = "Success",
                TransactionDate = DateTime.Now
            };

            _context.RechargeTransactions.Add(transaction);
            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Recharge successful",
                amount = request.Amount,
                mobile = request.MobileNumber,
                package = rechargeOption.Description ?? rechargeOption.Type
            });
        }

    }
    public class RechargeRequestDto
    {
        public string MobileNumber { get; set; }
        public decimal Amount { get; set; }
    }

}

