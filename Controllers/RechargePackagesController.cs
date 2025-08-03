using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mobile_Phone_Project.Areas.Identity.Data;
using Mobile_Phone_Project.Models;


public class RechargePackagesController : Controller
{
    private readonly MyDbContext _context;
 


    public RechargePackagesController(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var packages = _context.RechargePackages.Include(r => r.Operator);
        return View(await packages.ToListAsync());
    }

    


    public IActionResult Create()
    {
        ViewBag.OperatorId = new SelectList(_context.Operators, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RechargePackage rechargePackage)
    {
        if (ModelState.IsValid)
        {
            _context.Add(rechargePackage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.OperatorId = new SelectList(_context.Operators, "Id", "Name", rechargePackage.OperatorId);
        return View(rechargePackage);
    }


    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var package = await _context.RechargePackages.FindAsync(id);
        if (package == null) return NotFound();
        ViewBag.OperatorId = new SelectList(_context.Operators, "Id", "Name", package.OperatorId);
        return View(package);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RechargePackage rechargePackage)
    {
        if (id != rechargePackage.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(rechargePackage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.OperatorId = new SelectList(_context.Operators, "Id", "Name", rechargePackage.OperatorId);
        return View(rechargePackage);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var package = await _context.RechargePackages.Include(r => r.Operator).FirstOrDefaultAsync(m => m.Id == id);
        if (package == null) return NotFound();
        return View(package);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var package = await _context.RechargePackages.FindAsync(id);
        if (package != null)
        {
            _context.RechargePackages.Remove(package);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var package = await _context.RechargePackages.Include(r => r.Operator).FirstOrDefaultAsync(m => m.Id == id);
        if (package == null) return NotFound();
        return View(package);
    }
}
