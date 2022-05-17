using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.DTOs.ApplicationUsersModule
{
    public class UpdatePasswordDTO
    {
        public string CurrentPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
