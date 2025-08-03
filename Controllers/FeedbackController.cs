using Microsoft.AspNetCore.Mvc;
using Mobile_Phone_Project.Areas.Identity.Data;
using Mobile_Phone_Project.Models;
using Microsoft.EntityFrameworkCore;


namespace Mobile_Phone_Project.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly MyDbContext _context;

        public FeedbackController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allFeedback = await _context.Feedbacks.OrderByDescending(f => f.CreatedDate).ToListAsync();
            return View(allFeedback);
        }

        // ADMIN - Approve feedback
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                feedback.IsApproved = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Approved()
        {
            var approvedFeedbacks = await _context.Feedbacks
                .Where(f => f.IsApproved)
                .OrderByDescending(f => f.CreatedDate)
                .ToListAsync();

            return View(approvedFeedbacks);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Approved");
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                var feedback = new FeedbackViewModel
                {
                    Name = model.Name,
                    Email = model.Email,
                    Rating = model.Rating,
                    Comment = model.Comment,
                    CreatedDate = DateTime.Now,
                    IsApproved = false 
                };

                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("create"); 
            }
            return View(model);
        }

        //public IActionResult ThankYou()
        //{
        //    return View();
        //}

        public IActionResult SubmitFeedback()
        {
            // feedback save logic
            return RedirectToAction("Index", "Home"); // or any controller/page you want
        }


    }

}
