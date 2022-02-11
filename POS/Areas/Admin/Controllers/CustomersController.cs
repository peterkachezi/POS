using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.CustomerModule;
using POS.Data.Models;
using POS.Data.Services.CountyModule;
using POS.Data.Services.CustomerModule;
using POS.Data.Services.SMSModule;
using POS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        private readonly ICustomersService customerService;


        private readonly IMessagingService  messagingService;

        private readonly IMailService mailService;

        private readonly UserManager<AppUser> userManager;

        public CustomersController(UserManager<AppUser> userManager,IMailService mailService, ICustomersService customerService, IMessagingService messagingService)

        {
            this.userManager = userManager;

            this.customerService = customerService;

            this.messagingService = messagingService;

            this.mailService = mailService;

        }
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customer = await customerService.GetAll();

                return Json(new { data = customer });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }


        public async Task<ActionResult> Create(CustomerDTO customerDTO)
        {
            try
            {
                var IsCustomerExist = (await customerService.GetAll()).Where(x => x.Email == customerDTO.Email && x.PhoneNumber == customerDTO.PhoneNumber).Count();

                if (IsCustomerExist > 0)
                {
                    return Json(new { success = false, responseText = "The customer already exists" });

                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                customerDTO.CreatedBy = user.Id;

                var result = await customerService.Create(customerDTO);

                if (result != null)
                {

                    var sendMail = mailService.SendEmailNotification(customerDTO);

                    //var s = iSMSService.SendCustomerSMS(customerDTO);

                    return Json(new { success = true, responseText = "Customer has been successfully saved" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Customer has been  not been saved" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> GetById(Guid Id)
        {
            try
            {
                var data = await customerService.GetById(Id);

                if (data != null)
                {
                    CustomerDTO file = new CustomerDTO()
                    {
                        Id = data.Id,

                        FirstName = data.FirstName,

                        MiddleName = data.MiddleName,

                        LastName = data.LastName,

                        Email = data.Email,

                        PhoneNumber = data.PhoneNumber,

                        CreatedBy = data.CreatedBy,

                        CreateDate = data.CreateDate,

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
        public async Task<ActionResult> Delete(Guid Id)
        {
            try
            {
                var results = await customerService.Delete(Id);

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

        public async Task<ActionResult> Update(CustomerDTO customerDTO)
        {
            try
            {
                var results = await customerService.Update(customerDTO);

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
    }
}
