using System.Collections.Generic;

namespace MainProjectFull.Models
{
    public class SkillsHeader
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public IList<SkillsHeaderPosition> SkillsHeaderPosition { get; set; }

    }
}
