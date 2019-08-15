using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.Models
{
    public class University
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
