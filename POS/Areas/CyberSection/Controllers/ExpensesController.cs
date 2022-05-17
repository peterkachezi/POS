using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.ExpenseModule;
using POS.Data.Models;
using POS.Data.Services.ExpenseModule;
using POS.Data.Services.ExpenseTypeModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class ExpensesController : Controller
    {

        private readonly IExpenseService expenseService;

        private readonly IExpenseTypeService expenseTypeService;

        private readonly IWebHostEnvironment env;

        private readonly UserManager<AppUser> userManager;
        public ExpensesController(IExpenseTypeService expenseTypeService, IWebHostEnvironment env, UserManager<AppUser> userManager, IExpenseService expenseService)
        {
            this.expenseTypeService = expenseTypeService;

            this.env = env;

            this.expenseService = expenseService;

            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.ExpenseTypes = await expenseTypeService.GetAll();

            return View();
        }
        public async Task<IActionResult> GetExpenses()
        {
            try
            {
                var landlords = (await expenseService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<IActionResult> Create(ExpenseDTO expenseDTO)
        {
            try
            {

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                expenseDTO.CreatedBy = user.Id;

                if (expenseDTO.ExpenseTypeId == null || expenseDTO.ExpenseTypeId == Guid.Empty)
                {
                    return Json(new { success = false, responseText = "Please select expense type" });

                }

                var results = await expenseService.Create(expenseDTO);

                if (results != null)
                {
                    return Json(new { success = true, responseText = "Record has been successfully saved " });
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to save record" });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return Json(new { success = false, responseText = "Something went wrong,please try again" });

            }
        }
        public async Task<IActionResult> Update(ExpenseDTO expenseDTO)
        {
            try
            {
                var results = await expenseService.Update(expenseDTO);

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
                var data = await expenseService.GetById(Id);

                if (data != null)
                {
                    ExpenseDTO file = new ExpenseDTO
                    {
                        Id = data.Id,

                        Amount = data.Amount,

                        ExpenseTypeId = data.ExpenseTypeId,

                        ExpenseTypeName = data.ExpenseTypeName,

                        Description = data.Description,

                        FileAttchmentName = data.FileAttchmentName,

                        CreateDate = data.CreateDate,

                        ExpenseDate = data.ExpenseDate,

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
                var results = await expenseService.Delete(Id);

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
