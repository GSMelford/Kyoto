using Kyoto.Domain.BotFactory.Bot;
using Kyoto.Services.HttpServices.BotRegistration.Dto;
using Newtonsoft.Json;

namespace Kyoto.Services.HttpServices.BotRegistration;

public class BotRegistrationHttpService
{
    private readonly HttpClient _httpClient;

    private const string API_URL = "https://api.telegram.org/bot";
    
    public BotRegistrationHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BotModel> GetBotInfoAsync(BotModel botModel)
    {
        var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{API_URL}{botModel.Token}/getMe"));
        var botInfo = JsonConvert.DeserializeObject<BotInfoDto>(await response.Content.ReadAsStringAsync())!.BotInfoResult;
        
        return botModel.Init(
            botInfo.Id,
            botInfo.FirstName,
            botInfo.Username,
            botInfo.CanJoinGroups,
            botInfo.CanReadAllGroupMessages,
            botInfo.SupportsInlineQueries);
    }
}