using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using MainProjectFull.ViewModel;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainProjectFull.Controllers
{
    public class ProfileController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public ProfileController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [Route("Profile/Experience/{id?}")]
        public async Task<IActionResult> Experience(int? id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            Profile currentProfile = null;
            int currentId = 0;
            int profileId = 0;

            if (_signInManager.IsSignedIn(User))
            {
                currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
                currentId = currentProfile.id;
            }

            if (id == null)
            {
                if (_signInManager.IsSignedIn(User))
                {
                    profileId = currentId;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                profileId = (int)id;
            }

            var profile = db.Profile.FirstOrDefault(p => p.id == profileId);
            var user = db.Users.FirstOrDefault(u => u.Id == profile.UsersId);

            // if blocked
            if (_signInManager.IsSignedIn(User))
            {
                var block = db.Blocklist.FirstOrDefault(b => b.blockedId == user.Id && b.blockerId == currentUser.Id || b.blockerId == user.Id && b.blockedId == currentUser.Id);
                
                if(block != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            //if banned

            if (profile.Disabled && profile.UsersId != currentUser.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (currentProfile != null && currentProfile.Disabled)
            {
                return RedirectToAction("Ban", "Home", new { profile.id });
            }

            var experiences = db.Experience.Where(exp => exp.ProfileId == profile.id).ToList();
            experiences.Reverse();
            // Get profile Cover information
            bool isfolowing = false;
            if (currentUser != null)
            {
                var folow = db.Follow.FirstOrDefault(f => f.folowerId == currentProfile.id && f.folowingId == profile.id);
                if (folow != null) { isfolowing = true; }
            }

            var coverClass = new CoverClass()
            {
                Profile = profile,
                User = db.Users.FirstOrDefault(u => u.Id == profile.UsersId),
                folowings = db.Follow.Where(f => f.folowerId == profile.id).Count(),
                folowers = db.Follow.Where(f => f.folowingId == profile.id).Count(),
                views = profile.View,
                isFolowing = isfolowing
            };

            // preparing experience model
            var model = new ExperienceVM()
            {
                Experiences = experiences,
                Cover = coverClass,
                Profile = profile,
                Companies = db.Company.ToList()
            };

            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
                return PartialView("_Experience", model);

            else
                return View(model);
        }



        [Route("Profile/Skills/{id?}")]
        public async Task<IActionResult> Skills(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            Profile currentProfile = null;
            int currentId = 0;
            int profileId = 0;

            if (_signInManager.IsSignedIn(User))
            {
                currentProfile = db.Profile.First(p => p.UsersId == user.Id);
                currentId = currentProfile.id;
            }

            if (id == null)
            {
                if (_signInManager.IsSignedIn(User))
                {
                    profileId = currentId;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                profileId = (int)id;
            }

            var profile = db.Profile.FirstOrDefault(p => p.id == profileId);
            var thisuser = db.Users.FirstOrDefault(u => u.Id == profile.UsersId);

            if (_signInManager.IsSignedIn(User))
            {
                var block = db.Blocklist.FirstOrDefault(b => b.blockedId == user.Id && b.blockerId == thisuser.Id || b.blockerId == user.Id && b.blockedId == thisuser.Id);

                if (block != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            if (profile.Disabled && profile.UsersId != user.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            if (currentProfile != null && currentProfile.Disabled)
            {
                return RedirectToAction("Ban", "Home", new { profile.id });
            }

            var skillHeaders = db.SkillsHeader.Where(sk => sk.ProfileId == profile.id).ToList();
            skillHeaders.Reverse();

            // Get profile Cover information
            bool isfolowing = false;
            if (user != null)
            {
                var folow = db.Follow.FirstOrDefault(f => f.folowerId == currentProfile.id && f.folowingId == profile.id);
                if (folow != null) { isfolowing = true; }
            }

            var coverClass = new CoverClass()
            {
                Profile = profile,
                User = db.Users.FirstOrDefault(u => u.Id == profile.UsersId),
                folowings = db.Follow.Where(f => f.folowerId == profile.id).Count(),
                folowers = db.Follow.Where(f => f.folowingId == profile.id).Count(),
                views = profile.View,
                isFolowing = isfolowing
            };

            // preparing skills model
            var model = new SkillsVM()
            {
                Position = db.Position.ToList(),
                SkillsHeader = skillHeaders,
                SkillsHeaderPosition = db.SkillsHeaderPosition.ToList(),
                CoverClass = coverClass,
                UserLanguages = db.UsersLanguages.Where(ul => ul.UsersId == profile.UsersId).ToList(),
                Languages = db.Languages.ToList()
            };


            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
                return PartialView("_Skills", model);
            else
                return View(model);
        }





        [Route("Profile/About/{id?}")]
        public async Task<IActionResult> About(int? id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            Profile currentProfile = null;
            int currentId = 0;
            int profileId = 0;

            if (_signInManager.IsSignedIn(User))
            {
                currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
                currentId = currentProfile.id;
            }

            if (id == null)
            {
                if (_signInManager.IsSignedIn(User))
                {
                    profileId = currentId;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                profileId = (int)id;
            }

            var profile = db.Profile.FirstOrDefault(p => p.id == profileId);
            var user = db.Users.FirstOrDefault(u => u.Id == profile.UsersId);

            if (_signInManager.IsSignedIn(User))
            {
                var block = db.Blocklist.FirstOrDefault(b => b.blockedId == user.Id && b.blockerId == currentUser.Id || b.blockerId == user.Id && b.blockedId == currentUser.Id);

                if (block != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            if (currentUser == null)
            {
                profile.View = profile.View + 1;
                await db.SaveChangesAsync();
            }
            else if (_signInManager.IsSignedIn(User) && profile.UsersId != currentUser.Id)
            {
                profile.View = profile.View + 1;
                await db.SaveChangesAsync();
            }


            if (profile.Disabled && profile.UsersId != currentUser.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            if (currentProfile != null && currentProfile.Disabled)
            {
                return RedirectToAction("Ban", "Home", new { profile.id });
            }

            var usersActs = db.SocialAct.Where(s => s.UsersId == profile.UsersId).ToList();
            usersActs.Reverse();
            // Get profile Cover information
            bool isfolowing = false;
            if (currentUser != null)
            {
                var folow = db.Follow.FirstOrDefault(f => f.folowerId == currentProfile.id && f.folowingId == profile.id);
                if (folow != null) { isfolowing = true; }
            }

            var coverClass = new CoverClass()
            {
                Profile = profile,
                User = db.Users.FirstOrDefault(u => u.Id == profile.UsersId),
                folowings = db.Follow.Where(f => f.folowerId == profile.id).Count(),
                folowers = db.Follow.Where(f => f.folowingId == profile.id).Count(),
                views = profile.View,
                isFolowing = isfolowing
            };

            // preparing model
            var model = new AboutVM()
            {
                CoverClass = coverClass,
                SosialActs = usersActs,
                Universities = db.University.Where(u => u.UserId == profile.UsersId).ToList()
            };

            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
                return PartialView("_About", model);
            else
                return View(model);
        }
        

        [Route("Profile/CV/{id?}")]
        public async Task<IActionResult> CV(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            Profile currentProfile = null;
            int currentId = 0;
            int profileId = 0;

            if (_signInManager.IsSignedIn(User))
            {
                currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);
                currentId = currentProfile.id;
            }

            if (id == null)
            {
                if (_signInManager.IsSignedIn(User))
                {
                    profileId = currentId;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                profileId = (int)id;
            }

            var profile = db.Profile.FirstOrDefault(p => p.id == profileId);
            var thisUser = db.Users.FirstOrDefault(u => u.Id == profile.UsersId);

            if (_signInManager.IsSignedIn(User))
            {
                var block = db.Blocklist.FirstOrDefault(b => b.blockedId == user.Id && b.blockerId == thisUser.Id || b.blockerId == user.Id && b.blockedId == thisUser.Id);

                if (block != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            if (profile.Disabled && profile.UsersId != user.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            if (currentProfile != null && currentProfile.Disabled)
            {
                return RedirectToAction("Ban", "Home", new { profile.id });
            }

            // Get profile Cover information
            bool isfolowing = false;
            if (user != null)
            {
                var folow = db.Follow.FirstOrDefault(f => f.folowerId == currentProfile.id && f.folowingId == profile.id);
                if (folow != null) { isfolowing = true; }
            }

            var coverClass = new CoverClass()
            {
                Profile = profile,
                User = db.Users.FirstOrDefault(u => u.Id == profile.UsersId),
                folowings = db.Follow.Where(f => f.folowerId == profile.id).Count(),
                folowers = db.Follow.Where(f => f.folowingId == profile.id).Count(),
                views = profile.View,
                isFolowing = isfolowing
            };

            var model = new CvVM()
            {
                coverClass = coverClass,
                Cv = db.Cv.FirstOrDefault(c => c.ProfileId == profile.id),
                SkillHeaders = db.SkillsHeader.Where(sh => sh.ProfileId == profile.id).ToList(),
                Skills = db.Position.ToList(),
                SkillsHeaders = db.SkillsHeaderPosition.ToList(),
                SocialActs = db.SocialAct.Where(sa => sa.UsersId == profile.UsersId).ToList(),
                Company = db.Company.ToList(),
                Experience = db.Experience.Where(exp => exp.ProfileId == profile.id).ToList(),
                Universities = db.University.Where(u => u.UserId == profile.UsersId).ToList(),
                Languages = db.Languages.ToList(),
                UsersLanguages = db.UsersLanguages.Where(ul => ul.UsersId == profile.UsersId).ToList()
            };

            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
                return PartialView("_CV", model);
            else
                return View(model);
        }




        [Route("Profile/Portfolio/{id?}")]
        public async Task<IActionResult> Portfolio(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            Profile currentProfile = null;
            int currentId = 0;
            int profileId = 0;

            if (_signInManager.IsSignedIn(User))
            {
                currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);
                currentId = currentProfile.id;
            }

            if (id == null)
            {
                if (_signInManager.IsSignedIn(User))
                {
                    profileId = currentId;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                profileId = (int)id;
            }


            var profile = db.Profile.FirstOrDefault(p => p.id == profileId);
            var thisUser = db.Users.FirstOrDefault(u => u.Id == profile.UsersId);

            if (_signInManager.IsSignedIn(User))
            {
                var block = db.Blocklist.FirstOrDefault(b => b.blockedId == user.Id && b.blockerId == thisUser.Id || b.blockerId == user.Id && b.blockedId == thisUser.Id);

                if (block != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            if (profile.Disabled && profile.UsersId != user.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            if (currentProfile != null && currentProfile.Disabled)
            {
                return RedirectToAction("Ban", "Home", new { profile.id });
            }

            // Get profile Cover information
            bool isfolowing = false;
            if (user != null)
            {
                var folow = db.Follow.FirstOrDefault(f => f.folowerId == currentProfile.id && f.folowingId == profile.id);
                if (folow != null) { isfolowing = true; }
            }
            var coverClass = new CoverClass()
            {
                Profile = profile,
                User = db.Users.FirstOrDefault(u => u.Id == profile.UsersId),
                folowings = db.Follow.Where(f => f.folowerId == profile.id).Count(),
                folowers = db.Follow.Where(f => f.folowingId == profile.id).Count(),
                views = profile.View,
                isFolowing = isfolowing
            };

            var model = new PortVM()
            {
                CoverClass = coverClass,
                Portfolio = db.Portfolio.Where(p => p.ProfileId == profile.id).ToList(),
                PortfolioImages = db.PortfolioImages.ToList()
            };

            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
                return PartialView("_Portfolio", model);
            else
                return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> Edit(string color, string button)
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);

            if (color != null)
            {
                profile.BackColor = color;
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            else if (button != null)
            {
                if (button == "0")
                {
                    profile.MessageButton = false;
                }
                else
                {
                    profile.MessageButton = true;
                }

                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("Index", "Settings");

            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCover(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);

            if (currentProfile != null && currentProfile.Disabled)
            {
                return Json(new { error = "Sizin profil ban edilmişdir" });
            }

            string uniqFileName = null;

            uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\Covers", uniqFileName);

            using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
            {
                await file.CopyToAsync(fileSteam);
            }

            var profile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);
            profile.Cover = uniqFileName;
            await db.SaveChangesAsync();

            return Json(new { response = uniqFileName });
        }


        [HttpPost]
        public async Task<IActionResult> ChangePhoto(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);

            var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);

            if (currentProfile.Disabled)
            {
                return Json(new { error = "Sizin profil ban edilmişdir" });
            }

            string uniqFileName = null;

            uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\Users", uniqFileName);

            using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
            {
                await file.CopyToAsync(fileSteam);
            }

            var profile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);
            profile.Photo = uniqFileName;
            await db.SaveChangesAsync();

            return Json(new { response = uniqFileName });
        }


        // Folow profile
        [HttpPost]
        public async Task<IActionResult> Folow(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var currProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);

            if (currProfile.Disabled)
            {
                return Json(new { error = "Sizin profiliniz ban edilmişdir" });
            }

            var folow = db.Follow.FirstOrDefault(f => f.folowerId == currProfile.id && f.folowingId == id);

            if (folow == null)
            {
                db.Follow.Add(new Follow() { folowerId = currProfile.id, folowingId = id });
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { });
            }
        }

        // Unfollow profile
        [HttpPost]
        public async Task<IActionResult> Unfollow(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var currProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);

            if (currProfile.Disabled)
            {
                return Json(new { error = "Sizin profiliniz ban edilmişdir" });
            }

            var folow = db.Follow.FirstOrDefault(f => f.folowerId == currProfile.id && f.folowingId == id);

            if (folow != null)
            {
                db.Follow.Remove(folow);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { });
            }
        }



    }
}