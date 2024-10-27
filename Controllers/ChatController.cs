using Microsoft.AspNetCore.Mvc;
using SalesIA.DTO;
using SalesIA.Services;

namespace SalesIA.Controllers;
[Route("api/chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly ChatService _service;

    public ChatController(ChatService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(MessageDTO request)
    {
        var response = await _service.GetResponse(request.Message);

        return !string.IsNullOrWhiteSpace(response)
            ? Ok(response)
            : NoContent();
    }
}
 