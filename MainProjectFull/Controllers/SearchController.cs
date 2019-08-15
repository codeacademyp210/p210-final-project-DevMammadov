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
    public class SearchController : Controller
    {
        public readonly CodeContext db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public SearchController(CodeContext db, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Sorting portfolio
        public async Task<IActionResult> GetPorts(string sort)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            List<Portfolio> Portfolios = new List<Portfolio>();

            switch (sort)
            {
                case "all":
                    Portfolios = db.Portfolio.Where(p => p.Profile.Disabled == false).ToList();
                    break;
                case "topview":
                    Portfolios = db.Portfolio.Where(p => p.Profile.Disabled == false).OrderByDescending(p => p.View).ToList();
                    break;
                case "design":
                    Portfolios = db.Portfolio.Where(p => p.Profile.Disabled == false).Where(p => p.Type.Contains("design")).ToList();
                    break;
                case "web":
                    Portfolios = db.Portfolio.Where(p => p.Profile.Disabled == false).Where(p => p.Type.Contains("web")).ToList();
                    break;

            }

            var model = new PortSearchVM()
            {
                Portfolios = Portfolios,
                Users = db.Users.ToList(),
                CurrentUser = currentUser,
                PortfolioImages = db.PortfolioImages.ToList(),
                Profiles = db.Profile.Where(p => p.Disabled == false).ToList()
            };

            return PartialView("_indexPortfolio", model);
        }

        // Get Cv PArtial
        public IActionResult Cv()
        {
            var model = new CvSearch()
            {
                Cvs = db.Cv.Where(cv => cv.Profile.Disabled == false).ToList(),
                Skills = db.Position.ToList(),
                Users = db.Users.ToList(),
                Profiles = db.Profile.Where(p => p.Disabled == false).ToList()
            };

            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
                return PartialView("_indexCv", model);
            else
                return View(model);
        }


        // Sorting Cv
        [Route("Search/Cv/Tags/{tags?}")]
        public async Task<IActionResult> GetCv(string tags)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (tags == null)
            {
                return RedirectToAction("Cv", "Search");
            }

            string[] skillsQuery = tags.Split(",");
            List<string> Skills = new List<string>();
            List<Users> FindedUsers = new List<Users>();

            foreach (string skill in skillsQuery)
            {
                Skills.Add(skill.Replace('-', ' '));
            }

            foreach (var user in db.Users.ToList())
            {
                List<string> userSkills = new List<string>();
                var profile = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);

                if (!profile.Disabled)
                {
                    var sh = db.SkillsHeader.Where(s => s.ProfileId == profile.id);

                    foreach (var header in sh)
                    {
                        foreach (var pear in db.SkillsHeaderPosition.Where(p => p.SkillsHeaderId == header.id))
                        {
                            var skill = db.Position.FirstOrDefault(p => p.id == pear.PositionId);
                            userSkills.Add(skill.Name.ToLower());
                        }
                    }

                    int c = 0;
                    foreach (var item in userSkills)
                    {
                        foreach (var skill in Skills)
                        {
                            if (item.ToLower() == skill.ToLower())
                            {
                                c++;
                            }
                        }
                    }

                    if (c == Skills.Count())
                    {
                        FindedUsers.Add(user);
                    }
                }
            }

            var model = new CvSearch()
            {
                Cvs = db.Cv.ToList(),
                Skills = db.Position.ToList(),
                Users = FindedUsers,
                Profiles = db.Profile.ToList()
            };


            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
                return PartialView("_indexCv", model);
            else
                return View("Cv", model);
        }


        // Get persons
        public IActionResult Person()
        {
            var model = new PersonSearchVM()
            {
                Profiles = db.Profile.Where(p => p.Disabled == false).ToList(),
                Users = db.Users.ToList()
            };

            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
                return PartialView("_indexPerson", model);
            else
                return View(model);
        }


        //Sorting Person
        public async Task<IActionResult> GetPerson(string sort)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            List<Profile> profileList = new List<Profile>();

            switch (sort)
            {
                case "all":
                    profileList = db.Profile.Where(p => p.Disabled == false).ToList();
                    break;
                case "topview":
                    profileList = db.Profile.Where(p => p.Disabled == false).OrderByDescending(p => p.View).ToList();
                    break;
                case "topPortfolio":
                    profileList = db.Profile.Where(p => p.Disabled == false).OrderByDescending(p => p.Portfolios.Count).ToList();
                    break;
                case "code":
                    profileList = db.Profile.Where(p => p.Type.Contains("code") && p.Disabled == false).ToList();
                    break;
                case "design":
                    profileList = db.Profile.Where(p => p.Type.Contains("design") && p.Disabled == false).ToList();
                    break;

            }

            var model = new PersonSearchVM()
            {
                Users = db.Users.ToList(),
                Profiles = profileList
            };

            return PartialView("_indexPerson", model);
        }


        // Search all name
        [HttpPost]
        [Route("Search/GetByName")]
        public async Task<IActionResult> GetByName(string type, string text)
        {
            string searctTxt = text.ToLower();

            if (type == "cv")
            {
                List<Cv> cvList = new List<Cv>();
                List<Profile> profList = new List<Profile>();
                List<Users> usersList = new List<Users>();

                var findedUsers = db.Users.Where(u => u.Name.ToLower().Contains(searctTxt) || u.Surname.ToLower().Contains(searctTxt) || u.Email.ToLower().Contains(searctTxt)).ToList();
                foreach (var item in findedUsers)
                {
                    var profile = db.Profile.FirstOrDefault(p => p.UsersId == item.Id);

                    if (profile.Disabled == false)
                    {
                        cvList.Add(db.Cv.FirstOrDefault(c => c.ProfileId == profile.id));
                        profList.Add(profile);
                        usersList.Add(item);
                    }
                }

                var model = new CvSearch()
                {
                    Cvs = cvList,
                    Skills = db.Position.ToList(),
                    Users = usersList,
                    Profiles = profList
                };
                return PartialView("_indexCv", model);
            }
            else if (type == "person")
            {

                List<Profile> profiles = new List<Profile>();
                List<Users> userList = new List<Users>();
                var users = db.Users.Where(u => u.Name.ToLower().Contains(searctTxt) || u.Surname.ToLower().Contains(searctTxt) || u.Email.ToLower().Contains(searctTxt)).ToList();

                foreach (var user in users)
                {
                    var prof = db.Profile.FirstOrDefault(p => p.UsersId == user.Id);
                    if (prof.Disabled == false)
                    {
                        profiles.Add(prof);
                        userList.Add(user);
                    }
                }

                var userModel = new PersonSearchVM()
                {
                    Users = users,
                    Profiles = profiles
                };

                return PartialView("_indexPerson", userModel);
            }
            else if (type == "port")
            {

                var portList = db.Portfolio.Where(p => p.Name.ToLower().Contains(searctTxt) && p.Profile.Disabled == false).ToList();

                var portModel = new PortSearchVM()
                {
                    Portfolios = portList,
                    Users = db.Users.ToList(),
                    CurrentUser = await _userManager.GetUserAsync(User),
                    PortfolioImages = db.PortfolioImages.ToList(),
                    Profiles = db.Profile.Where(p => p.Disabled == false).ToList()
                };
                return PartialView("_indexPortfolio", portModel);
            }
            else
            {
                return Json(new { error = "Netice tapilmadi" });

            }
        }





    }
}