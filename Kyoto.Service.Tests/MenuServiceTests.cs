using Kyoto.Domain.Menu.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Services.Menu;
using NSubstitute;
using Xunit;

namespace Kyoto.Tests;

public class MenuServiceTests
{
    private readonly IMenuService _menuService;
    private readonly IMenuRepository _menuRepository;
    private readonly IPostService _postService;
    
    public MenuServiceTests()
    {
        _menuRepository = Substitute.For<IMenuRepository>();
        _postService = Substitute.For<IPostService>();
        _menuService = new MenuService(_menuRepository, _postService);
    }

    [Fact]
    public async Task TryGetMenuCommandCode_DoesNotExist_WhenMenuButtonTextEmpty()
    {
        //Arrange
        string menuButtonText = string.Empty;
        _menuRepository.IsMenuButtonAsync(menuButtonText).Returns(false);
        
        //Act
        var (isExist, command) = await _menuService.TryGetMenuCommandCodeAsync(menuButtonText);

        //Assert
        Assert.False(isExist);
        Assert.Equal(command, string.Empty);
    }
    
    [Fact]
    public async Task TryGetMenuCommandCode_IsExists_WhenMenuButtonCommandText()
    {
        //Arrange
        const string menuButtonText = "Command";
        const string commandCode = "CommandCode";
        _menuRepository.IsMenuButtonAsync(menuButtonText).Returns(true);
        _menuRepository.GetMenuButtonCodeAsync(menuButtonText).Returns(commandCode);
        
        //Act
        var (isExist, command) = await _menuService.TryGetMenuCommandCodeAsync(menuButtonText);

        //Assert
        Assert.True(isExist);
        Assert.Equal(commandCode, command);
    }
}