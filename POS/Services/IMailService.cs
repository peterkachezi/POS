using POS.Data.DTOs.ApplicationUsersModule;
using POS.Data.DTOs.CustomerModule;

namespace POS.Services
{
    public interface IMailService
    {
        bool SendAccountCreationEmailNotification(ApplicationUserDTO applicationUserDTO);
        bool PasswordResetEmailNotification(ResetPasswordDTO resetPasswordDTO);
    }
}