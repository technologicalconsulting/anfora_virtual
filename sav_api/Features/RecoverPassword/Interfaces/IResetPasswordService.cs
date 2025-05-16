namespace sav_api.Features.RecoverPassword.Interfaces
{
    public interface IResetPasswordService
    {
        Task ResetPasswordAsync(string code, string newPassword);
    }
}
