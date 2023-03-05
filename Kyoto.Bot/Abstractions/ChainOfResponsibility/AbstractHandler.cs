namespace Kyoto.Bot.Abstractions.ChainOfResponsibility;

public abstract class AbstractHandler<THandlerContext> where THandlerContext : class
{
    private IHandler<THandlerContext>? _nextHandler;

    public IHandler<THandlerContext> SetNext(IHandler<THandlerContext> handler)
    {
        _nextHandler = handler;
        return handler;
    }
        
    public virtual async Task<THandlerContext?> HandleAsync(THandlerContext context)
    {
        if (_nextHandler is not null)
        {
            return await _nextHandler.HandleAsync(context);
        }
        
        return context;
    }
}