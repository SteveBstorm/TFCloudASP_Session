using devnetcloud.Context;
using devnetcloud.Models;
using devnetcloud.Models.Forms;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RegisterForm form)
        {
            if (ModelState.IsValid) 
            {
                User u = new User()
                {
                    Id = FakeDB.Users.Max(x => x.Id) + 1,
                    Email = form.Email,
                    Password = form.Password,
                    Pseudo = form.Pseudo
                };
                FakeDB.Users.Add(u);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int id)
        {
            UpdatePasswordForm form = new UpdatePasswordForm()
            {
                Id = id,
                ConfirmationPassword = "",
                Password = ""
            };
            return View(form);
        }

        [HttpPost]
        public IActionResult Update(UpdatePasswordForm form)
        {

            Console.WriteLine(form.Id);

            if (ModelState.IsValid)
            {
                User? u = FakeDB.Users.Find(x => x.Id == form.Id);
                if (u != null)
                {
                    if (u.Password == form.OldPassword)
                    {
                        u.Password = form.Password;
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("","Le Mot de passe actuel ne correspond pas.");
                    
                }
            }

            return View();
        }


        public IActionResult Delete(int id)
        {
            User? u = FakeDB.Users.Find(x => x.Id == id);

            if (u is not null)
            {
                FakeDB.Users.Remove(u);
            }

            return RedirectToAction("Index");
        }
    }
}
