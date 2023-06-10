using Kyoto.Database.CommonModels;
using Kyoto.Domain.BotClient.Deploy.Interfaces;
using Kyoto.Domain.Deploy;
using Kyoto.Domain.PreparedMessagesSystem;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PostEvent = Kyoto.Database.CommonModels.PostEvent;

namespace Kyoto.Database.Repositories.Deploy;

public abstract class BaseDeployRepository : IDeployRepository
{
    private protected readonly IDatabaseContext DatabaseContext;

    protected BaseDeployRepository(IDatabaseContext databaseContext)
    {
        DatabaseContext = databaseContext;
    }

    public async Task InitDatabaseAsync(InitTenantInfo initTenantInfo)
    {
        var status = await DatabaseContext.Set<SystemStatus>()
            .FirstOrDefaultAsync(x=>x.Name == "Database");
        
        if (status?.Status == "Initialized") {
            return;
        }
        
        await InitMenuAsync();
        await InitTemplateMessagesAsync(initTenantInfo.TemplateMessages);
        await InitPostEventAsync();
        
        await DatabaseContext.SaveAsync(new SystemStatus
        {
            Name = "Database",
            Status = "Initialized"
        });
    }

    private async Task InitTemplateMessagesAsync(string templateMessageFileName)
    {
        var templateMessages = 
            await File.ReadAllTextAsync(Path.Combine("KnowledgeBase", templateMessageFileName));
        
        foreach (var templateMessage in JToken.Parse(templateMessages))
        {
            var startMessage = new CommonModels.TemplateMessage
            {
                TemplateMessageType = new TemplateMessageType
                {
                    Name = templateMessage["Name"]!.ToString(),
                    Code = int.Parse(templateMessage["Code"]!.ToString()),
                    Description = templateMessage["Description"]!.ToString()
                },
                Text = templateMessage["Text"]!.ToString()
            };

            await DatabaseContext.SaveAsync(startMessage);
        }
    }

    private async Task InitPostEventAsync()
    {
        var postEvents = new List<PostEvent>()
        {
            new()
            {
                Code = PostEventCode.Time,
                Name = "Message by time"
            },
            new()
            {
                Code = PostEventCode.Answer,
                Name = "Answer to words"
            }
        };

        foreach (var postEvent in postEvents)
        {
            await DatabaseContext.SaveAsync(postEvent);
        }
    }
    
    protected abstract Task InitMenuAsync();
}