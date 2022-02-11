
using POS.Data.DTOs.ApplicationUsersModule;
using System.Threading.Tasks;

namespace POS.Data.Services.SMSModule
{
    public interface IMessagingService
    {
        Task<RegisterDTO> usersAccount(RegisterDTO registerDTO);

    }
}