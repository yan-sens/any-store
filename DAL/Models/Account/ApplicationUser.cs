using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace DAL.Models.Account
{
    public class ApplicationUser : IdentityUser, IDbEntity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
