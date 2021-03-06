using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

using Domain;
using Infrastructure;
using Application;

namespace Presentation.WebApp.Controllers
{
    [Authorize]
    public class CitasController : Controller
    {
        private readonly CitasDbContext _citasDbContext;
        private readonly PacientesDbContext _pacientesDbContext;
        private readonly SmtpClientEmailService _email;
        public CitasController(IConfiguration configuration)
        {
            _citasDbContext = new CitasDbContext(configuration.GetConnectionString("DefaultConnection"));
            _pacientesDbContext = new PacientesDbContext(configuration.GetConnectionString("DefaultConnection"));
            var config = configuration.GetSection("Smtp");
            _email = new SmtpClientEmailService(config["Displayname"], config["Address"], config["Host"], int.Parse(config["Port"]), config["Username"], config["Password"]);
        }

        public IActionResult Index()
        {
            var data = _citasDbContext.List();
            return View(data);
        }

        public IActionResult Details(Guid id)
        {
            var data = _citasDbContext.Details(id);
            return PartialView(data);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create(Guid id)
        {
            var data = new Cita
            {
                Fecha = DateTime.Now,
                Paciente = _pacientesDbContext.Details(id)
            };
            return View(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Cita data)
        {
            _citasDbContext.Create(data);
            await _email.SendEmailAsync(data.Paciente.Correo, "Asunto", $"<h4>Hola {data.Paciente.Nombre}</h4>", true);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var data = _citasDbContext.Details(id);
            data.Paciente = _pacientesDbContext.Details(data.Paciente.Id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Cita data)
        {
            _citasDbContext.Edit(data);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            _citasDbContext.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
