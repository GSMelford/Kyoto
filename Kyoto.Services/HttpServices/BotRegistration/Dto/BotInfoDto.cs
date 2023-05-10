using Newtonsoft.Json;

namespace Kyoto.Services.HttpServices.BotRegistration.Dto;

public class BotInfoDto
{
    [JsonProperty("result")] 
    public BotInfoResult BotInfoResult { get; set; } = null!;
}

public class BotInfoResult
{
    [JsonProperty("id")]
    public string Id { get; set; } = null!;
    
    [JsonProperty("first_name")]
    public string FirstName { get; set; } = null!;
    
    [JsonProperty("username")]
    public string Username { get; set; } = null!;
    
    [JsonProperty("can_join_groups")]
    public bool CanJoinGroups { get; set; }
    
    [JsonProperty("can_read_all_group_messages")]
    public bool CanReadAllGroupMessages { get; set; }
    
    [JsonProperty("supports_inline_queries")]
    public bool SupportsInlineQueries { get; set; }
}