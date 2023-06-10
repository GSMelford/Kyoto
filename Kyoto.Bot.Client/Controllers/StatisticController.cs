using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kyoto.Bot.Client.Controllers;

[ApiController]
[Route("api/statistic")]
public class StatisticController : ControllerBase
{
    //[Authorize]
    [HttpGet]
    public Task<StatisticDto> GetFeedbacks()
    {
        return Task.FromResult(new StatisticDto());
    }
}

public class StatisticDto
{
    
}