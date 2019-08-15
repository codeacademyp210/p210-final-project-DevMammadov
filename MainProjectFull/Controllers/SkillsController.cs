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
using Newtonsoft.Json;

namespace MainProjectFull.Controllers
{
    public class SkillsController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public SkillsController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> GetSkillsAsync()
        { 
                var allSkills = await db.Position.ToListAsync();
                return Json(new { skills = allSkills });
        }

        public async Task<IActionResult> GetSkillHeaders()
        {
                var skillHeaders = await db.SkillsHeader.ToListAsync();
                return Json(new { skillHeaders });
        }


        public async Task<IActionResult> GetSkillsHeadersPear()
        {
            
                var skillsHeaders = await db.SkillsHeaderPosition.ToListAsync();
                return Json(new { skillsHeaders });
        }


        [HttpPost]
        public async Task<IActionResult> CreateSkill(string name,string type, IFormFile file)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                if (name != null && file != null)
                {
                    string uniqFileName = null;

                    uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\Skillicons", uniqFileName);

                    using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
                    {
                        await file.CopyToAsync(fileSteam);
                    }

                    await db.Position.AddAsync(new Position { Name = name, Icon = uniqFileName,Type = type, Veryfied = false });
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { error = "oops! Skill did not sent" });
                }
            }

            return Json(new { error = "You dont have permission for this operation" });

        }


        [HttpPost]
        public async Task<IActionResult> SaveAsync(string skills, string header)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var profile = await db.Profile.FirstOrDefaultAsync(p => p.UsersId == currentUser.Id);

                List<Position> GetPositions = JsonConvert.DeserializeObject<List<Position>>(skills);

                var Header = new SkillsHeader() { Name = header, ProfileId = profile.id };

                await db.SkillsHeader.AddAsync(Header);
                await db.SaveChangesAsync();

                int headerId = Header.id;

                foreach (var item in GetPositions)
                {
                    await db.SkillsHeaderPosition.AddAsync(new SkillsHeaderPosition()
                    {
                        PositionId = item.id,
                        SkillsHeaderId = headerId
                    });

                    await db.SaveChangesAsync();
                }

                return Json(new { success = headerId });
            }

            return Json(new { });
        }


        [HttpPost]
        public async Task<IActionResult> RemoveTopic(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            int headerId = Convert.ToInt32(id);

            if (currentUser != null)
            {
                var profile = await db.Profile.FirstOrDefaultAsync(p => p.UsersId == currentUser.Id);
                var headerToRemove = await db.SkillsHeader.FirstOrDefaultAsync(sh => sh.id == headerId && sh.ProfileId == profile.id);

                var skillHeaders = db.SkillsHeaderPosition.Where(p => p.SkillsHeaderId == headerToRemove.id);

                db.SkillsHeaderPosition.RemoveRange(skillHeaders);
                await db.SaveChangesAsync();


                db.SkillsHeader.Remove(headerToRemove);
                await db.SaveChangesAsync();

                return Json(new { success = true });
            }
            return Json(new { error = "oops! something wrong" });

        }


        [HttpPost]
        public async Task<IActionResult> EditTopic(string id, string name, string skillId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var profile = await db.Profile.FirstOrDefaultAsync(p => p.UsersId == currentUser.Id);

            if (skillId == null)
            {
                int headerId = Convert.ToInt32(id);
                var header = await db.SkillsHeader.FirstOrDefaultAsync(h => h.id == headerId && h.ProfileId == profile.id);
                header.Name = name;
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            else if (skillId != null)
            {
                var skillID = Convert.ToInt32(skillId);
                var topicID = Convert.ToInt32(id);

                await db.SkillsHeaderPosition.AddAsync(new SkillsHeaderPosition() { PositionId = skillID, SkillsHeaderId = topicID });
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return Json(new { error = "oops! something wrong!!" });
        }


        [HttpPost]
        public async Task<IActionResult> RemoveSkill(string topicId, string skillId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var skillID = Convert.ToInt32(skillId);
            var topicID = Convert.ToInt32(topicId);

            if (currentUser != null)
            {
                var skillsHeader = await db.SkillsHeaderPosition.FirstOrDefaultAsync(sk => sk.SkillsHeaderId == topicID && sk.PositionId == skillID);
                db.SkillsHeaderPosition.Remove(skillsHeader);

                await db.SaveChangesAsync();

                return Json(new { success = true });
            }
            return Json(new { error = "oops! something wrong!!" });
        }


        // Languages
        [HttpPost]
        public async Task<IActionResult> GetLangs()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                var langs = db.Languages.ToList();
                return Json(new { langs });
            }
            else
            {
                return Json(new { error = "You dont have permission for this operation" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SaveLang(string id, string level)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && id != null)
            {
                var langId = Convert.ToInt32(id);
                var lang = new UsersLanguages() { LanguagesId = langId, Level = level, UsersId = currentUser.Id };
                var langs = db.UsersLanguages.Add(lang);
                await db.SaveChangesAsync();

                return Json(new { success = true });
            }
            else
            {
                return Json(new { error = "You dont have permission for this operation" });

            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveLang(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && id != null)
            {
                var langId = Convert.ToInt32(id);
                var langs = db.UsersLanguages.FirstOrDefault(ul => ul.LanguagesId == langId && ul.UsersId == currentUser.Id);
                db.UsersLanguages.Remove(langs);
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