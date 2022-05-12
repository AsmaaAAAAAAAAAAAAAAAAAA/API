using AngularApiProject.Helper;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AngularApiProject.Model
{
    public class ApplicationUser : IdentityUser
    {
        public CustomAddress Address { get; set; } = new CustomAddress();
        public List<CustomMobile> Mobiles { get; set; } = new List<CustomMobile>();
    }
    
}
