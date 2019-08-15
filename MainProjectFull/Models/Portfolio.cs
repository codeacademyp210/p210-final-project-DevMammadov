using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.Models
{
    public class Portfolio
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public int View { get; set; }
        public string Type { get; set; }
        public string Github { get; set; }
        public string Behance { get; set; }
        public string Website { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public List<PortfolioImages> PortfolioImages { get; set; }
        public List<Comments> Comments { get; set; }
    }
}
