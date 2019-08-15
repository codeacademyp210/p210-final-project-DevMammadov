using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.Models
{
    public class Comments
    {
        public int id { get; set; }
        public int PortfolioId { get; set; }
        public int SenderId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}
