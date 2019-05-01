using System.Threading.Tasks;
using VRegisterApp.API.DTO.Register;

namespace VRegisterApp.API.Services.Abstract
{
    public interface IVoiceService
    {
        Task<bool> RegisterUser(RegisterRequest registerRequest);
    }
}