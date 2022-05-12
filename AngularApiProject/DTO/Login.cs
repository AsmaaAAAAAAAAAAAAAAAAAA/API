using System.ComponentModel.DataAnnotations;

namespace AngularApiProject.DTO
{
    public class Login
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
