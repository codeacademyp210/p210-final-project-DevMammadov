using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainProjectFull.Controllers
{
    public class ExperienceController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public ExperienceController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Get users all experience list
        [HttpPost]
        public IActionResult GetAll()
        {
            var Companies = db.Company.ToList();
            return Json(new { result = Companies });
        }

        // Save experience
        [HttpPost]
        public async Task<IActionResult> SaveAsync(string CompanyName, int? CompanyId, string startDate, string endDate, string about)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var profile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
            int compId;
            int? responseId;

            if (CompanyId == null)
            {
                // if id null then create new company
                var company = new Company() { Name = CompanyName, Icon = "none.png" };
                db.Company.Add(company);
                compId = company.id;
                responseId = company.id;
            }
            else
            {
                compId = (int)CompanyId;
                responseId = null;
            }

            var experience = new Experience()
            {
                About = about,
                StartDate = Convert.ToDateTime(startDate),
                EndDate = Convert.ToDateTime(endDate),
                CompanyId = compId,
                ProfileId = profile.id
            };

            // nofify followers about it
            var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
            foreach (var folower in db.Follow.Where(f => f.folowingId == currentProfile.id))
            {
                foreach (var prof in db.Profile.Where(p => p.id == folower.folowerId))
                {
                    var user = db.Users.FirstOrDefault(u => u.Id == prof.UsersId);
                    db.Notify.Add(new Notify()
                    {
                        Photo = profile.Photo,
                        Text = CompanyName + " adlı yerdə iş təcrübəsi paylaşdı",
                        PortfolioId = null,
                        Date = DateTime.Now,
                        SenderName = currentUser.Name,
                        SenderProfileId = currentProfile.id,
                        SenderSurname = currentUser.Surname,
                        Type = "exper",
                        Status = false,
                        UsersId = user.Id,
                        SenderId = currentUser.Id
                    });
                }
            }


            db.Experience.Add(experience);
            await db.SaveChangesAsync();

            return Json(new { experId = experience.id, companyId = responseId });

        }

    // Edit experience
    [HttpPost]
    public async Task<IActionResult> Edit(string about, string startDate, string EndDate, string id)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
        var expId = Convert.ToInt32(id);
        var exper = db.Experience.FirstOrDefault(e => e.id == expId && e.ProfileId == currentProfile.id);

        if (about != null)
        {
            exper.About = about;
            await db.SaveChangesAsync();
        }
        else if (startDate != null)
        {
            exper.StartDate = Convert.ToDateTime(startDate);
            await db.SaveChangesAsync();
        }
        else if (EndDate != null)
        {
            exper.EndDate = Convert.ToDateTime(EndDate);
            await db.SaveChangesAsync();
        }
        else
        {
            return Json(new { response = "Ooops! About text is not changed" });
        }

        return Json(new { success = true });
    }

    // Remove experience
    public async Task<IActionResult> Remove(string id)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
        var expId = Convert.ToInt32(id);
        var exper = db.Experience.FirstOrDefault(e => e.id == expId && e.ProfileId == currentProfile.id);

        db.Experience.Remove(exper);
        var result = await db.SaveChangesAsync();

        if (result > 0)
        {
            return Json(new { success = true });
        }
        else
        {
            return Json(new { response = "Ooops! Experience did not removed" });
        }
    }


}
}