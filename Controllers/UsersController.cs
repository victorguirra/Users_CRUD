using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersCRUD.Models;

namespace UsersCRUD.Controllers
{
    public class UsersController : Controller
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        public IActionResult UserDetails(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var user = _context.Users.FirstOrDefault(user => user.UserId == id);

            return View(user);
        }

        [HttpGet]
        public IActionResult UpdateUser(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var user = _context.Users.Find(id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUser(int id, User user)
        {
            if(id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(user);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(User);
        }

        [HttpGet]
        public IActionResult DeleteUser(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var user = _context.Users.FirstOrDefault(user => user.UserId == id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var user = _context.Users.FirstOrDefault(user => user.UserId == id);
            _context.Remove(user);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
