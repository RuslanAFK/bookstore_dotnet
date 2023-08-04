namespace BookStoreServer.Test.ExceptionHandlers;

public class ExceptionHandlerMiddlewareTest
{
    private HttpContext httpContext;
    [SetUp]
    public void Setup()
    {
        httpContext = new DefaultHttpContext();
    }
    [Test]
    public async Task Invoke_WithBaseException_ReturnsResponseWithCorrectStatusCode()
    {
        Task NextDelegate(HttpContext _)
        {
            var type = A.Dummy<Type>();
            var str = A.Dummy<string>();
            throw new EntityNotFoundException(type, str);
        }
        var middleware = new ExceptionHandlerMiddleware(NextDelegate);
        await middleware.Invoke(httpContext);
        var statusCode = httpContext.Response.StatusCode;
        Assert.That(statusCode, Is.EqualTo(StatusCodes.Status404NotFound));
    }
    [Test]
    public async Task Invoke_WithValidationException_ReturnsResponseWith400StatusCode()
    {
        Task NextDelegate(HttpContext _) => throw new ValidationException();
        var middleware = new ExceptionHandlerMiddleware(NextDelegate);
        await middleware.Invoke(httpContext);
        var statusCode = httpContext.Response.StatusCode;
        Assert.That(statusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
    }
    [Test]
    public async Task Invoke_WithOtherException_ReturnsResponseWith500StatusCode()
    {
        Task NextDelegate(HttpContext _) => throw new Exception();
        var middleware = new ExceptionHandlerMiddleware(NextDelegate);
        await middleware.Invoke(httpContext);
        var statusCode = httpContext.Response.StatusCode;
        Assert.That(statusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
    }
}