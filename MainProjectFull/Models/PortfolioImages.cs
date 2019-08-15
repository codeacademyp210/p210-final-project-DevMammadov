using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.Models
{
    public class PortfolioImages
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}
