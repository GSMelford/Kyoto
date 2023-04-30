using System.ComponentModel.DataAnnotations;
using Kyoto.Telegram.Receiver.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TBot.Telegram.Dto.Updates;

namespace Kyoto.Telegram.Receiver.Controllers;

[ApiController]
[Route("api/update")]
public class UpdateController : ControllerBase
{
    private readonly IUpdateService _updateService;
    private const string TELEGRAM_TENANT_HEADER = "X-Telegram-Bot-Api-Secret-Token";

    public UpdateController(IUpdateService updateService)
    {
        _updateService = updateService;
    }

    public async Task GetUpdate([FromBody, Required] UpdateDto updateDto)
    {
        Request.Headers.TryGetValue(TELEGRAM_TENANT_HEADER, out var tenantKey);
        await _updateService.HandleAsync(tenantKey.ToString(), updateDto.ToDomain());
    }
}