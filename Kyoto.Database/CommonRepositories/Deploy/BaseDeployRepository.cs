using Kyoto.Database.CommonModels;
using Kyoto.Domain.BotClient.Deploy.Interfaces;
using Kyoto.Domain.Deploy;
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

    public virtual async Task InitDatabaseAsync(InitTenantInfo initTenantInfo)
    {
        await TemplateMessagesAsync(initTenantInfo.TemplateMessages);
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
                    Name = templateMessage["TemplateMessageTypeName"]!.ToString(),
                    Code = int.Parse(templateMessage["TemplateMessageTypeCode"]!.ToString()),
                    Description = templateMessage["TemplateMessageTypeDescription"]!.ToString()
                },
                Text = templateMessage["TemplateMessageText"]!.ToString()
            };

            await DatabaseContext.SaveAsync(startMessage);
        }
    }
}