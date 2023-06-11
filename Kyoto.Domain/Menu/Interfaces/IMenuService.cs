using Kyoto.Domain.System;

namespace Kyoto.Domain.Menu.Interfaces;

public interface IMenuService
{
    Task SendHomeMenuAsync(Session session);
    Task<bool> SendMenuIfExistsAsync(Session session, string menuButtonText);
    Task<(bool, string)> TryGetMenuCommandCodeAsync( string menuButtonText);
}