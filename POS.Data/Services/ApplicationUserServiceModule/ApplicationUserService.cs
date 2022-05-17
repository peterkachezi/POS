using POS.Data.DTOs.ApplicationUsersModule;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace POS.Data.Services.ApplicationUserServiceModule
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(string Id)
        {
            try
            {
                bool result = false;

                var user = await context.AppUsers.FindAsync(Id);

                if (user != null)
                {
                    context.Remove(user);

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
        public async Task<bool> DisableAccount(string Id)
        {
            try
            {
                bool result = false;

                var user = await context.AppUsers.FindAsync(Id);

                if (user != null)
                {

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        user.IsActive = false;

                        transaction.Commit();

                        await context.SaveChangesAsync();
                    }
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
        public async Task<bool> EnableAccount(string Id)
        {
            try
            {
                bool result = false;

                var user = await context.AppUsers.FindAsync(Id);

                if (user != null)
                {

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        user.IsActive = true;

                        transaction.Commit();

                        await context.SaveChangesAsync();
                    }
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

        public List<ApplicationUserDTO> GetAll()
        {
            try
            {
                var users = (from u in context.AppUsers                                                      

                             join userrol in context.UserRoles on u.Id equals userrol.UserId

                             join role in context.Roles on userrol.RoleId equals role.Id

                             join creater in context.AppUsers on u.CreatedBy equals creater.Id

                             select new ApplicationUserDTO
                             {
                                 Id = u.Id,

                                 Email = u.Email,

                                 FirstName = u.FirstName,

                                 LastName = u.LastName,

                                 PhoneNumber = u.PhoneNumber,

                                 CreateDate = u.CreateDate,

                                 IsActive = u.IsActive,

                                 CreatedBy = u.CreatedBy,

                                 RoleName = role.Name,

                                 CreatedByName = creater.FirstName + " " + creater.LastName,

                             }).ToList();

                return users;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public ApplicationUserDTO GetByEmail(string Email)
        {
            throw new NotImplementedException();
        }

        public ApplicationUserDTO GetById(string Id)
        {
            try
            {
                var users = (from u in context.AppUsers

                             join userrol in context.UserRoles on u.Id equals userrol.UserId

                             join role in context.Roles on userrol.RoleId equals role.Id

                             join creater in context.AppUsers on u.CreatedBy equals creater.Id

                             where u.Id==Id

                             select new ApplicationUserDTO
                             {
                                 Id = u.Id,

                                 Email = u.Email,

                                 FirstName = u.FirstName,

                                 LastName = u.LastName,

                                 PhoneNumber = u.PhoneNumber,

                                 CreateDate = u.CreateDate,

                                 IsActive = u.IsActive,

                                 CreatedBy = u.CreatedBy,

                                 RoleName = role.Name,

                                 CreatedByName = creater.FirstName + " " + creater.LastName,

                             }).FirstOrDefault();

                return users;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ApplicationUserDTO> Update(ApplicationUserDTO applicationUserDTO)
        {
            try
            {
                var user = await context.AppUsers.FindAsync(applicationUserDTO.Id);

                if (user != null)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        user.FirstName = applicationUserDTO.FirstName.Substring(0, 1).ToUpper() + applicationUserDTO.FirstName.Substring(1).ToLower().Trim();

                        user.LastName = applicationUserDTO.LastName.Substring(0, 1).ToUpper() + applicationUserDTO.LastName.Substring(1).ToLower().Trim();

                        user.PhoneNumber = applicationUserDTO.PhoneNumber;

                        user.Email = applicationUserDTO.Email.ToLower();

                        user.UserName = applicationUserDTO.Email.ToLower();

                        user.NormalizedEmail = applicationUserDTO.Email.ToUpper();

                        user.NormalizedUserName = applicationUserDTO.Email.ToUpper();

                        transaction.Commit();

                        await context.SaveChangesAsync();
                    }
                    return applicationUserDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ApplicationUserDTO> UnlockAccount(ApplicationUserDTO applicationUserDTO)
        {
            try
            {
                var user = await context.AppUsers.FindAsync(applicationUserDTO.Id);

                if (user != null)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        user.LockoutEnd = null;

                        transaction.Commit();

                        await context.SaveChangesAsync();
                    }
                    return applicationUserDTO;
                }
                else
                {
                    return null;
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