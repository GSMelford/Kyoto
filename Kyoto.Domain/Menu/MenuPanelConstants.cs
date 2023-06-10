namespace Kyoto.Domain.Menu;

public static class MenuPanelConstants
{
    public const string HomeMenuPanel = "🏠 Головна панель";
    public const string BotManagementMenuPanel = "🤖⚙️ Управління ботами";
    public const string BotFeaturesMenuPanel = "🪄 Функції бота";
    public const string ComplaintsSuggestionsMenuPanel = "🤔 Скарги та пропозиції";
    
    public const string MenuPanelCode = "MenuPanel";
    public const string Back = "⬅️ Назад - ";

    public static string BuildBackText(string menuPanelName)
    {
        return $"{Back}{menuPanelName}";
    }
}