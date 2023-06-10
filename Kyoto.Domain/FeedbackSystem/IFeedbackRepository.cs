namespace Kyoto.Domain.FeedbackSystem;

public interface IFeedbackRepository
{
    Task SaveFeedbackAsync(string text, bool isAnonymous, long? telegramId, int stars);
    Task<FeedbackSet> GetFeedbackSetAsync(int offset, int limit);
}