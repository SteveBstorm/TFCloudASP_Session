using devnetcloud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace devnetcloud.Controllers
{
    public class HomeController : Controller
    {
    

        public IActionResult Index(string id)
        {

            Console.WriteLine(id);
            return View();
        }

        public IActionResult Privacy(int rantanplan)
        {

            Console.WriteLine(rantanplan);
            return View();
        }
    }
}