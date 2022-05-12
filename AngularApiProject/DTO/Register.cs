using AngularApiProject.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularApiProject.DTO
{
    public class Register
    {
        [Required]
        public string userName { set; get; }
        [Required]
        public string email { set; get; }
        [Required]
        public string password { set; get; }
    }
}
