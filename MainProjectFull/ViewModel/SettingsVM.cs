using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class SettingsVM
    {
        public Profile Profile { get; set; }
        public List<Users> Blocklist { get; set; }
        public Users User { get; set; }
    }
}
