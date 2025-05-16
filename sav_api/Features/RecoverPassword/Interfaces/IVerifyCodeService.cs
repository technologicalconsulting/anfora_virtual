namespace sav_api.Features.RecoverPassword.Interfaces
{
    public interface IVerifyCodeService
    {
        Task VerifyCodeAsync(string code);
    }
}
