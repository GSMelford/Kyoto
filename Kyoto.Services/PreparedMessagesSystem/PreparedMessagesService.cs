using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.PreparedMessagesSystem;
using Kyoto.Domain.System;

namespace Kyoto.Services.PreparedMessagesSystem;

public class PreparedMessagesService : IPreparedMessagesService
{
    private readonly IPreparedMessagesRepository _preparedMessagesRepository;
    private readonly IPostService _postService;

    public PreparedMessagesService(IPreparedMessagesRepository preparedMessagesRepository, IPostService postService)
    {
        _preparedMessagesRepository = preparedMessagesRepository;
        _postService = postService;
    }
    
    public async Task<bool> ProcessAsync(Session session, string messageText)
    {
        var (isExist, suitablePreparedMessage) = await TryFindPreparedMessageAsync(messageText);
        if (isExist)
        {
            await _postService.SendTextMessageAsync(session, suitablePreparedMessage!.Text);
            return true;
        }
        
        return false;
    }

    private async Task<(bool, PreparedMessage?)> TryFindPreparedMessageAsync(string messageText)
    {
        var preparedMessages = await _preparedMessagesRepository.GetPreparedMessagesByAnswerAsync();

        var isExist = false;
        var coincidences = 0;
        PreparedMessage? suitablePreparedMessage = null;
        
        foreach (var preparedMessage in preparedMessages)
        {
            int tampCoincidences = 0;
            PreparedMessage? tempSuitablePreparedMessage = null;
            foreach (var keyWord in preparedMessage.KeyWords!.Split(","))
            {
                if (messageText.ToLower().Contains(keyWord.ToLower().Trim()))
                {
                    tampCoincidences++;
                    tempSuitablePreparedMessage = preparedMessage;
                }
            }

            if (tampCoincidences > coincidences && tempSuitablePreparedMessage is not null)
            {
                suitablePreparedMessage = tempSuitablePreparedMessage;
                coincidences = tampCoincidences;
                isExist = true;
            }
        }
        
        return (isExist, suitablePreparedMessage);
    }
}