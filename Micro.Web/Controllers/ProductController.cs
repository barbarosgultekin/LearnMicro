using Micro.MessageBus;
using Micro.Web.Models;
using Micro.Web.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Micro.Web.Controllers
{
    public class ProductController : Controller
    {
        private const string ProductQueueName = "checkoutqueue";

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDTO> list = new();

            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://admin:123456@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();


            channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);


            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                BaseMessage baseMessage = JsonConvert.DeserializeObject<BaseMessage>(message);
                BaseMessage baseMessage1 = new()
                {
                    Id = baseMessage.Id,
                    MessageCreated = baseMessage.MessageCreated,
                    token = baseMessage.token
                };
                channel.BasicAck(e.DeliveryTag, false);
            };

            channel.BasicConsume("checkoutqueue", false, consumer);


            var response = await _productService.GetAllProductsAsync<ResponseDTO>("");

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
