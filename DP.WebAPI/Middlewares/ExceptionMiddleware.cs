using FluentValidation;
using System.Net;
using System.Text.Json;

namespace DP.WebAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleException(context, ex.Errors.Select(e => e.ErrorMessage), HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            await HandleException(context, new[] { ex.Message }, HttpStatusCode.InternalServerError);
        }
    }

    private static async Task HandleException(HttpContext context, IEnumerable<string> errors, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            Errors = errors
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}