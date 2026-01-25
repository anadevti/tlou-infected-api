using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace tlou_infected_api.Handlers;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        // Limpa qualquer conteúdo anterior e configura a resposta
        httpContext.Response.Clear();
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        httpContext.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred",
            Status = StatusCodes.Status400BadRequest,
            Detail = "One or more validation errors occurred.",
            Type = exception.GetType().Name,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            Extensions = { ["errors"] = validationException.Errors }
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true; // Indica que a exceção foi tratada
    }
}