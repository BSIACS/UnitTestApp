using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTestApp.Models;

namespace UnitTestApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository repository;

        public UsersController(IRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult GetUsers()
        {
            return View(repository.GetAll());
        }

        public IActionResult GetUser(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            User user = repository.Get(id.Value);
            if (user == null)
                return NotFound();
            return View(user);
        }

        public IActionResult AddUser() => View();

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                repository.Create(user);
                return RedirectToAction("GetUsers");
            }
            return View(user);
        }
    }
}
