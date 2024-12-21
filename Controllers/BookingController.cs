using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrainTicketReservationSystem.Models;

public class BookingController : Controller
{
    private readonly MyNewContext _context;

    public BookingController(MyNewContext context)
    {
        _context = context;
    }

    // GET: Booking
    public async Task<IActionResult> BookingIndex()
    {
        var bookings = await _context.Booking
            .Include(b => b.User)
            .Include(b => b.TrainRoute).ToListAsync();

        return View(bookings);
    }

    // GET: Booking/Create
    public IActionResult CreateBooking()
    {
        ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName");
        ViewBag.TrainRoutes = new SelectList(_context.TrainRoute, "TrainRouteId", "RouteDetails"); // Replace RouteDetails with a suitable display property.
        return View();
    }

    // POST: Booking/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateBooking(Booking booking)
    {
        if (ModelState.IsValid)
        {
            booking.BookingDate = DateTime.Now;
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName", booking.UserId);
        ViewBag.TrainRoutes = new SelectList(_context.TrainRoute, "TrainRouteId", "RouteDetails", booking.TrainRouteId);
        return View(booking);
    }

    // GET: Booking/Edit/5
    public async Task<IActionResult> EditBooking(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _context.Booking.FindAsync(id);
        if (booking == null) return NotFound();

        ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName", booking.UserId);
        ViewBag.TrainRoutes = new SelectList(_context.TrainRoute, "TrainRouteId", "RouteDetails", booking.TrainRouteId);
        return View(booking);
    }

    // POST: Booking/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditBooking(int id, Booking booking)
    {
        if (id != booking.BookingId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.BookingId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName", booking.UserId);
        ViewBag.TrainRoutes = new SelectList(_context.TrainRoute, "TrainRouteId", "RouteDetails", booking.TrainRouteId);
        return View(booking);
    }

    // GET: Booking/Delete/5
    public async Task<IActionResult> DeleteBooking(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _context.Booking
            .Include(b => b.User)
            .Include(b => b.TrainRoute)
            .FirstOrDefaultAsync(m => m.BookingId == id);

        if (booking == null) return NotFound();

        return View(booking);
    }

    // POST: Booking/Delete/5
    [HttpPost, ActionName("DeleteBooking")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var booking = await _context.Booking.FindAsync(id);
        _context.Booking.Remove(booking);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookingExists(int id)
    {
        return _context.Booking.Any(e => e.BookingId == id);
    }
}
