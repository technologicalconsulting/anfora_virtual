using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using SendGrid;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Resend;
using sav_api.Features.RecoverPassword.Interfaces;

namespace sav_api.Features.RecoverPassword.Services
{
    public class ResendEmailService: IResendEmailService
    {
        private readonly IResend _resend;
        

        public ResendEmailService(IResend resend)
        {
            _resend = resend;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new EmailMessage();
            message.From = "onboarding@resend.dev";
            message.To.Add(toEmail);
            message.Subject = subject;
            message.HtmlBody = body;


            await _resend.EmailSendAsync(message);            
        }
    }
    
}
