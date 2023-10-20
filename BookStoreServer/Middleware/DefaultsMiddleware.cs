using Microsoft.EntityFrameworkCore;
using Services.Abstractions;

namespace BookStoreServer.Middleware;

public class DefaultsMiddleware
{
    private readonly RequestDelegate _next;

    public DefaultsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAuthService service)
    {
         await service.AddDefaultCreator();
         await _next(context);
    }
}