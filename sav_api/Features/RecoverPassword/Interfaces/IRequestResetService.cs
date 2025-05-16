namespace sav_api.Features.RecoverPassword.Interfaces
{
    public interface IRequestResetService
    {
        Task RequestResetAsync(string email);
        
        

    }
}
