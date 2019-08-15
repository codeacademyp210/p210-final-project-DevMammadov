using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using MainProjectFull.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainProjectFull.Controllers
{
    public class MessageController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public MessageController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [Route("/Message/Index/{id?}")]
        public async Task<IActionResult> Index(int? id)
        {
            if (_signInManager.IsSignedIn(User) && id != null)
            {

                int fId = (int)id;
                var currendUser = await _userManager.GetUserAsync(User);
                var currendProfile = db.Profile.FirstOrDefault(p => p.UsersId == currendUser.Id);

                if (!currendProfile.Disabled)
                {

                    var friend = db.Users.FirstOrDefault(u => u.Id == fId);
                    var friendProfile = db.Profile.FirstOrDefault(p => p.UsersId == friend.Id);

                    if (!friendProfile.Disabled)
                    {
                          
                        var messages = db.Messages.Where(m => m.SenderId == currendUser.Id && m.GetterId == id || m.GetterId == currendUser.Id && m.SenderId == id).ToList();

                            var model = new ChatVM()
                            {
                                CurrentProfile = currendProfile,
                                CurrentUser = currendUser,
                                Friend = friend,
                                friendProfile = friendProfile,
                                Messages = messages
                            };

                            return View(model);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Ban", "Home", new { currendProfile.id });
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // Send message
        public async Task<IActionResult> Send(int id, string text)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var friend = db.Users.FirstOrDefault(u => u.Id == id);
            var block = db.Blocklist.FirstOrDefault(b => b.blockedId == friend.Id && b.blockerId == currentUser.Id || b.blockerId == friend.Id && b.blockedId == currentUser.Id);

            if (block != null)
            {
                return Json(new { error = "Siz bu yazışmaya mesaj göndərə bilmərsiniz" });
            }
            else
            {
                var message = new Messages()
                {
                    SenderId = currentUser.Id,
                    GetterId = id,
                    Message = text,
                    Date = DateTime.Now,
                    Status = false
                };

                db.Messages.Add(message);
                await db.SaveChangesAsync();

                return Json(new { message.id });
            }
        }

        [HttpPost]
        // Get message
        public async Task<IActionResult> Get(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var messages = db.Messages.Where(m => m.SenderId == id && m.GetterId == currentUser.Id && m.Status == false).ToList();

            foreach (var msg in messages)
            {
                msg.Status = true;
            }

            await db.SaveChangesAsync();

            return Json(new { messages });
        }


        [HttpPost]
        // Get message
        public async Task<IActionResult> RemoveChat(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if(currentUser != null)
            {
                foreach(var msg in db.Messages.Where(m => m.SenderId == currentUser.Id && m.GetterId == id || m.GetterId == currentUser.Id && m.SenderId == id))
                {
                    db.Messages.Remove(msg);
                }
                await db.SaveChangesAsync();

                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }







    }
}