using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.Areas.Admin.ViewModel
{
    public class LangVM
    {
        public Languages Language { get; set; }
        public List<Languages> Languages { get; set; }
        public string Action { get; set; }
        public string Button { get; set; }
    }
}
