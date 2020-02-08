using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class UserResponseModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RolesEnum Role { get; set; }
    }
}
