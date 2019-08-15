using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.ViewModel
{
    public class LoginClass
    {
        [Required(ErrorMessage ="Email adresini yazınız")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifrəni yazınız")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
