using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Menu;
using MenuPanelDal = Kyoto.Database.CommonModels.MenuPanel;
using MenuButtonDal = Kyoto.Database.CommonModels.MenuButton;

namespace Kyoto.Database.BotFactory.Repositories.Deploy.MenuPanels;

public static class BotFeaturesMenuPanel
{
    public static MenuPanelDal GetFirstPart(string backMenuPanelName)
    {
        return new MenuPanelDal
        {
            Name = MenuPanelConstants.BotFeaturesMenuPanel,
            Buttons = new List<MenuButtonDal>
            {
                new ()
                {
                    Text = "🧑‍💻 Налаштувати реєстрацію",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.SetRegistration,
                    Index = 0,
                    Line = 0,
                    IsEnable = true
                },
                new ()
                {
                    Text = "📝 Додати повідмолення",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.AddNewsletter,
                    Index = 0,
                    Line = 1,
                    IsEnable = true
                },
                new ()
                {
                    Text = "❌ Видалити повідомлення",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.RemoveNewsletter,
                    Index = 1,
                    Line = 1,
                    IsEnable = true
                },
                new ()
                {
                    Text = MenuPanelConstants.Button.EnableCollectFeedback,
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.EnableCollectFeedback,
                    Index = 0,
                    Line = 2,
                    IsEnable = true,
                    IsNeedAccessToWatch = true
                },
                new ()
                {
                    Text = MenuPanelConstants.Button.DisableCollectFeedback,
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.DisableCollectFeedback,
                    Index = 0,
                    Line = 2,
                    IsEnable = true,
                    IsNeedAccessToWatch = true
                },
                new ()
                {
                    Text = "📃 Показати відгуки",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.ShowFeedbacks,
                    Index = 1,
                    Line = 2,
                    IsEnable = true
                },
                new ()
                {
                    Text = "🙋‍♂️ Редагувати FAQ",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.SetFaq,
                    Index = 0,
                    Line = 3,
                    IsEnable = true
                },
                new ()
                {
                    Text = "ℹ️ Статистика",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.GetStatistics,
                    Index = 1,
                    Line = 3,
                    IsEnable = true
                },
                new ()
                {
                    Text = MenuPanelConstants.BuildBackText(backMenuPanelName),
                    Code = MenuPanelConstants.MenuPanelCode,
                    Index = 0,
                    Line = 4,
                    IsEnable = true
                }
            }
        };
    }
}