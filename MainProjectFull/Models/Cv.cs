using System;

namespace MainProjectFull.Models
{
    public class Cv
    {
        public int id { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Photo { get; set; }
        public DateTime LastUpdated { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
