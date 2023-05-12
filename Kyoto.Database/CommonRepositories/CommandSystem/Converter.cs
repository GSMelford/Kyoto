using Kyoto.Domain.CommandSystem;

namespace Kyoto.Database.CommonRepositories.CommandSystem;

public static class Converter
{
    public static Command ToDomain(this CommonModels.Command command)
    {
        return Command.Create(
            command.SessionId,
            command.ChatId,
            command.ExternalUserId,
            command.Name,
            command.AdditionalData,
            command.Step,
            (CommandState)command.State);
    }
}