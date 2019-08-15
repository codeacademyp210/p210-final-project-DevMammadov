using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class CoverClass
    {
        public Users User { get; set; }
        public Profile Profile { get; set; }
        public int folowers { get; set; }
        public int folowings { get; set; }
        public bool isFolowing { get; set; }
        public int views { get; set; }
    }
}
