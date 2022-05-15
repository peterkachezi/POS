using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.CyberServiceModule;
using POS.Data.Models;
using POS.Data.Services.CyberServiceModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class ServicesController : Controller
    {
        private readonly ICyber_Service cyber_Service;

        private readonly UserManager<AppUser> userManager;
        public ServicesController(UserManager<AppUser> userManager, ICyber_Service cyber_Service)
        {

            this.cyber_Service = cyber_Service;

            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetCyberServices()
        {
            try
            {
                var landlords = (await cyber_Service.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<IActionResult> Create(CyberServiceDTO cyberServiceDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                cyberServiceDTO.CreatedBy = user.Id;

                var results =await cyber_Service.Create(cyberServiceDTO);

                if (results != null)
                {
                    return Json(new { success = true, responseText = "Record has been saved successfully " });
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to upload file" });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return Json(new { success = false, responseText = "Something went wrong,please try again" });

            }
        }
        public async Task<IActionResult> Update(CyberServiceDTO cyberServiceDTO)
        {
            try
            {
                var results = await cyber_Service.Update(cyberServiceDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Record  has been  successfully updated!" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update the record  !" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var data = await cyber_Service.GetById(Id);

                if (data != null)
                {
                    CyberServiceDTO file = new CyberServiceDTO
                    {
                        Id = data.Id,

                        Name = data.Name,

                        Amount = data.Amount,

                        CreatedBy = data.CreatedBy,

                        CreatedByName = data.CreatedByName,
                    };

                    return Json(new { data = file });
                }

                return Json(new { data = false });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var results = await cyber_Service.Delete(Id);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record  has been  successfully Deleted!" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to Delete because the file is in use with other records!" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
