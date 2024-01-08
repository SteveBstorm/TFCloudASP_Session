using BLL.Services;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using BLL.Forms;

namespace devnetcloud.Controllers
{
    public class UserController : Controller
    {

        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


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
                _userService.Create(form);
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
