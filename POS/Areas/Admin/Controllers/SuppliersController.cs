using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.SupplerModule;
using POS.Data.Models;
using POS.Data.Services.CountyModule;
using POS.Data.Services.SupplerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService  supplierService;

        private readonly ICountyService  countyService;

        private readonly UserManager<AppUser> userManager;
        public SuppliersController(ICountyService countyService,UserManager<AppUser> userManager, ISupplierService supplierService)
        {
            this.supplierService = supplierService;

            this.userManager = userManager;

            this.countyService = countyService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.County = await countyService.GetAll();

            return View();
        }

        public async Task<IActionResult> GetSuppliers()
        {
            try
            {
                var landlords = (await supplierService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> Create(SupplerDTO supplerDTO)
        {
            try
            {
                var iSPackageExist = (await supplierService.GetAll()).Where(x => x.Email == supplerDTO.Email && x.PhoneNumber == supplerDTO.PhoneNumber).Count();

                if (iSPackageExist > 0)
                {
                    return Json(new { success = false, responseText = "The record already exists" });

                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                supplerDTO.CreatedBy = user.Id;

                var result = await supplierService.Create(supplerDTO);

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
        public async Task<IActionResult> Update(SupplerDTO supplerDTO)
        {
            try
            {
                var results = await supplierService.Update(supplerDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Record  has been  successfully updated!" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update the record!" });
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
                var data = await supplierService.GetById(Id);

                if (data != null)
                {
                    SupplerDTO file = new SupplerDTO
                    {
                        Id = data.Id,

                        FirstName = data.FirstName,

                        MiddleName = data.MiddleName,

                        LastName = data.LastName,

                        Email = data.Email,

                        PhoneNumber = data.PhoneNumber,

                        CountyId = data.CountyId,

                        CreateDate = DateTime.Now,

                        CreatedBy = data.CreatedBy,

                        SupplierCode = data.SupplierCode,

                        Town = data.Town,
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
                var results = await supplierService.Delete(Id);

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
