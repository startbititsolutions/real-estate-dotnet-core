using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SmtpService
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailData sendingemaildata);
    }
}
