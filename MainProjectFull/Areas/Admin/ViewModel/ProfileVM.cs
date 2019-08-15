using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.Areas.Admin.ViewModel
{
    public class ProfileVM
    {
        public Users User { get; set; }
        public Profile Profile { get; set; }
        public List<SkillsHeader> SkillHeaders { get; set; }
        public List<Position> Position { get; set; }
        public List<SkillsHeaderPosition> SkillsHeaders { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<Company> Companies { get; set; }
        public List<Portfolio> Portfolio { get; set; }
        public List<PortfolioImages> PortfolioImages { get; set; }
        public List<University> Universities { get; set; }
        public List<SocialAct> Socials { get; set; }
    }
}
