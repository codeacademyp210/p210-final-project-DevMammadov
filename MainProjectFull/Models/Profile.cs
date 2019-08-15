using System.Collections.Generic;

namespace MainProjectFull.Models
{
    public class Profile
    {
        public int id { get; set; }
        public string Photo { get; set; }
        public string Cover { get; set; }
        public int View { get; set; }
        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public bool MessageButton { get; set; }
        public int UsersId { get; set; }
        public string Type { get; set; }
        public string BanReason { get; set; }
        public bool Disabled { get; set; }
        public Users User { get; set; }
        public IList<Experience> Experience { get; set; }
        public IList<Cv> Cv { get; set; }
        public IList<SkillsHeader> SkillsHeader { get; set; }
        public IList<Portfolio> Portfolios { get; set; }
    }

}
