using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class ExperienceVM : BaseVM
    {
        public List<Experience> Experiences { get; set; }
        public CoverClass Cover { get; set; }
        public List<Company> Companies { get; set; }
        public override Profile Profile { get => base.Profile; set => base.Profile = value; }
    }
}
