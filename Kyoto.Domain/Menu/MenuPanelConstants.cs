namespace Kyoto.Domain.Menu;

public static class MenuPanelConstants
{
    public const string HomeMenuPanel = "🏠 Home";
    public const string BotManagementMenuPanel = "🤖⚙️ Bot management";
    public const string BotFeaturesMenuPanel = "🪄 Features of the bot";
    public const string ComplaintsSuggestionsMenuPanel = "🤔 Complaints and suggestions";
    
    public const string MenuPanelCode = "MenuPanel";
    public const string Back = "⬅️ Back - ";

    public static string BuildBackText(string menuPanelName)
    {
        return $"{Back}{menuPanelName}";
    }
}