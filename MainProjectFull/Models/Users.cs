using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.Models
{
    public class Users : IdentityUser<int>
    {
        public override int Id { get; set; }
        //public override int Id { get; set; }
        [Required(ErrorMessage = "Please ender name")]
        [MaxLength(60, ErrorMessage = "Name must be less than 60 character")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please ender surname")]
        [MaxLength(60, ErrorMessage = "Name must be less than 60 character")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please ender email address")]
        [EmailAddress(ErrorMessage = "This email adress invalid")]
        public override string Email { get; set; }

        public override string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please ender password")]
        public override string PasswordHash { get; set; }

        public bool Gender { get; set; }
        public DateTime Birth { get; set; }
        public string Profession { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public string Linkedin { get; set; }
        public string Github { get; set; }
        public string Behance { get; set; }
        public string Facebook { get; set; }
        public string Hobbi { get; set; }
        public string Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public IList<SocialAct> SocialAct { get; set; }
        public IList<UsersLanguages> UsersLanguages { get; set; }
        public IList<Notify> Notify { get; set; }
        public IList<Profile> Profile { get; set; }
        public IList<University> University { get; set; }
    }
}
