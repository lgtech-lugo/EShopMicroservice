﻿using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("[START]  {Request} - {Response} - {RequestDate}", typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();
        
        var response = await next();
        
        timer.Stop();
        logger.LogInformation("[END]  {Request} - {Response} - {RequestDate} - {ElapsedMilliseconds}ms", typeof(TRequest).Name, typeof(TResponse).Name, request, timer.ElapsedMilliseconds);
        return response;
    }
}