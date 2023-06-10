using System.ComponentModel.DataAnnotations;
using Kyoto.Domain.FeedbackSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kyoto.Bot.Client.Controllers;

[ApiController]
[Route("api/feedback")]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackController(IFeedbackService feedbackService, IFeedbackRepository feedbackRepository)
    {
        _feedbackService = feedbackService;
        _feedbackRepository = feedbackRepository;
    }

    [HttpPost]
    //[Authorize]
    public Task SetEnableStatus([FromQuery, Required] bool isEnable)
    {
        return _feedbackService.SetFeedbackStatusAsync(isEnable);
    }
    
    //[Authorize]
    [HttpGet("list")]
    public async Task<FeedbackSet> GetFeedbacks([FromQuery, Required] int offset, [FromQuery] int limit = 5)
    {
        var feedbackSet = await _feedbackRepository.GetFeedbackSetAsync(offset, limit);
        return feedbackSet;
    }
    
    //[Authorize]
    [HttpGet("rating")]
    public Task<RatingDto> GetRating()
    {
        return Task.FromResult(new RatingDto());
    }
}

public class RatingDto
{
    
}