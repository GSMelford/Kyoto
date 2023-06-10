using Kyoto.Database.CommonModels;
using Kyoto.Domain.FeedbackSystem;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Database.Repositories.Feedback;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly IDatabaseContext _databaseContext;

    public FeedbackRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task SaveFeedbackAsync(string text, bool isAnonymous, long? telegramId, int stars)
    {
        var feedback = new CommonModels.Feedback
        {
            Text = text,
            Type = "Client",
            StarCount = stars
        };

        if (!isAnonymous)
        {
            feedback.ExternalUser = await _databaseContext.Set<ExternalUser>()
                .FirstOrDefaultAsync(x => x.PrivateId == telegramId);
        }

        await _databaseContext.SaveAsync(feedback);
    }

    public async Task<FeedbackSet> GetFeedbackSetAsync(int offset, int limit)
    {
        var feedbacks = await _databaseContext.Set<CommonModels.Feedback>()
            .Include(x=>x.ExternalUser)
            .OrderBy(x=>x.CreationTime)
            .ToListAsync();

        if (offset < 0) {
            return new FeedbackSet();
        }

        if (feedbacks.Count - offset < limit) {
            limit = feedbacks.Count - offset;
        }
        
        return new FeedbackSet
        {
            Feedbacks = feedbacks.Take(new Range(offset, limit + offset)).Select(x =>
            {
                var feedback = new Domain.FeedbackSystem.Feedback
                {
                    Text = x.Text,
                    Stars = x.StarCount
                };
                
                if (x.ExternalUser is not null)
                {
                    var user = _databaseContext.Set<User>()
                        .Include(i => i.TelegramUser)
                        .FirstOrDefault(y => y.TelegramUser!.Id == x.ExternalUser.Id);

                    feedback.ClientFullName = $"{user!.FirstName} {user.LastName}";
                }

                return feedback;
            }).ToList()
        };
    }
}