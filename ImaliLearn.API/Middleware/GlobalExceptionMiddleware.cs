// ===============================
// Global Exception Middleware
// ===============================

// 📍 API/Middleware/GlobalExceptionMiddleware.cs

using System.Net;
using System.Text.Json;
using ImaliLearn.API.Models;
using Microsoft.AspNetCore.Http;

namespace ImaliLearn.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (status, code) = exception switch
        {   // Map specific exceptions to status codes and error codes
            ArgumentException => (HttpStatusCode.BadRequest, "validation_error"),
            InvalidOperationException => (HttpStatusCode.Conflict, "conflict_error"),
            _ => (HttpStatusCode.InternalServerError, "server_error")
        };

        var response = new ErrorResponse
        {
            Code = code,
            Message = exception.Message,
            Status = (int)status, // HTTP status code
            TraceId = context.TraceIdentifier // Optional: include trace ID for debugging
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}