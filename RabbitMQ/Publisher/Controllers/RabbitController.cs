using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using SharedClass;
using System.Text;

namespace Publisher.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RabbitController : Controller
    {
        private readonly IRabbitMqService _rabbitMqService;

        public RabbitController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }
        [HttpPost]
        public IActionResult SendMessage()
        {
            using var connection = _rabbitMqService.CreateChannel();
            using var model = connection.CreateModel();
            model.QueueDeclare("test", true, false, false);
            var body = Encoding.UTF8.GetBytes("Hi mohammad zarrabi ");

            model.BasicPublish("", "test",body:body);

            return Ok();
        }
    }
}
