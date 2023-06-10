using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Menu;
using MenuPanelDal = Kyoto.Database.CommonModels.MenuPanel;
using MenuButtonDal = Kyoto.Database.CommonModels.MenuButton;

namespace Kyoto.Database.BotFactory.Repositories.Deploy.MenuPanels;

public static class BotFeaturesMenuPanel
{
    public static MenuPanelDal Get(string backMenuPanelName)
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
                    Text = "📜 Додати заготовлене повідомлення",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.AddNewsletter,
                    Index = 0,
                    Line = 1,
                    IsEnable = true
                },
                new ()
                {
                    Text = "📜 Видалити заготовлене повідомлення",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.RemoveNewsletter,
                    Index = 1,
                    Line = 1,
                    IsEnable = true
                },
                new ()
                {
                    Text = "🗣️ Увімкнути збір відгуків",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.EnableCollectFeedback,
                    Index = 0,
                    Line = 2,
                    IsEnable = true
                },
                new ()
                {
                    Text = "🙋‍♂️ Встановити FAQ",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.SetFaq,
                    Index = 1,
                    Line = 2,
                    IsEnable = true
                },
                new ()
                {
                    Text = "ℹ️ Отримати статистику",
                    IsCommand = true,
                    Code = CommandCodes.BotFeatures.GetStatistics,
                    Index = 2,
                    Line = 2,
                    IsEnable = true
                },
                new ()
                {
                    Text = MenuPanelConstants.BuildBackText(backMenuPanelName),
                    Code = MenuPanelConstants.MenuPanelCode,
                    Index = 0,
                    Line = 3,
                    IsEnable = true
                }
            }
        };
    }
}