﻿using System.Net;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BookStoreServer.ExceptionHandlers;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException e)
        {
            await HandleBaseExceptionAsync(context, e);
        }
        catch (DbUpdateException)
        {
            await HandleDbUpdateExceptionAsync(context);
        }
        catch
        {
            await HandleUnexpectedExceptionAsync(context);
        }
    }
    private static Task HandleBaseExceptionAsync(HttpContext context, BaseException exception)
    {
        var code = exception.StatusCode;
        var result = JsonConvert.SerializeObject(new
        {
            error = exception.Message
        });
        return WriteErrorToJson(context, result, code);
    }
    private static Task HandleDbUpdateExceptionAsync(HttpContext context)
    {
        var code = HttpStatusCode.BadRequest;
        var message = "Data cannot be updated.";
        var result = JsonConvert.SerializeObject(new
        {
            error = message
        });
        return WriteErrorToJson(context, result, code);
    }
    private static Task HandleUnexpectedExceptionAsync(HttpContext context)
    {
        var code = HttpStatusCode.InternalServerError;
        var message = "Unexpected error occurred.";
        var result = JsonConvert.SerializeObject(new
        {
            error = message
        });
        return WriteErrorToJson(context, result, code);
    }
    private static Task WriteErrorToJson(HttpContext context, string result, HttpStatusCode code)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}