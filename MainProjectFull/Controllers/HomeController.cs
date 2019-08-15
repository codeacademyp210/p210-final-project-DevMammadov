using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MainProjectFull.Models;
using MainProjectFull.DAL;
using Microsoft.AspNetCore.Identity;
using MainProjectFull.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MainProjectFull.Controllers
{
    public class HomeController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private List<Messages> notifies;

        public HomeController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user =  await _userManager.GetUserAsync(User);
            Users currentUser = new Users();

            if(user != null)
            {
                currentUser = user;
                var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
                if (currentProfile.Disabled)
                {
                    return RedirectToAction("Ban", new { currentProfile.id });
                }
            }
            else
            {
                currentUser = null;
            }
            
            var model = new PortSearchVM()
            {
                Portfolios = db.Portfolio.Where(p => p.Profile.Disabled == false).ToList(),
                Users = db.Users.ToList(),
                CurrentUser = currentUser,
                PortfolioImages = db.PortfolioImages.ToList(),
                Profiles = db.Profile.Where(p => p.Disabled == false).ToList()
            };

            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
                return PartialView("_indexPortfolio", model);
            else
                return View(model);
        }

        // Getting notifications
        [HttpPost]
        public async Task<IActionResult> GetNotifications(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var notifies = db.Notify.Where(n => n.UsersId == id && n.SenderId != id).ToList();
                int unreadCount = db.Notify.Where(n => n.Status == false && n.UsersId == id && n.SenderId != id).Count();

                return Json(new { notifies , unreadCount });
            }

            return Json(new { error = "You dont have permission for this information" });
        }

        // Getting messages
        //[HttpPost]
        //public async Task<IActionResult> GetMessages(int id)
        //{
        //    if (_signInManager.IsSignedIn(User))
        //    {

        //        List<Messages> messages = db.Messages.Where(m => m.GetterId == id).ToList();
        //        List<MessageVM> uniqueUser = new List<MessageVM>();

        //        foreach(var user in db.Users.ToList())
        //        {
        //            var msg = messages.FirstOrDefault(m => m.SenderId == user.Id);
                    
        //            if(msg != null)
        //            {
        //                var mess = new MessageVM()
        //                {
        //                    Message = msg.Message,
        //                    Name = user.Name,
        //                    Surname = user.Surname,
        //                    Photo = db.Profile.FirstOrDefault(p => p.UsersId == user.Id).Photo,
        //                    SenderId = user.Id,
        //                    Date = msg.Date.ToString()
        //                };

        //                uniqueUser.Add(mess);
        //            }
        //        }

        //        int unreadCount = db.Messages.Where(m => m.GetterId == id && m.Status == false).Count();

        //        return Json(new { uniqueUser, unreadCount });
        //    }

        //    return Json(new { error = "You dont have permission for this information" });
        //}


        // Read notifications
        [HttpPost]
        public async Task<IActionResult> ReadNotify(string email)
        {
                var user = db.Users.FirstOrDefault(u => u.Email == email);
                foreach (var item in db.Notify.Where(n=> n.UsersId == user.Id))
                {
                    item.Status = true;
                }
                await db.SaveChangesAsync();

                return Json(new { success = true });
        }

        
        // Ban user
        public async Task<IActionResult> Ban(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var currentProfile = db.Profile.FirstOrDefault(p => p.UsersId == currentUser.Id);
                var banned = db.Profile.FirstOrDefault(p => p.id == id);

                if (banned.Disabled)
                {
                    if(banned.id == currentProfile.id)
                    {
                        ViewBag.reason = banned.BanReason;

                        await _signInManager.SignOutAsync();
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("About","Profile");
                    }
                }
                else
                {
                    return RedirectToAction("About", "Profile",new { banned.id });
                }
                
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // Block user
        public async Task<IActionResult> Block(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                db.Blocklist.Add(new Blocklist() { blockerId = currentUser.Id, blockedId = id });
                await db.SaveChangesAsync();

                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
