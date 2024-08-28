using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebMessagingService.API.Hubs;
using WebMessagingService.API.Models;
using WebMessagingService.API.Repositories;

namespace WebMessagingService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageRepository _repository;
    private readonly IHubContext<MessageHub> _hubContext;

    public MessagesController(IMessageRepository repository, IHubContext<MessageHub> hubContext)
    {
        _repository = repository;
        _hubContext = hubContext;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] Message message)
    {
        message.Timestamp = DateTime.UtcNow;
        _repository.AddMessage(message);
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        return Ok();
    }

    [HttpGet("history")]
    public IActionResult GetMessages([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        var messages = _repository.GetMessages(from, to);
        if (!messages.Any())
        {
            return NotFound("Сообщения не найдены.");
        }
        return Ok(messages);
    }
}