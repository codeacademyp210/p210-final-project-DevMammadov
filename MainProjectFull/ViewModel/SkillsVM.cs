using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class SkillsVM
    {
        public List<SkillsHeader> SkillsHeader { get; set; }
        public List<SkillsHeaderPosition> SkillsHeaderPosition { get; set; }
        public List<Position> Position { get; set; }
        public List<UsersLanguages> UserLanguages { get; set; }
        public List<Languages> Languages { get; set; }

        public CoverClass CoverClass { get; set; }
    }
}
