using JWTCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JWTCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string Uname, string Upss)
        {
            var user = new User(
       1,
       "bruno.bernardes",
       "Bruno Bernardes",
       "bruno@gmail.com",
       "q1w2e3r4t5",
       ["developer"]);

            return View();
        }
    }
}
