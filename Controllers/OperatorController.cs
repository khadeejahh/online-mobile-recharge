using Microsoft.AspNetCore.Mvc;
using Mobile_Phone_Project.Models;
using Mobile_Phone_Project.Areas.Identity.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mobile_Phone_Project.Controllers
{
    public class OperatorController : Controller
    {
        private readonly MyDbContext _context;

        public OperatorController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Operator
        public async Task<IActionResult> Index()
        {
            return View(await _context.Operators.ToListAsync());
        }

     

        // GET: Operator/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operator/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Operator op)
        {
            if (ModelState.IsValid)
            {
                _context.Operators.Add(op);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(op);
        }

        // GET: Operator/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var op = await _context.Operators.FindAsync(id);
            if (op == null) return NotFound();

            return View(op);
        }

        // POST: Operator/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Operator op)
        {
            if (id != op.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(op);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Operators.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(op);
        }

        // GET: Operator/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var op = await _context.Operators.FirstOrDefaultAsync(m => m.Id == id);
            if (op == null) return NotFound();

            return View(op);
        }

        // POST: Operator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var op = await _context.Operators.FindAsync(id);
            _context.Operators.Remove(op);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
