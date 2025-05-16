using sav_api.Features.Login.Dtos;

namespace sav_api.Features.Login.Interfaces
{
    public interface ILoginService
    {
        Task LoginAsync(LoginDto loginDto);
    }
}
