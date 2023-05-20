using TemplateMessageDal = Kyoto.Database.CommonModels.TemplateMessage;

namespace Kyoto.Database.CommonRepositories.TemplateMessage;

public static class Converter
{
    public static Domain.TemplateMessage.TemplateMessage ToDomain(this TemplateMessageDal templateMessage)
    {
        return Domain.TemplateMessage.TemplateMessage.Create(
            templateMessage.TemplateMessageType.Code, 
            templateMessage.Text, 
            templateMessage.TemplateMessageType.Description);
    }
}