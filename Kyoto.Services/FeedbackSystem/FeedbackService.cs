using Kyoto.Domain.FeedbackSystem;

namespace Kyoto.Services.FeedbackSystem;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackService(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public Task SetFeedbackStatusAsync(bool isEnable)
    {
        return _feedbackRepository.SetFeedbackStatusAsync(isEnable);
    }
}