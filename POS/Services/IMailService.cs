using POS.Data.DTOs.CustomerModule;

namespace POS.Services
{
    public interface IMailService
    {
        bool SendEmailNotification(CustomerDTO customerDTO);
       // bool SendAccountConfirmationEmail(UserDTO userDTO);
    }
}