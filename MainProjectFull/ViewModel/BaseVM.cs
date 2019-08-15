using MainProjectFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public abstract class BaseVM
    {
        public virtual Profile Profile { get; set; }
    }
}
