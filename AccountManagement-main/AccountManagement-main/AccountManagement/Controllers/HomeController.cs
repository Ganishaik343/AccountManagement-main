using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AccountManagement.Models;

namespace AccountManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Feedback()
        {
            return View();
        }         
    }
}
