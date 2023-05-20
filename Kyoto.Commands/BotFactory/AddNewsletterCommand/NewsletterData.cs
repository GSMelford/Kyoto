using Kyoto.Domain.PreparedMessagesSystem;

namespace Kyoto.Commands.BotFactory.AddNewsletterCommand;

public class NewsletterData
{
    public string TenantKey { get; set; } = null!;
    public PostEventCode PostEventCode { get; set; }
    public string Text { get; set; } = null!;
}