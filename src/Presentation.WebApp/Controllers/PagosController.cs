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
    public class PagosController : Controller
    {

        private readonly PagosDbContext _PagosDbContext;
        public PagosController(IConfiguration configuration)
        {
            _PagosDbContext = new PagosDbContext(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            // ToDo
            return View("Error");
        }

        public IActionResult Details(Guid id)
        {
            // ToDo
            return View("Error");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Pago data, IFormFile file)
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
        public IActionResult Edit(Pago data, IFormFile file)
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