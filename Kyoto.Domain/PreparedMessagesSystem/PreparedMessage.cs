namespace Kyoto.Domain.PreparedMessagesSystem;

public class PreparedMessage
{
    public PostEventCode PostEventCode { get; private set; }
    public string Text { get; private set; } = null!;
    public DateTime? TimeToSend { get; private set; }

    public PreparedMessage(PostEventCode postEventCode, string text, DateTime? timeToSend)
    {
        PostEventCode = postEventCode;
        Text = text;
        TimeToSend = timeToSend;
    }

    public static PreparedMessage Create(PostEventCode postEventCode, string text, DateTime? timeToSend)
    {
        return new PreparedMessage(postEventCode, text, timeToSend);
    }
}