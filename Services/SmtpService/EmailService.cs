using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.SmtpService
{
    public  class EmailService:IEmailService
    {
        #region Fields
        EmailSetting _emailSettings = null;
        #endregion

        #region constructor
        public EmailService(IOptions<EmailSetting> options)
        {
            _emailSettings = options.Value;
        }
        #endregion

        public async Task<bool> SendEmailAsync(EmailData sendingemaildata)
        {
            try
            {
                await Task.Yield();
                string userState = "mailto" + sendingemaildata.EmailId;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_emailSettings.EmailId);
                mail.To.Add(sendingemaildata.EmailId);
                mail.Subject = sendingemaildata.EmailSubject;
                mail.Body = sendingemaildata.EmailBody;
                mail.IsBodyHtml = sendingemaildata.IsHtml;
                var smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port);
                smtp.EnableSsl = _emailSettings.UseSSL;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_emailSettings.Name, _emailSettings.Password);
                // smtp.SendAsync(mail, userState);
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
