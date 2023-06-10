namespace Kyoto.Domain.FeedbackSystem;

public class Feedback
{
    public string Text { get; set; } = null!;
    public string? ClientFullName { get; set; }
    public int Stars { get; set; }
}