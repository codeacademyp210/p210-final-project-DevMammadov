using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using MainProjectFull.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainProjectFull.Controllers
{
    public class SettingsController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public SettingsController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var currentprofile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);

            if (currentprofile.Disabled)
            {
                return RedirectToAction("Ban", "Home", new { currentprofile.id });
            }

            // take blocked users
            List<Users> BlockedUsers = new List<Users>();
            List<Users> Allusers = db.Users.ToList();

            List<Blocklist> blcList = db.Blocklist.Where(b => b.blockerId == user.Id).ToList();

            foreach (var item in blcList)
            {
                BlockedUsers.Add(Allusers.FirstOrDefault(u => u.Id == item.blockedId));
            }

            var model = new SettingsVM()
            {
                Profile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id),
                User = user,
                Blocklist = BlockedUsers
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string name, string surname, string phone, string address, string lin, string be, string fb, string gb)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var user = db.Users.FirstOrDefault(u => u.Id == currentUser.Id);

                if (name != null && surname != null)
                {
                    user.Name = name;
                    user.Surname = surname;

                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else if(phone != null || address != null || lin != null || be != null || fb != null || gb != null)
                {
                    user.PhoneNumber = phone;
                    user.Address = address;
                    user.Linkedin = lin;
                    user.Behance = be;
                    user.Facebook = fb;
                    user.Github = gb;
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { error = "Oops! something wrong" });
                }
            }
            else
            {
                return Json(new { error = "You did not have permission for this operation" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Unblock(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var blockedUser = db.Blocklist.FirstOrDefault(p => p.blockerId == currentUser.Id && p.blockedId == id);

            if (blockedUser != null)
            {
                db.Blocklist.Remove(blockedUser);
                await db.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        //[HttpPost]
        //public async Task<IActionResult> Security(string oldpass, string newpass)
        //{
        //    var currentUser = await _userManager.GetUserAsync(User);
        //    var user = db.Users.FirstOrDefault(u => u.Id == currentUser.Id);

        //    PasswordHasher passwordHasher = new PasswordHasher();
        //    var result = passwordHasher.VerifyHashedPassword(user.PasswordHash, oldpass);
            
        //    return Json(new { response = user.PasswordHash });
        //}





    }
}