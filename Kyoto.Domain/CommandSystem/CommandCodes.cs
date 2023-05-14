namespace Kyoto.Domain.CommandSystem;

public static class CommandCodes
{
    public static class BotManagement
    {
        public const string BotRegistration = "BotRegistration";
        public const string DeployBot = "DeployBot";
        public const string DeleteBot = "DeleteBot";
        public const string DisableBot = "DisableBot";
    }
    
    public static class BotFeatures
    {
        public const string AddNewsletter = "AddNewsletter";
        public const string RemoveNewsletter = "RemoveNewsletter";
        public const string SetRegistration = "SetRegistration";
        public const string EnableCollectFeedback = "EnableCollectFeedback";
        public const string SetFaq = "SetFaq";
        public const string GetStatistics = "GetStatistics";
    }
    
    public static class ComplaintsSuggestions
    {
        public const string Complaints = "AddComplaints";
        public const string Suggestions = "AddSuggestions";
    }
    
    public const string Registration = "Registration";
    public const string Cancel = "CancelCommand";
    public const string AboutBot = "AboutBot";
    public const string GenerateQrCode = "GenerateQrCode";
}