using System.Net;
using System.Text.Json;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Models;
using ValidationException = TaskManager.Application.Common.Exceptions.ValidationException;

namespace TaskManager.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        ApiResponse<object?> response = exception switch
        {
            NotFoundException nfe => ApiResponse<object?>.NotFound(nfe.Message),
            ValidationException ve => ApiResponse<object?>.BadRequest("Validation failed.", ve.Errors),
            _ => ApiResponse<object?>.Fail("An unexpected error occurred.")
        };

        context.Response.StatusCode = response.StatusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(response,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }
}
