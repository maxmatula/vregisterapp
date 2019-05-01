using System.Threading.Tasks;
using VRegisterApp.API.DTO.Login;
using VRegisterApp.API.DTO.Register;

namespace VRegisterApp.API.Services.Abstract
{
    public interface IVoiceService
    {
        Task<bool> RegisterUser(RegisterRequest registerRequest);
        Task<bool> LoginUser(LoginRequest loginRequest);
        Task<string> GetUserContext(string email);
    }
}