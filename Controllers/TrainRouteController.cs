using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTicketReservationSystem.Models;

public class TrainRouteController : Controller
{
    private readonly MyNewContext _context;

    public TrainRouteController(MyNewContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> RootIndex()
    {
        var routes = await _context.TrainRoute.Include(r => r.Train).ToListAsync();
        return View(routes);
    }

    public IActionResult CreateRoot()
    {
        ViewBag.Trains = new SelectList(_context.Train, "TrainId", "TrainName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRoot(TrainRoute trainRoute)
    {
        if (ModelState.IsValid)
        {
            _context.Add(trainRoute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Trains = new SelectList(_context.Train, "TrainId", "TrainName");
        return View(trainRoute);
    }

    public async Task<IActionResult> EditRoot(int? id)
    {
        if (id == null) return NotFound();

        var trainRoute = await _context.TrainRoute.FindAsync(id);
        if (trainRoute == null) return NotFound();

        ViewBag.Trains = new SelectList(_context.Train, "TrainId", "TrainName", trainRoute.TrainId);
        return View(trainRoute);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRoot(int id, TrainRoute trainRoute)
    {
        if (id != trainRoute.TrainRouteId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(trainRoute);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainRouteExists(trainRoute.TrainRouteId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Trains = new SelectList(_context.Train, "TrainId", "TrainName", trainRoute.TrainId);
        return View(trainRoute);
    }

    public async Task<IActionResult> DeleteRoute(int? id)
    {
        if (id == null) return NotFound();

        var trainRoute = await _context.TrainRoute
            .Include(r => r.Train)
            .FirstOrDefaultAsync(m => m.TrainRouteId == id);

        if (trainRoute == null) return NotFound();

        return View(trainRoute);
    }

    [HttpPost, ActionName("DeleteRoute")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var trainRoute = await _context.TrainRoute.FindAsync(id);
        _context.TrainRoute.Remove(trainRoute);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TrainRouteExists(int id)
    {
        return _context.TrainRoute.Any(e => e.TrainRouteId == id);
    }
}
