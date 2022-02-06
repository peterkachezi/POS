using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.BrandModule;
using POS.Data.Models;
using POS.Data.Services.BrandModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly IBrandService brandService;

        private readonly UserManager<AppUser> userManager;
        public BrandsController(UserManager<AppUser> userManager, IBrandService brandService)
        {
            this.brandService = brandService;

            this.userManager = userManager;
        }
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> GetBrands()
        {
            try
            {
                var landlords = (await brandService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> Create(BrandDTO brandDTO)
        {
            try
            {

                var isExist = (await brandService.GetAll()).Where(x => x.Name == brandDTO.Name).Count();

                if (isExist > 0)
                {
                    return Json(new { success = false, responseText = "The record already exists" });

                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                brandDTO.CreatedBy = user.Id;

                var result = await brandService.Create(brandDTO);

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
        public async Task<IActionResult> Update(BrandDTO brandDTO)
        {
            try
            {
                var results = await brandService.Update(brandDTO);

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
                var data = await brandService.GetById(Id);

                if (data != null)
                {
                    BrandDTO file = new BrandDTO
                    {
                        Id = data.Id,

                        Name = data.Name,
                       
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
                var results = await brandService.Delete(Id);

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
