using devnetcloud.Context;
using devnetcloud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace devnetcloud.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View(FakeDB.Users);
        }

        public IActionResult Details(int id)
        {

            User? u = FakeDB.Users.Find(x => x.Id == id);

            if (u != null)
            {
                return View(u);
            }

            return View("NotFound");

            
        }
    }
}
