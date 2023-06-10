using Kyoto.Commands.BotRegistrationCommand;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;
using NSubstitute;
using Xunit;

namespace Kyoto.Commands.Tests;

public class BotRegistrationTests
{
    private readonly IPostService _postService;
    
    public BotRegistrationTests()
    {
        _postService = Substitute.For<IPostService>();
    }
    
    [Fact]
    public async Task SetActionRequest_WhenPostServiceSuccess()
    {
        //Arrange
        const string text = "TestMessage";
        var successResult = CommandStepResult.CreateSuccessful();
        _postService.SendTextMessageAsync(Arg.Any<Session>(), text).Returns(Task.CompletedTask);
        var botRegistrationTokenStep = new BotRegistrationTokenStep(_postService);
        
        //Act
        var result = await botRegistrationTokenStep.SendActionRequestAsync();

        //Assert
        Assert.Equal(successResult.IsRetry, result.IsRetry);
        Assert.Equal(successResult.IsInterrupt, result.IsInterrupt);
        Assert.Equal(successResult.ToRetryStep, result.ToRetryStep);
    }
    
    [Fact]
    public async Task ProcessResponse_WhenPostServiceSuccess()
    {
        //Arrange
        const string token = "TOKEN";
        var commandContext = CommandContext.Create(new Message { Text = token }, null);
        var botRegistrationTokenStep = new BotRegistrationTokenStep(_postService);
        botRegistrationTokenStep.SetCommandContext(commandContext);
        
        //Act
        await botRegistrationTokenStep.ProcessResponseAsync();

        //Assert
        Assert.NotNull(botRegistrationTokenStep.CommandContext.AdditionalData);
        Assert.NotEmpty(botRegistrationTokenStep.CommandContext.AdditionalData);
    }
}