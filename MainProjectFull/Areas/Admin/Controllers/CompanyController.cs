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
    public class CompanyController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public CompanyController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("Admin/Company/")]
        public async Task<IActionResult> Index()
        {
            var model = new CompanyVM()
            {
                Company = null,
                Companies = db.Company.ToList(),
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
        public async Task<IActionResult> Create(Company Company, IFormFile file)
        {
            string uniqFileName = null;

            uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\CompanyLogo", uniqFileName);

            using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
            {
                await file.CopyToAsync(fileSteam);
            }

            await db.Company.AddAsync(new Company() { Name = Company.Name, Icon = uniqFileName });
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Route("Admin/Company/Edit/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var model = new CompanyVM()
                {
                    Company = db.Company.FirstOrDefault(c => c.id == id),
                    Companies = db.Company.ToList(),
                    Action = "Edit",
                    Button = "Redaktə et"
                };
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user.Status == "Admin")
                    {
                        return View("Index",model);
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
        public async Task<IActionResult> Edit(Company Company, IFormFile file)
        {
            if(file == null)
            {
                var cmp =  db.Company.FirstOrDefault(c => c.id == Company.id);
                cmp.Name = Company.Name;
                await db.SaveChangesAsync();
            }
            else
            {
                string uniqFileName = null;
                uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\CompanyLogo", uniqFileName);

                using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
                {
                    await file.CopyToAsync(fileSteam);
                }

                var cmp = db.Company.FirstOrDefault(c => c.id == Company.id);
                cmp.Name = Company.Name;
                cmp.Icon = uniqFileName;
                await db.SaveChangesAsync();

            }

            return RedirectToAction("Index");
        }










        }
}