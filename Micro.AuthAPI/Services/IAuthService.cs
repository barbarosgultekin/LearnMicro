using Micro.AuthAPI.Models;

namespace Micro.AuthAPI.Services
{
    public interface IAuthService
    {
        Task<User> Register(UserDTO request);
        Task<ResponseDTO> Login(UserDTO request);
    }
}