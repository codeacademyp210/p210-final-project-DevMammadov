using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class ImgBoxVM
    {
        public Portfolio Portfolio { get; set; }
        public Profile Profile { get; set; }
        public List<PortfolioImages> PortImages { get; set; }
        public List<Comments> Comments { get; set; }
        public Users CurrentUser { get; set; }
        public List<Users> Users { get; set; }
        public List<Profile> Profiles { get; set; }
        
    }
}
