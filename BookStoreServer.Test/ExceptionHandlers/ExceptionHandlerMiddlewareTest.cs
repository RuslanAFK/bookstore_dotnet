using System.ComponentModel.DataAnnotations;
using BookStoreServer.ExceptionHandlers;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BookStoreServer.Test.ExceptionHandlers;

public class ExceptionHandlerMiddlewareTest
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public async Task Invoke_WithBaseException_ReturnsResponseWithCorrectStatusCode()
    {
        var context = new DefaultHttpContext();
        RequestDelegate nextDelegate = (_) => 
            throw new EntityNotFoundException(typeof(object), "");
        var middleware = new ExceptionHandlerMiddleware(nextDelegate);

        await middleware.Invoke(context);
        
        Assert.That(context.Response.StatusCode, Is.EqualTo(404));
    }
    [Test]
    public async Task Invoke_WithValidationException_ReturnsResponseWith400StatusCode()
    {
        var context = new DefaultHttpContext();
        RequestDelegate nextDelegate = (context) =>
            throw new ValidationException();
        var middleware = new ExceptionHandlerMiddleware(nextDelegate);

        await middleware.Invoke(context);

        Assert.That(context.Response.StatusCode, Is.EqualTo(400));
    }
    [Test]
    public async Task Invoke_WithOtherException_ReturnsResponseWith500StatusCode()
    {
        var context = new DefaultHttpContext();
        RequestDelegate nextDelegate = (context) =>
            throw new Exception();
        var middleware = new ExceptionHandlerMiddleware(nextDelegate);

        await middleware.Invoke(context);

        Assert.That(context.Response.StatusCode, Is.EqualTo(500));
    }
}