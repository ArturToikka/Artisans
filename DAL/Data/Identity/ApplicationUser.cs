using DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        public string ContactEmail { get; set; }
        public string ProfileName { get; set; }
        public string ContactPhoneNumber { get; set; }

        public string Address { get; set; }

        public IList<ProfilesSpheres> ProfilesSpheres { get; set; }
        public IList<ProfileOperation> ProfileOperation { get; set; }


    }
}
