using System.ComponentModel.DataAnnotations;

namespace MainProjectFull.Models
{
    public class SkillsHeaderPosition
    {

        public int id { get; set; }
        public int PositionId { get; set; }
        public int SkillsHeaderId { get; set; }
        public Position Position { get; set; }
        public SkillsHeader SkillsHeader { get; set; }
    }
}
