using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class PortVM
    {
        public CoverClass CoverClass { get; set; }
        public List<Portfolio> Portfolio { get; set; }
        public List<PortfolioImages> PortfolioImages { get; set; }
    }
}
