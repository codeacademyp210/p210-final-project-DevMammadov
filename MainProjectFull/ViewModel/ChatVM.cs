using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class ChatVM
    {
        public Users Friend { get; set; }
        public Users CurrentUser { get; set; }
        public Profile CurrentProfile { get; set; }
        public Profile friendProfile { get; set; }
        public List<Messages> Messages { get; set; }
    }
}
