using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string CreatedBy { get; set; }  
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
