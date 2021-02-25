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
    }
}
