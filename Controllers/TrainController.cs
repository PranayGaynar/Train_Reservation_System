using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTicketReservationSystem.Models;

namespace TrainTicketReservationSystem.Controllers
{
    public class TrainController : Controller
    {
        private readonly MyNewContext _context;

        public TrainController(MyNewContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrains()
        {
            var train = await _context.Train.ToListAsync();
            return View(train);
        }

        [HttpGet]
        public async Task<IActionResult> TrainGetById(int id)
        {
            var train = await _context.Train.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }
            return View(train);
        }

        [HttpGet]
        public IActionResult AddTrain()
        {

            ViewBag.TrainType = Enum.GetValues(typeof(TrainType))
                .Cast<TrainType>()
                .Select(r => new SelectListItem
                {
                    Value = r.ToString(), // Store enum name as value
                    Text = r.ToString()   // Display enum name as text
                }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTrain(Train train)
        {
            if (ModelState.IsValid)
            {
                await _context.Train.AddAsync(train);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAllTrains));
            }


            return View(train);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTrain(int id)
        {
            var train = await _context.Train.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }
            ViewBag.TrainType = Enum.GetValues(typeof(TrainType))
              .Cast<TrainType>()
              .Select(r => new SelectListItem
              {
                  Value = r.ToString(), // Store enum name as value
                  Text = r.ToString()   // Display enum name as text
              }).ToList();

            return View(train);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTrain(int id, Train train)
        {
            if (id != train.TrainId)
            {
                return BadRequest("Train ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return View(train);
            }

            _context.Train.Update(train);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAllTrains));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTrain(int id)
        {
            var train = await _context.Train.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }

            return View(train);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteTrain")]
        public async Task<IActionResult> DeleteTrainConfirmed(int id)
        {
            var train = await _context.Train.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }

            _context.Train.Remove(train);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAllTrains));
        }
    }
}
