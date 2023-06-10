namespace Kyoto.Commands.SendFeedbackCommand;

public class FeedbackData
{
    public string Text { get; private set; }
    public bool IsAnonymous { get; set; }
    public int Stars { get; set; }
    public long? TelegramId { get; set; }

    public FeedbackData(string text)
    {
        Text = text;
    }
}