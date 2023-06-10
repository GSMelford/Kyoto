namespace Kyoto.Domain.Menu;

public static class MenuPanelConstants
{
    public const string HomeMenuPanel = "🏠 Головна панель";
    public const string BotManagementMenuPanel = "🤖⚙️ Управління ботами";
    public const string BotFeaturesMenuPanel = "🪄 Функції бота";
    public const string ComplaintsSuggestionsMenuPanel = "🤔 Скарги та пропозиції";
    
    public static class Button
    {
        public const string EnableCollectFeedback = "🗣️ Увімкнути збір відгуків";
        public const string DisableCollectFeedback = "🗣️ Вимкнути збір відгуків";
    }
    
    public static class Client
    {
        public const string SendFeedback = "🗣 Відправити відгук";
        public const string GetFaq = "ℹ️ FAQ";
        public const string PreOrderService = "⌛️ Передзамовити послугу";
    }
    
    public const string MenuPanelCode = "MenuPanel";
    public const string Back = "⬅️ Назад - ";

    public static string BuildBackText(string menuPanelName)
    {
        return $"{Back}{menuPanelName}";
    }
}