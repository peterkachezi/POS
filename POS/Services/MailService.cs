using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using POS.Data.DTOs.ApplicationUsersModule;
using POS.Data.DTOs.CustomerModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace POS.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration config;

        private readonly IWebHostEnvironment env;

        public MailService(IConfiguration config, IWebHostEnvironment env)
        {
            this.config = config;

            this.env = env;
        }
        public bool PasswordResetEmailNotification(ResetPasswordDTO resetPasswordDTO)
        {
            throw new NotImplementedException();
        }

        public bool SendAccountCreationEmailNotification(ApplicationUserDTO applicationUserDTO)
        {
            try
            {
                var SMTPEmailToNetwork = config.GetValue<string>("MailSettings:SMTPEmailToNetwork");

                var SMTPMailServer = config.GetValue<string>("MailSettings:SMTPMailServer");

                var SMTPPort = config.GetValue<string>("MailSettings:SMTPPort");

                var SMTPUserName = config.GetValue<string>("MailSettings:SMTPUserName");

                var Password = config.GetValue<string>("MailSettings:Password");

                var SMTPUseSSL = config.GetValue<string>("MailSettings:SMTPUseSSL");

                MailAddressCollection mailAddressesTo = new MailAddressCollection
                {
                    new MailAddress(applicationUserDTO.Email)
                };

                MailAddress mailAddressFrom = new MailAddress(SMTPUserName);

                MailMessage mailMessage = new MailMessage
                {
                    From = mailAddressFrom
                };

                foreach (var to in mailAddressesTo)
                    mailMessage.To.Add(to);


                mailMessage.Subject = "Healthier Kenya: ";

                var templatePath = env.WebRootPath
                           + Path.DirectorySeparatorChar.ToString()
                           + "Templates"
                           + Path.DirectorySeparatorChar.ToString()
                           + "EmailTemplate"
                           + Path.DirectorySeparatorChar.ToString()
                           + "UserAccountNotification.html";

                var builder = new BodyBuilder();

                using (StreamReader SourceReader = System.IO.File.OpenText(templatePath))
                {

                    builder.HtmlBody = SourceReader.ReadToEnd();

                }

                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                mailMessage.Body = string.Format(builder.HtmlBody,

                     applicationUserDTO.FullName,

                     applicationUserDTO.Email,

                     applicationUserDTO.Password,

                     applicationUserDTO.RoleName,

                     applicationUserDTO.RegionName

                    );

                mailMessage.IsBodyHtml = true;

                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = SMTPMailServer;
                    client.Port = int.Parse(SMTPPort);
                    if (SMTPUseSSL != string.Empty)
                    {
                        client.EnableSsl = bool.Parse(SMTPUseSSL);
                    }

                    client.UseDefaultCredentials = false;
                    bool bNetwork = bool.Parse(SMTPEmailToNetwork);
                    if (bNetwork)
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    }
                    else
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    }

                    client.Credentials = new NetworkCredential(SMTPUserName, Password);

                    client.ServicePoint.MaxIdleTime = 2;

                    client.ServicePoint.ConnectionLimit = 1;

                    client.Send(mailMessage);
                }

                return true;

            }


            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }


    }
}
