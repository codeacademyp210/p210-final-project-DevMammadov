using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainProjectFull.Controllers
{
    public class AboutController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AboutController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string position, string about)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var getUser = db.Users.FirstOrDefault(u => u.Id == currentUser.Id);
            var userProfile = db.Profile.FirstOrDefault(p => p.UsersId == getUser.Id);
            
            if (currentUser != null)
            {
                if (position != null)
                {
                    getUser.Profession = position;
                    await db.SaveChangesAsync();

                    return Json(new { success = true });
                }
                else if (about != null)
                {
                    getUser.About = about;
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
            }
            return Json(new { error = "You dont have permission for this operation" });
        }


        [HttpPost]
        public async Task<IActionResult> AddActivity(string companyName, string positionName, string about)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var newAct = new SocialAct() { Company = companyName, Position = positionName, About = about, UsersId = currentUser.Id };
                db.SocialAct.Add(newAct);
                await db.SaveChangesAsync();
                return Json(new { id = newAct.id });
            }
            else
            {
                return Json(new { error = "You dont have permission for this operation" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> ActivityEdit(string companyName, string positionName, string id, string about)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && id != null)
            {
                int actId = Convert.ToInt32(id);
                var currentAct = db.SocialAct.FirstOrDefault(act => act.id == actId);

                if (companyName != null)
                {
                    currentAct.Company = companyName;
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else if (positionName != null)
                {
                    currentAct.Position = positionName;
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else if (about != null)
                {
                    currentAct.About = about;
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { error = "Oops! Something wrong !!" });
                }
            }
            else
            {
                return Json(new { error = "You dont have permission for this operation" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveActivity(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && id != null)
            {

                int actId = Convert.ToInt32(id);
                var actToRemove = db.SocialAct.FirstOrDefault(act => act.id == actId && act.UsersId == currentUser.Id);
                db.SocialAct.Remove(actToRemove);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { error = "You dont have permission for this operation" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddUniversity(string uName, string pName, string startD, string endD)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var startDate = Convert.ToDateTime(startD);
            var endDate = Convert.ToDateTime(endD);

            if (currentUser != null)
            {
                var university = new University()
                {
                    Name = uName,
                    Profession = pName,
                    StartDate = startDate,
                    EndDate = endDate,
                    UserId = currentUser.Id
                };

                db.University.Add(university);
                await db.SaveChangesAsync();

                return Json(new { id = university.id });
            }
            else
            {
                return Json(new { error = "You dont have permission for this operation" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditUniversity(string profession, string id, string about)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && id != null)
            {
                int uId = Convert.ToInt32(id);
                var univer = db.University.FirstOrDefault(u => u.id == uId);

                if (profession != null)
                {
                    univer.Profession = profession;
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else if (about != null)
                {
                    univer.Name = about;
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { error = "Oops! something wrong!" });
                }
            }
            else
            {
                return Json(new { error = "You dont have permission for this operation" });
            }
        }



        [HttpPost]
        public async Task<IActionResult> RemoveUniversity(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && id != null)
            {
                int univerId = Convert.ToInt32(id);
                var univer = db.University.FirstOrDefault(u => u.id == univerId && u.UserId == currentUser.Id);
                db.University.Remove(univer);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { error = "You dont have permission for this operation" });
            }
        }





    }
}