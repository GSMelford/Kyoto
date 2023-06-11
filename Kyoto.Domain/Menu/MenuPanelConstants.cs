namespace Kyoto.Domain.Menu;

public static class MenuPanelConstants
{
    public const string HomeMenuPanel = "🏠 Головна панель";
    public const string BotManagementMenuPanel = "🤖⚙️ Управління ботами";
    public const string BotFeaturesMenuPanel = "🪄 Функції бота";
    public const string ComplaintsSuggestionsMenuPanel = "🤔 Скарги та пропозиції";
    
    public static class Button
    {
        public const string EnableCollectFeedback = "🗣️ Увімкнути відгуки";
        public const string DisableCollectFeedback = "🗣️ Вимкнути відгуки";
    }
    
    public static class Client
    {
        public const string SendFeedback = "🗣 Залишити відгук";
        public const string GetFaq = "ℹ️ Подивитися FAQ";
        public const string PreOrderService = "⌛️ Послуги";
    }
    
    public const string MenuPanelCode = "MenuPanel";
    public const string Back = "⬅️ Назад - ";

    public static string BuildBackText(string menuPanelName)
    {
        return $"{Back}{menuPanelName}";
    }
}