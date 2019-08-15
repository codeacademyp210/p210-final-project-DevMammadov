using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using MainProjectFull.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainProjectFull.Controllers
{
    public class RegisterController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public RegisterController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register(Users RegisterUser, string ProfileType,string rememberme)
        {
            bool remember;
            if(rememberme == "true")
            {
                remember = true;
            }
            else
            {
                remember = false;
            }

            if (ModelState.IsValid)
            {
                RegisterUser.UserName = RegisterUser.Email;

                // Add default contents
                RegisterUser.About = "Özünüz haqqda bizə danışın";
                RegisterUser.Profession = "İxtisasınız";
                RegisterUser.Status = "User";
                RegisterUser.RegisterDate = DateTime.Now;

                var result = await _userManager.CreateAsync(RegisterUser, RegisterUser.PasswordHash);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(RegisterUser, remember);

                    // new profile for new user
                    Profile newProfile = new Profile()
                    {
                        Photo = "profile.png",
                        Cover = "cover.jpg",
                        BackColor = "#0B6285",
                        TextColor = "rgb(0, 18, 26)",
                        View = 0,
                        MessageButton = true,
                        UsersId = RegisterUser.Id,
                        Type = ProfileType
                    };

                    db.Profile.Add(newProfile);
                    db.SaveChanges();

                    // New cv for new user
                    Cv newCv = new Cv()
                    {
                        Photo = "profile.png",
                        ProfileId = newProfile.id,
                        Color = "blue",
                        Type = "simple"
                    };

                    db.Cv.Add(newCv);
                    db.SaveChanges();

                    return RedirectToAction("About", "Profile", new { newProfile.id });
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View("Index");
        }


        public async Task<IActionResult> Login(LoginClass LoginUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(LoginUser.Email, LoginUser.Password, LoginUser.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = db.Users.FirstOrDefault(u => u.Email.Equals(LoginUser.Email));
                    var profile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);

                    int profileId = profile.id;
                    return RedirectToAction("About", "Profile", new { profile.id });
                }

                ModelState.AddModelError("", "Invalid login or password");
            }

            return View("Index");

        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }




        public async Task<IActionResult> SendUser()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var profile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);

                return Json(new { name = currentUser.Name, surname = currentUser.Surname, photo = profile.Photo,currentUser.Id });
            }
            else
            {
                return Json(new { error = "not signed in" });

            }

        }











    }
}