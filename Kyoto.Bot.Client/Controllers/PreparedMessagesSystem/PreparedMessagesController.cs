using System.ComponentModel.DataAnnotations;
using Kyoto.Domain.PreparedMessagesSystem;
using Kyoto.Dto.PreparedMessageSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kyoto.Bot.Client.Controllers.PreparedMessagesSystem;

[ApiController]
[Route("api/prepared-message")]
public class PreparedMessagesController : ControllerBase
{
    private readonly IPreparedMessagesRepository _messagesRepository;

    public PreparedMessagesController(IPreparedMessagesRepository messagesRepository)
    {
        _messagesRepository = messagesRepository;
    }

    [HttpPost]
    //[Authorize]
    public Task AddNewsLetter([FromBody, Required] PreparedMessageDto preparedMessageDto)
    {
        return _messagesRepository.AddNewsletterAsync(PreparedMessage.Create(
            preparedMessageDto.PostEventCode, 
            preparedMessageDto.Text,
            preparedMessageDto.TimeToSend, 
            preparedMessageDto.KeyWords));
    }
    
    [HttpGet]
    //[Authorize]
    public Task<PreparedMessageDto> GetNewsLetters([FromQuery, Required] int offset, [FromQuery] int limit = 5)
    {
        return Task.FromResult<PreparedMessageDto>(null);
    }
    
    [HttpDelete]
    //[Authorize]
    public Task RemoveNewsLetter([FromBody, Required] Guid id)
    {
        return Task.CompletedTask;
    }
}