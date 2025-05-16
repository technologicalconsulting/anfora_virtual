namespace sav_api.Features.RecoverPassword.Interfaces
{
    public interface IResendEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
