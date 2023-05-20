using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kyoto.Bot.Client.Controllers;

[ApiController]
[Route("api/faq")]
public class FaqController : ControllerBase
{
    [HttpPost]
    [Authorize]
    public Task SetFaq([FromBody, Required] string text)
    {
        return Task.CompletedTask;
    }
    
    [HttpGet]
    [Authorize]
    public Task<string> GetFaq()
    {
        return Task.FromResult(new string(""));
    }
}