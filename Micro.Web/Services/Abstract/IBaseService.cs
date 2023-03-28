using Micro.Web.Models;

namespace Micro.Web.Services.Abstract
{
    public interface IBaseService:IDisposable
    {
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
