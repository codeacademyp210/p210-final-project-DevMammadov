using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class AboutVM
    {
        public CoverClass CoverClass { get; set; }
        public List<SocialAct> SosialActs { get; set; }
        public List<University> Universities { get; set; }
    }
}
