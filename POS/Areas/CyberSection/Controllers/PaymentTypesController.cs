using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.PaymentTypeModule;
using POS.Data.Models;
using POS.Data.Services.PaymentTypeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class PaymentTypesController : Controller
    {
        private readonly IPaymentTypeService paymentTypeService;

        private readonly UserManager<AppUser> userManager;
        public PaymentTypesController(UserManager<AppUser> userManager,IPaymentTypeService paymentTypeService)
        {
            this.paymentTypeService = paymentTypeService;

            this.userManager = userManager;
        }

   
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> GetExpenseTypes()
        {
            try
            {
                var landlords = (await paymentTypeService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> Create(PaymentTypeDTO paymentTypeDTO)
        {
            try
            {

                var isExist = (await paymentTypeService.GetAll()).Where(x => x.Name == paymentTypeDTO.Name).Count();

                if (isExist > 0)
                {
                    return Json(new { success = false, responseText = "The record already exists" });

                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                paymentTypeDTO.CreatedBy = user.Id;

                var result = await paymentTypeService.Create(paymentTypeDTO);

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
        public async Task<IActionResult> Update(PaymentTypeDTO paymentTypeDTO)
        {
            try
            {
                var results = await paymentTypeService.Update(paymentTypeDTO);

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
                var data = await paymentTypeService.GetById(Id);

                if (data != null)
                {
                    PaymentTypeDTO file = new PaymentTypeDTO
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
                var results = await paymentTypeService.Delete(Id);

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
