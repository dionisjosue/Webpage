using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyWebPage.Services
{
    public class EmailService:IEmailService
    {
        private ILogger<EmailService> _Loger;

        public EmailService(ILogger<EmailService> Loger)
        {
            _Loger = Loger;
        }
        public void SendEmail(string to, string from, string body)
        {
            _Loger.LogInformation("sending Email to: " + to + " from: "+from+ ' ' + body);
        }

    }
}
