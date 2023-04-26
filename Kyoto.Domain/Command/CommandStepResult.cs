namespace Kyoto.Domain.Command;

public class CommandStepResult
{
    public bool IsSuccessful { get; private set; }
    public bool IsRetry { get; private set; }
    public string? ErrorMessage { get; private set; }
    public object? DateAfterProcessing { get; private set; }

    private CommandStepResult(bool isSuccessful, bool isRetry, string? errorMessage, object? dateAfterProcessing)
    {
        IsSuccessful = isSuccessful;
        IsRetry = isRetry;
        ErrorMessage = errorMessage;
        DateAfterProcessing = dateAfterProcessing;
    }

    public static CommandStepResult CreateSuccessful(object? dateAfterProcessing = null)
    {
        return new CommandStepResult(true, false, string.Empty, dateAfterProcessing);
    }
    
    public static CommandStepResult CreateError(string? errorMessage, object? dateAfterProcessing = null)
    {
        return new CommandStepResult(false, false, errorMessage, dateAfterProcessing);
    }
    
    public static CommandStepResult CreateRetry(string? errorMessage)
    {
        return new CommandStepResult(true, true, errorMessage, string.Empty);
    }
}