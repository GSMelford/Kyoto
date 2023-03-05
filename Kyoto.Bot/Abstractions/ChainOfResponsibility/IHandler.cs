namespace Kyoto.Bot.Abstractions.ChainOfResponsibility;

public interface IHandler<THandlerContext>
{
    IHandler<THandlerContext> SetNext(IHandler<THandlerContext> handler);
    Task<THandlerContext> HandleAsync(THandlerContext? context);
}