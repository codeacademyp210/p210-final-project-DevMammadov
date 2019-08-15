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
    public class UsersController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public UsersController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("Admin/Users")]
        public async Task<IActionResult> Index()
        {
            var model = new UserVM()
            {
                Users = db.Users.ToList(),
                Profiles = db.Profile.ToList()
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

        [Route("Admin/Users/SingleUser/{id?}")]
        public async Task<IActionResult> SingleUser(int? id)
        {
            if (id != null)
            {

                var profile = db.Profile.FirstOrDefault(p => p.UsersId == id);

                var model = new ProfileVM()
                {
                    Companies = db.Company.ToList(),
                    Experiences = db.Experience.Where(ex => ex.ProfileId == profile.id).ToList(),
                    Portfolio = db.Portfolio.Where(p => p.ProfileId == profile.id).ToList(),
                    PortfolioImages = db.PortfolioImages.ToList(),
                    Position = db.Position.ToList(),
                    Profile = profile,
                    SkillHeaders = db.SkillsHeader.Where(sk => sk.ProfileId == profile.id).ToList(),
                    SkillsHeaders = db.SkillsHeaderPosition.ToList(),
                    User = db.Users.FirstOrDefault(u => u.Id == id),
                    Universities = db.University.Where(u => u.UserId == id).ToList(),
                    Socials = db.SocialAct.Where(s => s.UsersId == id).ToList()
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
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Users User, IFormFile file)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == User.Id);
            user.Name = User.Name;
            user.Surname = User.Surname;
            user.Email = User.Email;
            user.PhoneNumber = User.PhoneNumber;
            user.About = User.About;
            user.Address = User.Address;
            user.Profession = User.Profession;
            user.Facebook = User.Facebook;
            user.Behance = User.Behance;
            user.Linkedin = User.Linkedin;
            user.Github = User.Github;
            user.Status = User.Status;

            if (file != null)
            {
                string uniqFileName = null;
                uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\Users", uniqFileName);

                using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
                {
                    await file.CopyToAsync(fileSteam);
                }

                var profile = db.Profile.FirstOrDefault(p => p.UsersId == User.Id);
                profile.Photo = uniqFileName;
            }

            db.Notify.Add(new Notify()
            {
                Photo = "edit.png",
                Text = "Məlumatlarınız administrasiya tərəfindən yeniləndi",
                PortfolioId = null,
                Date = DateTime.Now,
                SenderName = "System",
                SenderSurname = "",
                Type = "system",
                Status = false,
                UsersId = User.Id,
                SenderId = null
            });

            db.SaveChanges();
            return RedirectToAction("SingleUser", new { id = User.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePort(int? id, int? userId)
        {
            if (id != null && userId != null)
            {
                var port = db.Portfolio.FirstOrDefault(p => p.id == id);

                foreach (var item in db.PortfolioImages.Where(img => img.PortfolioId == port.id))
                {
                    db.PortfolioImages.Remove(item);
                }
                db.Portfolio.Remove(port);

                // send notification to user
                db.Notify.Add(new Notify()
                {
                    Photo = "remove.png",
                    Text = port.Name + " adlı portfolionuz administrasiya tərəfindən silinmişdir",
                    PortfolioId = null,
                    Date = DateTime.Now,
                    SenderName = "System",
                    SenderSurname = "",
                    Type = "system",
                    Status = false,
                    UsersId = (int)userId,
                    SenderId = null
                });

                await db.SaveChangesAsync();
                return RedirectToAction("SingleUser", new { id = userId });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveExperience(int? id, int? userId)
        {
            if (id != null && userId != null)
            {
                var exper = db.Experience.FirstOrDefault(p => p.id == id);
                var company = db.Company.FirstOrDefault(c => c.id == exper.CompanyId);
                db.Experience.Remove(exper);

                // send notification to user
                db.Notify.Add(new Notify()
                {
                    Photo = "remove.png",
                    Text = company.Name + " adlı yerdəki iş yeriniz sistem tərəfindən silinmişdir",
                    PortfolioId = null,
                    Date = DateTime.Now,
                    SenderName = "System",
                    SenderSurname = "",
                    Type = "system",
                    Status = false,
                    UsersId = (int)userId,
                    SenderId = null
                });

                await db.SaveChangesAsync();
                return RedirectToAction("SingleUser", new { id = userId });
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSkill(int? id, int? userId)
        {
            if (id != null && userId != null)
            {
                var skill = db.Position.FirstOrDefault(p => p.id == id);
                var profile = db.Profile.FirstOrDefault(p => p.UsersId == userId);
                string headerName = "";

                foreach (var item in db.SkillsHeader.Where(sh => sh.ProfileId == profile.id))
                {
                    foreach (var pear in db.SkillsHeaderPosition.Where(s => s.SkillsHeaderId == item.id))
                    {
                        var sk = db.Position.FirstOrDefault(p => p.id == pear.PositionId);
                        if (sk.id == id)
                        {
                            var headerSkillPear = db.SkillsHeaderPosition.FirstOrDefault(sh => sh.PositionId == sk.id && sh.SkillsHeaderId == item.id);
                            db.SkillsHeaderPosition.Remove(headerSkillPear);
                            headerName = item.Name;

                            // send notification to user
                            db.Notify.Add(new Notify()
                            {
                                Photo = "remove.png",
                                Text = "Sizin " + headerName + " Biliklərinizdən " + skill.Name + " sistem tərəfindən silinmişdir",
                                PortfolioId = null,
                                Date = DateTime.Now,
                                SenderName = "System",
                                SenderSurname = "",
                                Type = "system",
                                Status = false,
                                UsersId = (int)userId,
                                SenderId = null
                            });

                            await db.SaveChangesAsync();
                            return RedirectToAction("SingleUser", new { id = userId });
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUniversity(int? id, int? userId)
        {
            if (id != null && userId != null)
            {
                var univer = db.University.FirstOrDefault(u => u.id == id);
                db.University.Remove(univer);
                // send notification to user
                db.Notify.Add(new Notify()
                {
                    Photo = "remove.png",
                    Text = univer.Name + " təhsil yeriniz sistem tərəfindən silinmişdir",
                    PortfolioId = null,
                    Date = DateTime.Now,
                    SenderName = "System",
                    SenderSurname = "",
                    Type = "system",
                    Status = false,
                    UsersId = (int)userId,
                    SenderId = null
                });

                await db.SaveChangesAsync();
                return RedirectToAction("SingleUser", new { id = userId });
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSocial(int? id, int? userId)
        {
            if (id != null && userId != null)
            {
                var social = db.SocialAct.FirstOrDefault(u => u.id == id);
                db.SocialAct.Remove(social);
                // send notification to user
                db.Notify.Add(new Notify()
                {
                    Photo = "remove.png",
                    Text = social.Company + " adlı yerdəki sosial fəaliyyətiniz sistem tərəfindən silinmişdir",
                    PortfolioId = null,
                    Date = DateTime.Now,
                    SenderName = "System",
                    SenderSurname = "",
                    Type = "system",
                    Status = false,
                    UsersId = (int)userId,
                    SenderId = null
                });

                await db.SaveChangesAsync();
                return RedirectToAction("SingleUser", new { id = userId });
            }
            return RedirectToAction("Index");
        }


















    }
}