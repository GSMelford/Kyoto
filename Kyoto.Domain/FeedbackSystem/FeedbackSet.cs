namespace Kyoto.Domain.FeedbackSystem;

public class FeedbackSet
{
    public int Total { get; set; }
    public List<Feedback> Feedbacks { get; set; }= new ();
}