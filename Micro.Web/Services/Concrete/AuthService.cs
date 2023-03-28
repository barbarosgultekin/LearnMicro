using Micro.Web.Models;
using Micro.Web.Services.Abstract;

namespace Micro.Web.Services.Concrete
{
    public class AuthService : BaseService,IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;

        public AuthService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> Login<T>(UserDTO userDTO)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = userDTO,
                Url = SD.AuthAPIBase + "/api/auth/login",
                AccessToken = ""
            });
        }

        public async Task<T> Register<T>(UserDTO userDTO)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = userDTO,
                Url = SD.AuthAPIBase + "/api/auth/register",
                AccessToken = ""
            });
        }
    }
}
