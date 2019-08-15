using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.Models
{
    public class Blocklist
    {
        public int id { get; set; }
        public int blockerId { get; set; }
        public int blockedId { get; set; }
    }
}
