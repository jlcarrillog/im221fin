using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Presentation.WebApp.Models;
using System.Diagnostics;
using System.Linq;

using Domain;
using Infrastructure;
using Application;
using System;

namespace Presentation.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ProductosDbContext _productosDbContext;
        private readonly PacientesDbContext _pacientesDbContext;
        private readonly CitasDbContext _citasDbContext;
        public HomeController(IConfiguration configuration)
        {
            _productosDbContext = new ProductosDbContext(configuration.GetConnectionString("DefaultConnection"));
            _pacientesDbContext = new PacientesDbContext(configuration.GetConnectionString("DefaultConnection"));
            _citasDbContext = new CitasDbContext(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            var productos = _productosDbContext.List();
            ViewBag.Chart1Labels = productos
                .GroupBy(x => x.Nombre)
                .Select(x => $"'{x.Key}'")
                .ToList();
            ViewBag.Chart1Data = productos
                .GroupBy(x => x.Nombre)
                .Select(x => $"{x.Count()}")
                .ToList();

            var citas = _citasDbContext.List();
            ViewBag.Chart2Labels = citas
                .OrderBy(x => x.Fecha)
                .GroupBy(x => new { month = x.Fecha.Month, year = x.Fecha.Year })
                .Select(x => $"'{x.Key.month}-{x.Key.year}'")
                .ToList();
            ViewBag.Chart2Data = citas
                .GroupBy(x => new { month = x.Fecha.Month, year = x.Fecha.Year })
                .Select(x => $"{x.Count()}")
                .ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
