using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class MainPageVM : BaseVM
    {
        public Users User { get; set; }
        public List<singleUserClass> Folows { get; set; }
        public CoverClass CoverClass { get; set; }
        public override Profile Profile { get => base.Profile; set => base.Profile = value; }
    }
}
