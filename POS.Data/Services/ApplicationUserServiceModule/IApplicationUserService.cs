using POS.Data.DTOs.ApplicationUsersModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.ApplicationUserServiceModule
{
    public interface IApplicationUserService
    {
        List<ApplicationUserDTO> GetAll();
        ApplicationUserDTO GetById(string Id);
        ApplicationUserDTO GetByEmail(string Email);
        Task<bool> Delete(string Id);
        Task<bool> DisableAccount(string Id);
        Task<bool> EnableAccount(string Id);
        Task<ApplicationUserDTO> Update(ApplicationUserDTO applicationUserDTO);
        Task<ApplicationUserDTO> UnlockAccount(ApplicationUserDTO applicationUserDTO);

    }
}


