using Microsoft.EntityFrameworkCore;
using POS.Data.DTOs.CustomerModule;
using POS.Data.Models;
using POS.Data.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace POS.Data.Services.CustomerModule
{
    public class CustomersService : ICustomersService
    {
        private ApplicationDbContext context;

        public CustomersService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<CustomerDTO> Create(CustomerDTO customerDTO)
        {
            try
            {

                string customer_number = CustomerNumber.GenerateUniqueNumber();

                customerDTO.CustomerNumber = "CUS" + "" + customer_number;

                var c = new Customer
                {
                    Id = Guid.NewGuid(),

                    FirstName = customerDTO.FirstName.Substring(0, 1).ToUpper() + customerDTO.FirstName.Substring(1).ToLower().Trim(),

                    MiddleName = customerDTO.MiddleName.Substring(0, 1).ToUpper() + customerDTO.MiddleName.Substring(1).ToLower().Trim(),

                    LastName = customerDTO.LastName.Substring(0, 1).ToUpper() + customerDTO.LastName.Substring(1).ToLower().Trim(),

                    Email = customerDTO.Email.ToLower().Trim(),

                    PhoneNumber = customerDTO.PhoneNumber,

                    CustomerNumber = customerDTO.CustomerNumber,

                    CreatedBy = customerDTO.CreatedBy,

                    CreateDate = DateTime.Now,

                    Apartment = customerDTO.Apartment,

                    DeliveryLocation = customerDTO.DeliveryLocation,
                };

                context.Customers.Add(c);

                await context.SaveChangesAsync();

                return customerDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool result = false;

                var s = await context.Customers.FindAsync(Id);

                if (s != null)
                {
                    context.Customers.Remove(s);

                    await context.SaveChangesAsync();

                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<CustomerDTO> Update(CustomerDTO customerDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Customers.FindAsync(customerDTO.Id);
                    {
                        s.FirstName = customerDTO.FirstName.Substring(0, 1).ToUpper() + customerDTO.FirstName.Substring(1).ToLower().Trim();

                        s.MiddleName = customerDTO.MiddleName.Substring(0, 1).ToUpper() + customerDTO.MiddleName.Substring(1).ToLower().Trim();

                        s.LastName = customerDTO.LastName.Substring(0, 1).ToUpper() + customerDTO.LastName.Substring(1).ToLower().Trim();

                        s.Email = customerDTO.Email.ToLower().Trim();

                        s.PhoneNumber = customerDTO.PhoneNumber;

                    }
                    transaction.Commit();

                    await context.SaveChangesAsync();

                 
                }

                return customerDTO;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return null;
            }
        }


        public async Task<List<CustomerDTO>> GetAll()
        {
            try
            {
                var customers = (from c in context.Customers
                                 
                                 join u in context.AppUsers on c.CreatedBy equals u.Id
                                 
                                 select new CustomerDTO
                                 {
                                     Id = c.Id,

                                     FirstName = c.FirstName,

                                     MiddleName = c.MiddleName,

                                     LastName = c.LastName,

                                     Email = c.Email,

                                     PhoneNumber = c.PhoneNumber,

                                     CustomerNumber = c.CustomerNumber,

                                     CreatedBy = c.CreatedBy == null ? "" : c.CreatedBy,

                                     CreatedByFirstName = u.FirstName == null ? "" : c.LastName,

                                     CreateDate = c.CreateDate

                                 }).OrderByDescending(x=>x.CreateDate).ToListAsync();

                return await customers;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<CustomerDTO> GetById(Guid Id)
        {
            try
            {
                var customer = await context.Customers.FindAsync(Id);

                return new CustomerDTO
                {
                    Id = customer.Id,

                    FirstName = customer.FirstName,

                    MiddleName = customer.MiddleName,

                    LastName = customer.LastName,

                    Email = customer.Email,

                    PhoneNumber = customer.PhoneNumber,

                    CustomerNumber = customer.CustomerNumber,

                    CreatedBy = customer.CreatedBy == null ? "" : customer.CreatedBy,

                    //CreatedByFirstName = customer.AspNetUser.FirstName == null ? "" : customer.AspNetUser.FirstName,

                    //CreatedByLastName = customer.AspNetUser.LastName == null ? "" : customer.AspNetUser.LastName,

                    CreateDate = customer.CreateDate
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
