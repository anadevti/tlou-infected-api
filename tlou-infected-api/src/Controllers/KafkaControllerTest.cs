using Microsoft.AspNetCore.Mvc;
using tlou_infected_api.Application.Services;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KafkaControllerTest : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostKafka(string topic, string message)
    {
        await new KafkaProducerTestService().SendMessageAsync(topic, message);
        return Ok("Message sent!");
    }
}