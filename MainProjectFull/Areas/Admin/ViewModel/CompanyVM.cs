using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.Areas.Admin.ViewModel
{
    public class CompanyVM
    {
        public Company Company { get; set; }
        public List<Company> Companies { get; set; }
        public string Action { get; set; }
        public string Button { get; set; }
    }
}
