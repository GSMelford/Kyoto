namespace Kyoto.Domain.FeedbackSystem;

public interface IFeedbackService
{
    Task SetFeedbackStatusAsync(bool isEnable);
}