using System.Collections.Generic;

namespace MainProjectFull.Models
{
    public class Company
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public IList<Experience> Experience { get; set; }
    }

}
