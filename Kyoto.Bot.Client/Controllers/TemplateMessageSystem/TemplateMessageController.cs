using System.ComponentModel.DataAnnotations;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Dto.TemplateMessageSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kyoto.Bot.Client.Controllers.TemplateMessageSystem;

[ApiController]
[Route("api/template-message")]
public class TemplateMessageController : ControllerBase
{
    private readonly ITemplateMessageService _templateMessageService;

    public TemplateMessageController(ITemplateMessageService templateMessageService)
    {
        _templateMessageService = templateMessageService;
    }

    [HttpGet]
    //[Authorize]
    public async Task<TemplateMessageDto> GetTemplateMessage([FromQuery, Required] TemplateMessageTypeValue type)
    {
        var templateMessage = await _templateMessageService.GetTemplateMessageAsync(type);
        return templateMessage.ToDto();
    }
    
    [HttpPatch]
    //[Authorize]
    public Task UpdateTemplateMessage(
        [FromQuery, Required] TemplateMessageTypeValue type, 
        [FromQuery, Required] string text)
    {
        return _templateMessageService.UpdateTemplateMessageAsync(type, text);
    }
}