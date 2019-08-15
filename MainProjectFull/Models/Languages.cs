using System.Collections.Generic;

namespace MainProjectFull.Models
{
    public class Languages
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public IList<UsersLanguages> UsersLanguages { get; set; }
    }
}
