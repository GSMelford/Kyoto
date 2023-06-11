namespace Kyoto.Domain.PreparedMessagesSystem;

public class PreparedMessage
{
    public PostEventCode PostEventCode { get; private set; }
    public string Text { get; private set; }
    public DateTime? TimeToSend { get; private set; }
    public string? KeyWords { get; set; }

    public PreparedMessage(PostEventCode postEventCode, string text, DateTime? timeToSend, string? keyWords)
    {
        PostEventCode = postEventCode;
        Text = text;
        TimeToSend = timeToSend;
        KeyWords = keyWords;
    }

    public static PreparedMessage Create(PostEventCode postEventCode, string text, DateTime? timeToSend, string? keyWords)
    {
        return new PreparedMessage(postEventCode, text, timeToSend, keyWords);
    }
}