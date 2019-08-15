using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using MainProjectFull.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainProjectFull.Controllers
{
    public class CvController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public CvController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        public async Task<IActionResult> ChangeCvPhoto(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            string uniqFileName = null;

            uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\CvImages", uniqFileName);

            using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
            {
                await file.CopyToAsync(fileSteam);
            }

            var profile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);
            var cv = db.Cv.FirstOrDefault(c => c.ProfileId == profile.id);
            cv.Photo = uniqFileName;
            await db.SaveChangesAsync();

            return Json(new { success = true });
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string color, string type)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var profile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
                var cv = db.Cv.FirstOrDefault(c => c.ProfileId == profile.id);

                if (color != null)
                {
                    cv.Color = color;
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else if (type != null)
                {
                    cv.Type = type;
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
                return Json(new { error = "You dont have permission for this operation!" });
            }
        }


        [Route("Cv/GetCv/{id?}")]
     
        public async Task<IActionResult> GetCv(int? id)
        {
            Profile currentProfile = new Profile();
            Users currentuser = new Users();

            if (_signInManager.IsSignedIn(User))
            {
                currentuser = await _userManager.GetUserAsync(User);
                currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentuser.Id);
            }

            // if user is not self than add view
            //if (port.ProfileId != currentProfile.id)
            //{
            //    port.View = port.View + 1;
            //}

            // await db.SaveChangesAsync();

            var findedCv = db.Cv.FirstOrDefault(c => c.id == id);
            var findedProfile = db.Profile.FirstOrDefault(p => p.id == findedCv.ProfileId);
            var findedUser = db.Users.FirstOrDefault(u => u.Id == findedProfile.UsersId);

            var model = new CvBoxVM() {
                Cv = findedCv,
                Profile = findedProfile,
                User = findedUser,
                SkillHeaders = db.SkillsHeader.Where(sh => sh.ProfileId == findedProfile.id).ToList(),
                SkillsHeaders = db.SkillsHeaderPosition.ToList(),
                Skills = db.Position.ToList(),
                Universities = db.University.Where(u => u.UserId == findedUser.Id).ToList(),
                Company = db.Company.ToList(),
                Experience = db.Experience.ToList(),
                SocialActs = db.SocialAct.Where(sa => sa.UsersId == findedUser.Id).ToList(),
                Languages = db.Languages.ToList(),
                UsersLanguages = db.UsersLanguages.Where(ul => ul.UsersId == findedUser.Id).ToList()
            };

            return PartialView("_CvBox", model);

        }










    }
}