using ARMApocalypseSASAPI.Interfaces;
using ARMApocalypseSASAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ARMApocalypseSASAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICoreService _coreService;

        public HomeController(ILogger<HomeController> logger, ICoreService coreService)
        {
            _logger = logger;
            _coreService = coreService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Survivors()
        {
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