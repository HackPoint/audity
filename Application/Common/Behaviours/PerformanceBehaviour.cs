using System.Diagnostics;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> {
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehaviour(
        ILogger<TRequest> logger
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _timer = new Stopwatch();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken) {
        _timer.Start();
        var response = await next();
        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500) {
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning(
                "Too Long Running Request: Name {Name} ElapsedMilliseconds:({ElapsedMilliseconds} milliseconds) Request: {@Request}",
                requestName, elapsedMilliseconds, request
            );
        }
        return response;
    }
}