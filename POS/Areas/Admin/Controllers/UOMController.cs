using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.UnitOfMeasureModule;
using POS.Data.Models;
using POS.Data.Services.UnitOfMeasureModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UOMController : Controller
    {
        private readonly IUOMService  iUOMService;

        private readonly UserManager<AppUser> userManager;
        public UOMController(UserManager<AppUser> userManager,IUOMService iUOMService)
        {
            this.iUOMService = iUOMService;

            this.userManager = userManager;
        }
        public IActionResult Index()
        {          

            return View();
        }

        public async Task<IActionResult> GetUOMs()
        {
            try
            {
                var landlords = (await iUOMService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> Create(UOMDTO  uOMDTO)
        {
            try
            {

                var iSPackageExist = (await iUOMService.GetAll()).Where(x => x.Name == uOMDTO.Name && x.Unit == uOMDTO.Unit).Count();

                if (iSPackageExist > 0)
                {
                    return Json(new { success = false, responseText = "The record already exists" });

                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                uOMDTO.CreatedBy = user.Id;

                var result = await iUOMService.Create(uOMDTO);

                if (result != null)
                {

                    return Json(new { success = true, responseText = "Record has been successfully saved" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Unable to save record" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<IActionResult> Update(UOMDTO uOMDTO)
        {
            try
            {
                var results = await iUOMService.Update(uOMDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Product  has been  successfully updated!" });
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
                var data = await iUOMService.GetById(Id);

                if (data != null)
                {
                    UOMDTO file = new UOMDTO
                    {
                        Id = data.Id,

                        Name = data.Name,

                        Unit = data.Unit,

                        CreateDate = data.CreateDate,

                        CreatedBy = data.CreatedBy
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
                var results = await iUOMService.Delete(Id);

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
