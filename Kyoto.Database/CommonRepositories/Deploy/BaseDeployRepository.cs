using Kyoto.Database.CommonModels;
using Kyoto.Domain.BotClient.Deploy.Interfaces;
using Kyoto.Domain.Deploy;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TemplateMessage = Kyoto.Database.CommonModels.TemplateMessage;

namespace Kyoto.Database.CommonRepositories.Deploy;

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
        await TemplateMessagesAsync(initTenantInfo.TemplateMessages);
        
        await DatabaseContext.SaveAsync(new SystemStatus
        {
            Name = "Database",
            Status = "Initialized"
        });
    }

    private async Task TemplateMessagesAsync(string templateMessageFileName)
    {
        var templateMessages = 
            await File.ReadAllTextAsync(Path.Combine("KnowledgeBase", templateMessageFileName));
        
        foreach (var templateMessage in JToken.Parse(templateMessages))
        {
            var startMessage = new TemplateMessage
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

    protected abstract Task InitMenuAsync();
}