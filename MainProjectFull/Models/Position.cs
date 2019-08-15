using System.Collections.Generic;

namespace MainProjectFull.Models
{
    public class Position
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Type { get; set; }
        public bool Veryfied { get; set; }
        public IList<SkillsHeaderPosition> SkillsHeaderPosition { get; set; }
    }
}
