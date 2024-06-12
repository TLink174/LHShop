using LHShop.Data;
using LHShop.Models;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LHShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly Lhshop2024Context _context;


        public HomeController(ILogger<HomeController> logger, Lhshop2024Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            return View();

        }
        [Route("/error/404")]
        public IActionResult NotFound()
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
