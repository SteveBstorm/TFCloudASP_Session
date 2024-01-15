using BLL.Services;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using BLL.Forms;
using devnetcloud.Models;
using devnetcloud.Tools;

namespace devnetcloud.Controllers
{
    public class UserController : Controller
    {

        private readonly UserService _userService;
        private readonly SessionManager _sessionManager;

        public UserController(UserService userService, SessionManager sessionManager)
        {
            _userService = userService;
            _sessionManager = sessionManager;
        }

        [IsConnected]
        public IActionResult Index()
        {
            return View(_userService.GetAll(_sessionManager.Token));
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginForm form)
        {
            if (!ModelState.IsValid) return View(form);

            string token = _userService.Login(form.Email, form.Password);

            _sessionManager.Token = token;

            return RedirectToAction("index");
        }
    }
}
