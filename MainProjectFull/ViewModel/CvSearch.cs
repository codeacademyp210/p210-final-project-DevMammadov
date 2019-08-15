using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class CvSearch
    {
        public List<Position> Skills { get; set; }
        public List<Users> Users { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<Cv> Cvs { get; set; }
    }
}
