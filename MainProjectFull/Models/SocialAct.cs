using System.ComponentModel.DataAnnotations;

namespace MainProjectFull.Models
{
    public class SocialAct
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Enter Company name")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Enter your position")]
        public string Position { get; set; }
        public string About { get; set; }
        public int UsersId { get; set; }
        public Users User { get; set; }
    }
}
