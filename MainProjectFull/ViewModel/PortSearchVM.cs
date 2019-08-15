using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class PortSearchVM
    {
        public List<Users> Users { get; set; }
        public List<Portfolio> Portfolios { get; set; }
        public List<PortfolioImages> PortfolioImages { get; set; }
        public List<Profile> Profiles { get; set; }
        public Users CurrentUser { get; set; }
    }
}
