using Kyoto.Bot.HttpServices.BotRegistration.Dto;
using Kyoto.Domain.Bot;
using Newtonsoft.Json;

namespace Kyoto.Bot.HttpServices.BotRegistration;

public class BotRegistrationHttpServices
{
    private readonly HttpClient _httpClient;

    private const string API_URL = "https://api.telegram.org/bot";
    
    public BotRegistrationHttpServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BotModel> GetBotInfoAsync(BotModel botModel)
    {
        var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{API_URL}{botModel.Token}/getMe"));
        var botInfo = JsonConvert.DeserializeObject<BotInfoDto>(await response.Content.ReadAsStringAsync())!.BotInfoResult;
        
        return botModel.Init(
            botInfo.Id,
            botModel.FirstName,
            botModel.Username,
            botModel.CanJoinGroups,
            botModel.CanReadAllGroupMessages,
            botModel.SupportsInlineQueries);
    }
}