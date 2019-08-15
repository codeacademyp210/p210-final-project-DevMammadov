using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class CvBoxVM
    {
        public Users User { get; set; }
        public Profile Profile { get; set; }
        public Cv Cv { get; set; }
        public List<SkillsHeader> SkillHeaders { get; set; }
        public List<SkillsHeaderPosition> SkillsHeaders { get; set; }
        public List<Position> Skills { get; set; }
        public List<University> Universities { get; set; }
        public List<Company> Company { get; set; }
        public List<Experience> Experience { get; set; }
        public List<SocialAct> SocialActs { get; set; }
        public List<UsersLanguages> UsersLanguages { get; set; }
        public List<Languages> Languages { get; set; }
    }
}
