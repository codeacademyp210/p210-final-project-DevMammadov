using System;

namespace MainProjectFull.Models
{
    public class Experience
    {
        public int id { get; set; }
        public string Position { get; set; }
        public string About { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CompanyId { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public Company Company { get; set; }
    }

}
