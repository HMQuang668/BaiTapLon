using System.Diagnostics;
using BaiTapLon.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapLon.Controllers
{
    public class HomeController : Controller
    {




        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

    }
}
