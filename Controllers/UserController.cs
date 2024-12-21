using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTicketReservationSystem.Models;

namespace TrainTicketReservationSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly MyNewContext _context;

        public UserController(MyNewContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _context.Users.ToListAsync();
            return View(users); 
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            ViewBag.UserRoles = Enum.GetValues(typeof(UserRole))
                .Cast<UserRole>()
                .Select(r => new SelectListItem
                {
                    Value = r.ToString(), // Store enum name as value
                    Text = r.ToString()   // Display enum name as text
                }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                



                return RedirectToAction(nameof(GetAllUser));
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAllUser));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUserConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAllUser));
        }
    }
}
