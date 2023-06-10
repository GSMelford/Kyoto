using Kyoto.Domain.FeedbackSystem;
using Kyoto.Domain.Menu;
using Kyoto.Domain.Menu.Interfaces;

namespace Kyoto.Services.FeedbackSystem;

public class FeedbackService : IFeedbackService
{
    private readonly IMenuRepository _menuRepository;

    public FeedbackService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public Task SetFeedbackStatusAsync(bool isEnable)
    {
        return _menuRepository.SetMenuButtonStatusAsync(MenuPanelConstants.Client.SendFeedback, isEnable);
    }
}