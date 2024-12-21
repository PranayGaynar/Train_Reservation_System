using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainTicketReservationSystem.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

public class PaymentController : Controller
{
    private readonly MyNewContext _context;

    public PaymentController(MyNewContext context)
    {
        _context = context;
    }

    // GET: Payment
    public async Task<IActionResult> PaymentIndex()
    {
        var payments = await _context.Payment.Include(p => p.Booking).ToListAsync();
        return View(payments);
    }

    // GET: Payment/Details/5
    public async Task<IActionResult> PaymentDetails(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payment
            .Include(p => p.Booking)
            .FirstOrDefaultAsync(m => m.PaymentId == id);
        if (payment == null)
        {
            return NotFound();
        }

        return View(payment);
    }

    // GET: Payment/Create
    public IActionResult CreatePayment()
    {
        ViewBag.Bookings = new SelectList(_context.Booking, "BookingId", "PNR");
        return View();
    }

    // POST: Payment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePayment(Payment payment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Bookings = new SelectList(_context.Booking, "BookingId", "PNR", payment.BookingId);
        return View(payment);
    }

    // GET: Payment/Edit/5
    public async Task<IActionResult> EditPayment(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payment.FindAsync(id);
        if (payment == null)
        {
            return NotFound();
        }
        ViewBag.Bookings = new SelectList(_context.Booking, "BookingId", "PNR", payment.BookingId);
        return View(payment);
    }

    // POST: Payment/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPayment(int id, Payment payment)
    {
        if (id != payment.PaymentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(payment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(payment.PaymentId))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Bookings = new SelectList(_context.Booking, "BookingId", "PNR", payment.BookingId);
        return View(payment);
    }

    // GET: Payment/Delete/5
    public async Task<IActionResult> DeletePayment(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payment
            .Include(p => p.Booking)
            .FirstOrDefaultAsync(m => m.PaymentId == id);
        if (payment == null)
        {
            return NotFound();
        }

        return View(payment);
    }

    // POST: Payment/Delete/5
    [HttpPost, ActionName("DeletePayment")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var payment = await _context.Payment.FindAsync(id);
        _context.Payment.Remove(payment);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PaymentExists(int id)
    {
        return _context.Payment.Any(e => e.PaymentId == id);
    }
}
