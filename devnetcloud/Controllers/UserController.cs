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
            return View(_userService.GetAll());
        }

        public IActionResult Details(int id)
        {

            User? u = _userService.GetById(id);

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
            if (ModelState.IsValid)
            {

                if (_userService.UpdatePassword(form))
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Le Mot de passe actuel ne correspond pas.");

            }

            return View();
        }


        public IActionResult Delete(int id)
        {
            if (_userService.Delete(id))
            {
                return RedirectToAction("Index");
            }
            
            return View("NotFound");

        }
    }
}
