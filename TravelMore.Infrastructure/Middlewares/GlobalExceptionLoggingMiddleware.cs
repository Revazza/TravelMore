using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Common.Results;

namespace TravelMore.Infrastructure.Middlewares;

public class GlobalExceptionLoggingMiddleware : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (UnauthorizedAccessException err)
        {
            await ProcessExceptionAsync(err, context, HttpStatusCode.Forbidden);
        }
        catch (Exception err)
        {
            await ProcessExceptionAsync(err, context, HttpStatusCode.InternalServerError);
        }
    }

    private async Task ProcessExceptionAsync(Exception err, HttpContext context, HttpStatusCode statusCode)
    {
        ConfigureResponse(context, statusCode);
        LogError(err);
        await WriteResponseAsync(err, context);
    }

    private static void ConfigureResponse(HttpContext context, HttpStatusCode statusCode)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
    }

    private static async Task WriteResponseAsync(Exception err, HttpContext context)
    {
        //Returning brief description about exception
        var response = Result.Failure(new Error("", err.Message));
        var responseJson = JsonConvert.SerializeObject(response);

        await context.Response.WriteAsync(responseJson);
    }

    private void LogError(Exception err)
    {
        //Logging full information about exception
        Log.Error($"Error in {nameof(Exception)}: {err}");
    }


}
