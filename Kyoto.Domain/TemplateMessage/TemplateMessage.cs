namespace Kyoto.Domain.TemplateMessage;

public class TemplateMessage
{
    public TemplateMessageTypeValue Code { get; private set; }
    public string Text { get; private set; }
    public string Description { get; private set; }

    private TemplateMessage(TemplateMessageTypeValue code, string text, string description)
    {
        Code = code;
        Text = text;
        Description = description;
    }

    public static TemplateMessage Create(int code, string text, string description)
    {
        return new TemplateMessage((TemplateMessageTypeValue)code, text, description);
    }
}