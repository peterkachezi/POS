
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using POS.Data.DTOs.ApplicationUsersModule;
using POS.Data.Models;
using POS.Data.Services.ApplicationUserServiceModule;
using POS.Data.Services.SMSModule;
using POS.Extensions;
using POS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PasswordOptions = POS.Extensions.PasswordOptions;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        private readonly SignInManager<AppUser> signInManager;

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IMessagingService messagingService;

        private readonly IMailService mailService;

        private readonly IApplicationUserService applicationUserService;


        public UserManagerController(IMailService mailService, IApplicationUserService applicationUserService, IMessagingService messagingService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,

        RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;

            this.signInManager = signInManager;

            this.roleManager = roleManager;

            this.messagingService = messagingService;

            this.applicationUserService = applicationUserService;

            this.mailService = mailService;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var loggedInUser = await userManager.FindByEmailAsync(User.Identity.Name);

                ViewBag.Roles = await roleManager.Roles.Where(x => x.Name != "Admin").ToListAsync();

                var users = applicationUserService.GetAll().Where(x => x.CreatedBy == loggedInUser.Id);

                return View(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ActionResult> Delete(string Id)
        {
            try
            {
                var results = await applicationUserService.Delete(Id);

                if (results == true)
                {

                    return Json(new { success = true, responseText = "User has been deleted successfully " });
                }
                else
                {
                    return Json(new { success = false, responseText = "Unable to delete User " });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> DeactivateAccount(string Id)
        {
            try
            {
                var results = await applicationUserService.DisableAccount(Id);

                if (results == true)
                {

                    return Json(new { success = true, responseText = "Account has been successfully deactivated  " });
                }
                else
                {
                    return Json(new { success = false, responseText = "Unable to disabled account " });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> ActivateAccount(string Id)
        {
            try
            {
                var results = await applicationUserService.EnableAccount(Id);

                if (results == true)
                {

                    return Json(new { success = true, responseText = "Account has been successfully activated  " });
                }
                else
                {
                    return Json(new { success = false, responseText = "Unable to activate account " });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(ApplicationUserDTO applicationUserDTO)
        {
            try
            {

                if (applicationUserDTO.RoleName == null)
                {
                    return Json(new { success = false, responseText = "Please select Role Name" });

                }

                var loggedInUser = await userManager.FindByEmailAsync(User.Identity.Name);

                applicationUserDTO.CreatedBy = loggedInUser.Id;

                string password = PasswordStore.GenerateRandomPassword(new PasswordOptions
                {
                    RequiredLength = 8,

                    RequireNonLetterOrDigit = true,

                    RequireDigit = true,

                    RequireLowercase = true,

                    RequireUppercase = true,

                    RequireNonAlphanumeric = true,

                    RequiredUniqueChars = 1
                });

                applicationUserDTO.Password = password;

                var user = new AppUser()
                {
                    UserName = applicationUserDTO.Email.ToLower(),

                    Email = applicationUserDTO.Email.ToLower(),

                    IsActive = true,

                    PhoneNumber = applicationUserDTO.PhoneNumber,

                    FirstName = applicationUserDTO.FirstName.Substring(0, 1).ToUpper() + applicationUserDTO.FirstName.Substring(1).ToLower().Trim(),

                    LastName = applicationUserDTO.LastName.Substring(0, 1).ToUpper() + applicationUserDTO.LastName.Substring(1).ToLower().Trim(),

                    CreateDate = DateTime.Now,

                    CreatedBy = applicationUserDTO.CreatedBy,

                };


                var result = await userManager.CreateAsync(user, applicationUserDTO.Password);

                if (result.Succeeded == false)
                {
                    var error = result.Errors.FirstOrDefault();

                    return Json(new { success = false, responseText = error.Description });
                }

                if (result.Succeeded)
                {
                    // var sms = messagingService.AccointDetailsSMS(applicationUserDTO);

                    var sendmail = mailService.SendAccountCreationEmailNotification(applicationUserDTO);

                    var createRole = await userManager.AddToRoleAsync(user, applicationUserDTO.RoleName);

                    return Json(new { success = true, responseText = "Account has been created successfully" });
                }

                foreach (var error in result.Errors)
                {
                    return Json(new { success = false, responseText = "Unable to update record report details" });

                }

                return View();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
        public IActionResult GetUserById(string Id)
        {
            var data = applicationUserService.GetById(Id);

            if (data != null)
            {
                ApplicationUserDTO file = new ApplicationUserDTO
                {
                    Id = data.Id,

                    Email = data.Email,

                    FirstName = data.FirstName,

                    LastName = data.LastName,

                    PhoneNumber = data.PhoneNumber,

                    CreateDate = data.CreateDate,

                    IsActive = data.IsActive,

                    CreatedByName = data.CreatedByName,

                    RoleName = data.RoleName,

                };

                return Json(new { data = file });
            }
            else
            {
                return Json(new { data = false });
            }
        }
        public async Task<IActionResult> Update(ApplicationUserDTO applicationUserDTO)
        {
            try
            {
                var results = await applicationUserService.Update(applicationUserDTO);

                if (results != null)
                {
                    return Json(new { success = true, responseText = "Information has been updated successfully " });
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to update details" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public IActionResult ViewDetails(string Id)
        {
            try
            {
                ViewBag.GetArms = applicationUserService.GetAll().Where(x => x.RoleName == "ARM");

                var data = applicationUserService.GetById(Id);

                return View(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public IActionResult UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO updatePasswordDTO)
        {
            try
            {

                if (updatePasswordDTO.CurrentPassword == null)
                {
                    return Json(new { success = false, responseText = "Current password is a required field" });

                }

                if (updatePasswordDTO.NewPassword == null)
                {
                    return Json(new { success = false, responseText = "New password is a required field" });

                }

                if (updatePasswordDTO.ConfirmPassword == null)
                {
                    return Json(new { success = false, responseText = "Confirm password is a required field" });

                }

                if (updatePasswordDTO.ConfirmPassword != updatePasswordDTO.NewPassword)
                {
                    return Json(new { success = false, responseText = "Password and confirm password do not match" });

                }


                if (ModelState.IsValid)
                {
                    var user = await userManager.GetUserAsync(User);

                    if (user == null)
                    {
                        return RedirectToAction("/Account/Logout");
                    }

                    var result = await userManager.ChangePasswordAsync(user, updatePasswordDTO.CurrentPassword, updatePasswordDTO.NewPassword);

                    if (!result.Succeeded)
                    {

                        var validation = (result.Errors.FirstOrDefault().Description);

                        return Json(new { success = false, responseText = validation });

                    }

                    await signInManager.RefreshSignInAsync(user);

                    return Json(new { success = true, responseText = "Password has been changed successfully" });

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

            return View(updatePasswordDTO);
        }

    }
}
