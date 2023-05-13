namespace Kyoto.Domain.TemplateMessage;

public class TemplateMessage
{
    public int Code { get; private set; }
    public string Text { get; private set; }

    private TemplateMessage(int code, string text)
    {
        Code = code;
        Text = text;
    }

    public static TemplateMessage Create(int code, string text)
    {
        return new TemplateMessage(code, text);
    }
}