using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainProjectFull.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public HomeController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
         
        [Route("Admin")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexVM()
            {
                UserCount = db.Users.Count(),
                CvCount = db.Cv.Count(),
                PortCount = db.Portfolio.Count()
            };

            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user.Status == "Admin")
                {
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "AdminLogin");
                }
            }
            else
            {
                return RedirectToAction("Index","AdminLogin");
            }
        }
    }
}