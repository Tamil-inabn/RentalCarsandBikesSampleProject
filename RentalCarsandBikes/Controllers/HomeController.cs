using DAL.Interface;
using Microsoft.AspNetCore.Mvc;
using RentalCarsandBikes.Models;
using System.Diagnostics;

namespace RentalCarsandBikes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly DAL_Interface _dAL_Interface;
        public HomeController(ILogger<HomeController> logger, DAL_Interface dAL_Interface)
        {
            _logger = logger;
            _dAL_Interface = dAL_Interface;
        }

        public IActionResult Index()
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