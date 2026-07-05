using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoSerwis.Mvc.UI.Models;

namespace AutoSerwis.Mvc.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendHistoryEmail(long id)
        {
            // Ustawiamy komunikat sukcesu w TempData
            TempData["EmailSuccessMessage"] = "Wiadomość e-mail z pełną historią napraw została pomyślnie wygenerowana i wysłana na adres klienta!";

            // Bezpieczny powrót na dokładnie tę samą stronę, z której kliknięto przycisk
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}