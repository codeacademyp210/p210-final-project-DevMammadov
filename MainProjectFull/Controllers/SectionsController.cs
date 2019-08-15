using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MainProjectFull.Controllers
{
    public class SectionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Section(string sec)
        {
            if(sec == "skills"){
                return PartialView("_Skills");

            }
            else
            {
                return PartialView("_Experience");
            }
        }
    }
}