using Reservar.Common.Domain.Exceptions;
using Authentication.Domain.Exceptions;
using Reservar.Common.Domain.Response;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Net;

namespace Authentication.Infrastrunture.Middlewares;
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
            _logger.LogInformation("Handling request: " + context.Request.Path);
            await _next(context);
        }
        catch (AppException ex)
        {
            _logger.LogError(ex, $"StackTrace: {ex.StackTrace}");
            _logger.LogError(ex, $"An unhandled exception has occurred: {ex.Message}");
            await GetResult(ex, context);
        }
        finally
        {
            _logger.LogInformation("Finished handling request.");
        }
    }

    private async Task GetResult(Exception exception, HttpContext context)
    {
        switch (exception)
        {
            case ConflictException conflictException:
                await OnCustomConflictException(context, conflictException);
                break;

            case NoContentException noContentException:
                await OnCustomNotFoundException(context, noContentException);
                break;

            case IncorrectCredentialsException incorrectCredentialsException:
                await OnCustomConflictException(context, incorrectCredentialsException);
                break;

            case DuplicateCredentialsException duplicateCredentialsException:
                await OnCustomNotFoundException(context, duplicateCredentialsException);
                break;

            case FailCredentialsException failCredentialsException:
                await OnCustomNotRefreshException(context, failCredentialsException);
                break;

            default:
                await SendResult(context, exception, HttpStatusCode.InternalServerError);
                break;
        }
    }

    private async Task OnCustomConflictException(HttpContext context, Exception exception)
    {
        await SendResult(context, exception, HttpStatusCode.Conflict);
    }

    private async Task OnCustomNotFoundException(HttpContext context, Exception exception)
    {
        await SendResult(context, exception, HttpStatusCode.NoContent);
    }

    private async Task OnCustomNotRefreshException(HttpContext context, Exception exception)
    {
        await SendResult(context, exception, HttpStatusCode.Forbidden);
    }

    private async Task SendResult(HttpContext context, Exception exception, HttpStatusCode code)
    {
        var message = GetMessage(exception);
        var response = new Response<object>(message);
        var jsonReponse = JsonSerializer.Serialize(response);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        await context.Response.WriteAsync(jsonReponse);
    }

    private static string GetMessage(Exception exception)
    {
        try
        {
            return exception.Message;
        }
        catch (Exception)
        {
            return "Not-Message-Defined";
        }
    }
}