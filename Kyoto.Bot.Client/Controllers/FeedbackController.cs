using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kyoto.Bot.Client.Controllers;

[ApiController]
[Route("api/feedback")]
public class FeedbackController : ControllerBase
{
    [HttpPost]
    [Authorize]
    public Task SetEnableStatus([FromQuery, Required] bool isEnable)
    {
        return Task.CompletedTask;
    }
    
    [Authorize]
    [HttpGet("list")]
    public Task<FeedbackDto> GetFeedbacks([FromQuery, Required] int offset, [FromQuery] int limit = 5)
    {
        return Task.FromResult(new FeedbackDto());
    }
    
    [Authorize]
    [HttpGet("rating")]
    public Task<RatingDto> GetRating()
    {
        return Task.FromResult(new RatingDto());
    }
}

public class FeedbackDto
{
    
}

public class RatingDto
{
    
}