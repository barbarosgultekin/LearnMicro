using Micro.Web.Models;

namespace Micro.Web.Services.Abstract
{
    public interface IAuthService
    {
        Task<T> Login<T>(UserDTO userDTO);
        Task<T> Register<T>(UserDTO userDTO);
    }
}
