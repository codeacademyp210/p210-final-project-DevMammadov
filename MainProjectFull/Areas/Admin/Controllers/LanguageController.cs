using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.Areas.Admin.ViewModel;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainProjectFull.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguageController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public LanguageController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("/Admin/Language/")]
        public async Task<IActionResult> Index()
        {
            var model = new LangVM()
            {
                Language = null,
                Languages = db.Languages.ToList(),
                Action = "Create",
                Button = "Əlavə et"
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
                return RedirectToAction("Index", "AdminLogin");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Languages Language, IFormFile file)
        {
            string uniqFileName = null;

            uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\Flags", uniqFileName);

            using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
            {
                await file.CopyToAsync(fileSteam);
            }

            await db.Languages.AddAsync(new Languages() { Name = Language.Name, Icon = uniqFileName });
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Route("/Admin/Language/Edit/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var model = new LangVM()
                {
                    Language = db.Languages.FirstOrDefault(c => c.id == id),
                    Languages = db.Languages.ToList(),
                    Action = "Edit",
                    Button = "Redaktə et"
                };
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user.Status == "Admin")
                    {
                        return View("Index", model);
                    }
                    else
                    {
                        return RedirectToAction("Index", "AdminLogin");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "AdminLogin");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Languages Language, IFormFile file)
        {
            if (file == null)
            {
                var lng = db.Languages.FirstOrDefault(c => c.id == Language.id);
                lng.Name = Language.Name;
                await db.SaveChangesAsync();
            }
            else
            {
                string uniqFileName = null;
                uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\Flags", uniqFileName);

                using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
                {
                    await file.CopyToAsync(fileSteam);
                }

                var lng = db.Languages.FirstOrDefault(c => c.id == Language.id);
                lng.Name = Language.Name;
                lng.Icon = uniqFileName;
                await db.SaveChangesAsync();

            }

            return RedirectToAction("Index");
        }






    }
}