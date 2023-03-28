using Micro.MessageBus;
using Micro.Web.Models;
using Micro.Web.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IRabbitMQMessageBus _rabbitmqMessageBus;
        public AuthController(IAuthService authService, IRabbitMQMessageBus rabbitMQMessageBus)
        {
            _authService = authService;
            _rabbitmqMessageBus = rabbitMQMessageBus;
        }
        public async Task<IActionResult> Login([FromBody]UserDTO userDTO)
        {
            List<UserDTO> list = new();
          
            var response = await _authService.Login<ResponseDTO>(userDTO);

            if (response != null && response.IsSuccess)
            {
                var token = Convert.ToString(response.Result);
                Response.Cookies.Append("AccessToken", token , new CookieOptions
                { 
                    Secure=true,
                    HttpOnly=true,
                    SameSite = SameSiteMode.Strict
                });            
            }
             
            RabbitMQ(response);

            
            return View();
        }

        public void RabbitMQ(ResponseDTO response)
        {
            var accessToken = Request.Cookies["AccessToken"];

            BaseMessage message = new BaseMessage
            {
                Id = 1,
                MessageCreated = DateTime.Now,
                token=accessToken
            };       

            _rabbitmqMessageBus.PublishMessage(message, "checkoutqueue");            
        }
    }
}
