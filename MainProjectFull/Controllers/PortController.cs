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
    public class PortController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public PortController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Save(string name, string portType, string about, string github, string behance, string website, IFormFile[] files)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            string responseImg = null;

            if (currentUser != null)
            {
                var profile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
                var newPortfolio = new Portfolio()
                {
                    Name = name,
                    About = about,
                    Behance = behance,
                    Github = github,
                    Website = website,
                    ProfileId = profile.id,
                    Type = portType,
                    View = 0,
                };
                db.Portfolio.Add(newPortfolio);

                foreach (IFormFile file in files)
                {
                    string uniqFileName = null;
                    uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\PortImages", uniqFileName);

                    using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
                    {
                        await file.CopyToAsync(fileSteam);
                    }

                    db.PortfolioImages.Add(new PortfolioImages() { Name = uniqFileName, PortfolioId = newPortfolio.id });
                    await db.SaveChangesAsync();

                    responseImg = uniqFileName;
                }

                // nofify followers about this
                var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
                foreach (var folower in db.Follow.Where(f => f.folowingId == currentProfile.id))
                {
                    foreach (var prof in db.Profile.Where(p => p.id == folower.folowerId))
                    {
                        var user = db.Users.FirstOrDefault(u => u.Id == prof.UsersId);
                        db.Notify.Add(new Notify()
                        {
                            Photo = responseImg,
                            Text = name + " adlı yeni portfolio paylaşdı",
                            PortfolioId = newPortfolio.id,
                            Date = DateTime.Now,
                            SenderName = currentUser.Name,
                            SenderSurname = currentUser.Surname,
                            Type = "portImg",
                            Status = false,
                            UsersId = user.Id,
                            SenderId = currentUser.Id,
                        });
                    }
                }

                await db.SaveChangesAsync();



                return Json(new { newPortfolio.id, responseImg });
            }
            else
            {
                return Json(new { error = "You dont have permission for this operation" });
            }

        }

        [Route("Port/GetPortfolio/{id?}")]
        public async Task<IActionResult> GetPortfolio(int? id)
        {
            Profile currentProfile = new Profile();
            Users currentuser = new Users();

            if (_signInManager.IsSignedIn(User))
            {
                currentuser = await _userManager.GetUserAsync(User);
                currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentuser.Id);
            }

            if (id == null)
            {
                return Json(new { error = "Portfolio tapılmadı ola bilsin silinib" });
            }

            var port = db.Portfolio.FirstOrDefault(p => p.id == id);

            if (port == null)
            {
                return Json(new { error = "Portfolio tapılmadı ola bilsin silinib" });
            }

            // if user is not self than add view
            if (port.ProfileId != currentProfile.id)
            {
                port.View = port.View + 1;
            }

            await db.SaveChangesAsync();

            var allImgs = db.PortfolioImages.Where(img => img.PortfolioId == port.id).ToList();
            allImgs.Reverse();
            var comments = db.Comments.Where(c => c.PortfolioId == port.id).ToList();
            comments.Reverse();

            var model = new ImgBoxVM()
            {
                Portfolio = port,
                PortImages = allImgs,
                Profile = db.Profile.FirstOrDefault(p => p.id == port.ProfileId),
                CurrentUser = currentuser,
                Comments = comments,
                Users = db.Users.ToList(),
                Profiles = db.Profile.ToList()
            };
            return PartialView("_ImgBox", model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImg(int imgId, int portId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
                var portfolio = db.Portfolio.FirstOrDefault(p => p.id == portId && p.ProfileId == currentProfile.id);
                var img = db.PortfolioImages.FirstOrDefault(p => p.PortfolioId == portfolio.id && p.id == imgId);

                if (db.PortfolioImages.Where(p => p.PortfolioId == portfolio.id).Count() < 2)
                {

                    return Json(new { error = "Prtfolioda ən azı 1 şəkil olmalıdır" });
                }
                else
                {
                    db.PortfolioImages.Remove(img);
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
            }
            else
            {
                return Json(new { error = "Yout dont have permission for this operation" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddImg(IFormFile file, int id)
        {
            string uniqFileName = null;
            uniqFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string uploadTo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Images\\PortImages", uniqFileName);

            using (var fileSteam = new FileStream(uploadTo, FileMode.Create))
            {
                await file.CopyToAsync(fileSteam);
            }

            var newImg = new PortfolioImages() { Name = uniqFileName, PortfolioId = id };

            db.PortfolioImages.Add(newImg);
            await db.SaveChangesAsync();

            return Json(new { newImg.id, uniqFileName });
        }


        [HttpPost]
        public async Task<IActionResult> AddComment(int id, string text)
        {

            if (_signInManager.IsSignedIn(User))
            {
                var currentuser = await _userManager.GetUserAsync(User);
                var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentuser.Id);
                var CommentedPort = db.Portfolio.FirstOrDefault(p => p.id == id);
                var commentedProfile = db.Profile.FirstOrDefault(p => p.id == CommentedPort.ProfileId);
                var commentedUser = db.Users.FirstOrDefault(u => u.Id == commentedProfile.UsersId);

                var comment = new Comments()
                {
                    PortfolioId = id,
                    SenderId = currentuser.Id,
                    Title = "şərh yazdı",
                    Text = text,
                    Date = DateTime.Now
                };

                db.Comments.Add(comment);
                await db.SaveChangesAsync();

                var notify = new Notify()
                {
                    PortfolioId = id,
                    Photo = currentProfile.Photo,
                    Status = false,
                    Type = "port",
                    Text = text,
                    SenderName = currentuser.Name,
                    SenderSurname = currentuser.Surname,
                    SenderId = currentuser.Id,
                    UsersId = commentedUser.Id,
                    Date = DateTime.Now
                };

                db.Notify.Add(notify);
                await db.SaveChangesAsync();

                return Json(new { success = true, name = currentuser.Name, surname = currentuser.Surname, comment.id, img = currentProfile.Photo });
            }
            else
            {
                return Json(new { error = "You must login for post comment" });
            }
        }


        public async Task<IActionResult> Remove(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);

                var port = db.Portfolio.FirstOrDefault(p => p.id == id && p.ProfileId == currentProfile.id);

                if (port != null)
                {
                    foreach (var item in db.PortfolioImages.Where(img => img.PortfolioId == id))
                    {
                        db.PortfolioImages.Remove(item);
                    }

                    db.Portfolio.Remove(port);
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { error = "Portfolio silimədi" });
                }
            }
            else
            {
                return Json(new { error = "You dont have permission for this information" });
            }
        }




    }
}