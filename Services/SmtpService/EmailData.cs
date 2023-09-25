using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SmtpService
{
    public class EmailData
    {
        public string EmailTO { get; set; }
        public string EmailFROM { get; set; }
        public string EmailId { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public bool IsHtml { get; set; } = false;
        public int status { get; set; }
    }
}
