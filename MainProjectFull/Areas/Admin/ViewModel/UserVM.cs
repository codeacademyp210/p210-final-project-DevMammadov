using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.Areas.Admin.ViewModel
{
    public class UserVM
    {
        public List<Users> Users { get; set; }
        public List<Profile> Profiles { get; set; }
    }
}
