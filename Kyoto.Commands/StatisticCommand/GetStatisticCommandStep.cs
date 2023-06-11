using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.StatisticCommand;

public class GetStatisticCommandStep : BaseCommandStep
{
    private IPostService _postService;

    public GetStatisticCommandStep(IPostService postService)
    {
        _postService = postService;
    }

    protected override Task<CommandStepResult> SetActionRequestAsync()
    {
        throw new NotImplementedException();
    }

    protected override Task<CommandStepResult> SetProcessResponseAsync()
    {
        throw new NotImplementedException();
    }
}