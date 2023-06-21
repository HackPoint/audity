using System.Diagnostics;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull {
    private readonly ILogger<TRequest> _logger;

    public LoggingBehaviour(ILogger<TRequest> logger) {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    ) {
        var requestName = typeof(TRequest).Name;
        string uniqueId = Guid.NewGuid().ToString();

        _logger.LogInformation("Begin Request: Id: {UniqueId}, request name: {RequestName}", uniqueId, requestName);
        var timer = new Stopwatch();
        timer.Start();

        var response = await next();
        timer.Stop();
        
        _logger.LogInformation("Audity Request: {Name} {@Request} , total request time:{ElapsedMilliseconds}",
            requestName, request, timer.ElapsedMilliseconds);
        return response;
    }
}