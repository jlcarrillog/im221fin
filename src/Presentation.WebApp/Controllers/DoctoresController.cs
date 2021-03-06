using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Domain;
using Infrastructure;
using Application;

namespace Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctoresController : Controller
    {

        private readonly DoctoresDbContext _DoctoresDbContext;
        public DoctoresController(IConfiguration configuration)
        {
            _DoctoresDbContext = new DoctoresDbContext(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            // ToDo
            return View("Error");
        }

        public IActionResult Details(Guid id)
        {
            // ToDo
            return null;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor data, IFormFile file)
        {
            // ToDo
            return View("Error");
        }

        public IActionResult Edit(Guid id)
        {
            // ToDo
            return View("Error");
        }
        [HttpPost]
        public IActionResult Edit(Doctor data, IFormFile file)
        {
            // ToDo
            return View("Error");
        }

        public IActionResult Delete(Guid id)
        {
            // ToDo
            return View("Error");
        }
    }
}
