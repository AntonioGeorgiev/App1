using App1.BL.Interfaces;
using App1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App1.Controllers
{
    [ApiController]
    [Route("controller")]
    public class PersonController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMq;

        public PersonController(IRabbitMqService rabbitMq)
        {
            _rabbitMq = rabbitMq;
        }

        [HttpPost("Publish Person to RabbitMQ")]
        public async Task<IActionResult> ProducePerson([FromBody] Person person)
        {
            await _rabbitMq.PublishPersonAsync(person);

            return Ok();
        }
    }
}