using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.Areas.Admin.ViewModel;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainProjectFull.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AdminLoginController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginVM LoginUser)
        {
            if (ModelState.IsValid)
            {
                var existUser = db.Users.FirstOrDefault(u => u.Email.ToLower().Contains(LoginUser.Email.ToLower()));
                if (existUser != null && existUser.Status == "Admin")
                {
                    var result = await _signInManager.PasswordSignInAsync(LoginUser.Email, LoginUser.Password, LoginUser.Remember, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Invalid login or password");
                }
                else
                {
                    ViewBag.error = "Yout dont have permission for this";
                    return View("Index");
                }
            }

            return View("Index");
        }


        public async Task<IActionResult> Logout(LoginVM LoginUser)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }



    }
}