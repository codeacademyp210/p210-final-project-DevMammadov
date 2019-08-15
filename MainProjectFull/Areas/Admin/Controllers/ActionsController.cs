using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProjectFull.DAL;
using MainProjectFull.Models;
using Microsoft.AspNetCore.Mvc;

namespace MainProjectFull.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActionsController : Controller
    {
        public readonly CodeContext db;

        public ActionsController(CodeContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IActionResult SendWarning(string text, int id)
        {
            db.Notify.Add(new Notify()
            {
                Photo = "warning.png",
                Text = text,
                PortfolioId = null,
                Date = DateTime.Now,
                SenderName = "Diqqət! ",
                SenderSurname = " Xəbərdarlıq",
                Type = "system",
                Status = false,
                UsersId = id,
                SenderId = null
            });

            db.SaveChanges();

            return RedirectToAction("SingleUser","Users", new { id });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Ban(string text, int id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            var profile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);
            profile.BanReason = text;
            profile.Disabled = true;

            db.SaveChanges();

            return RedirectToAction("SingleUser", "Users", new { id });
        }


        [HttpGet]
        public IActionResult Free(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            var profile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);
            profile.BanReason = "";
            profile.Disabled = false;

            db.SaveChanges();

            return RedirectToAction("SingleUser", "Users", new { id });
        }





    }
}