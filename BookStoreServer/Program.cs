using BookStoreServer.Core;
using BookStoreServer.Persistence;
using BookStoreServer.Persistence.Repositories;
using BookStoreServer.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreConnection")));

builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:3000", "https://appname.azurestaticapps.net");
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Get Books", Version = "v1" });
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DocumentTitle = "Get Books";
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Book Model.");
    options.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();


app.Run();