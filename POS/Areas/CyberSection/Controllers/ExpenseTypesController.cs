using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.ExpenseTypeModule;
using POS.Data.Models;
using POS.Data.Services.ExpenseTypeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class ExpenseTypesController : Controller
    {
        private readonly IExpenseTypeService  expenseTypeService;

        private readonly UserManager<AppUser> userManager;
        public ExpenseTypesController(UserManager<AppUser> userManager, IExpenseTypeService expenseTypeService)
        {
            this.expenseTypeService = expenseTypeService;

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
                var landlords = (await expenseTypeService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> Create(ExpenseTypeDTO expenseTypeDTO)
        {
            try
            {

                var isExist = (await expenseTypeService.GetAll()).Where(x => x.Name == expenseTypeDTO.Name).Count();

                if (isExist > 0)
                {
                    return Json(new { success = false, responseText = "The record already exists" });

                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                expenseTypeDTO.CreatedBy = user.Id;

                var result = await expenseTypeService.Create(expenseTypeDTO);

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
        public async Task<IActionResult> Update(ExpenseTypeDTO expenseTypeDTO)
        {
            try
            {
                var results = await expenseTypeService.Update(expenseTypeDTO);

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
                var data = await expenseTypeService.GetById(Id);

                if (data != null)
                {
                    ExpenseTypeDTO file = new ExpenseTypeDTO
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
                var results = await expenseTypeService.Delete(Id);

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
