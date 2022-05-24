
using Microsoft.AspNetCore.Identity;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.SeedAppUsers
{
    public static class SeedUsers
    {
        public static void seed(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            seedRoles(roleManager);

            seedUsers(userManager);
        }

        private static void seedRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    var role = new IdentityRole();

                    role.Name = "Admin";

                    roleManager.CreateAsync(role).Wait();
                }

                if (!roleManager.RoleExistsAsync("CyberAdmin").Result)
                {
                    var role = new IdentityRole();

                    role.Name = "CyberAdmin";

                    roleManager.CreateAsync(role).Wait();
                }

                if (!roleManager.RoleExistsAsync("User").Result)
                {
                    var role = new IdentityRole();

                    role.Name = "User";

                    roleManager.CreateAsync(role).Wait();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void seedUsers(UserManager<AppUser> userManager)
        {
            try
            {
                #region Admin
                var admin = userManager.FindByEmailAsync("admin@gmail.com");

                if (admin.Result == null)
                {
                    var user = new AppUser();

                    user.UserName = "admin@gmail.com";

                    user.Email = "admin@gmail.com";

                    user.PhoneNumber = "0704509484";

                    user.FirstName = "Alex";

                    user.LastName = "Jobs";

                    user.EmailConfirmed = true;

                    user.IsActive = true;

                    user.CreateDate = DateTime.Now;

                    string userPWD = "Admin@2022";

                    var chkUser = userManager.CreateAsync(user, userPWD);

                    //Add default User to Role Admin    
                    if (chkUser.Result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();

                    }
                }
                #endregion

                #region Cyber
                var cyber = userManager.FindByEmailAsync("cyber@gmail.com");

                if (cyber.Result == null)
                {
                    var user = new AppUser();

                    user.UserName = "cyber@gmail.com";

                    user.Email = "cyber@gmail.com";

                    user.PhoneNumber = "0704509484";

                    user.FirstName = "Skisoft";

                    user.LastName = "Cyber";

                    user.EmailConfirmed = true;

                    user.IsActive = true;

                    user.CreateDate = DateTime.Now;

                    string userPWD = "Cyber@2022";

                    var chkUser = userManager.CreateAsync(user, userPWD);

                    //Add default User to Role Admin    
                    if (chkUser.Result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "CyberAdmin").Wait();

                    }
                }
                #endregion

                #region User
                var cyberuser = userManager.FindByEmailAsync("user@gmail.com");

                if (cyberuser.Result == null)
                {
                    var user = new AppUser();

                    user.UserName = "user@gmail.com";

                    user.Email = "user@gmail.com";

                    user.PhoneNumber = "0704509484";

                    user.FirstName = "Peter";

                    user.LastName = "Kachezi";

                    user.EmailConfirmed = true;

                    user.IsActive = true;

                    user.CreateDate = DateTime.Now;

                    string userPWD = "User@2022";

                    var chkUser = userManager.CreateAsync(user, userPWD);

                    //Add default User to Role Admin    
                    if (chkUser.Result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();

                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

    }
}
