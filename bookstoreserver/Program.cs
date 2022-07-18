using bookstoreserver.Data;
using Microsoft.OpenApi.Models;
using bookstoreserver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyMethod()
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

app.UseCors("CORSPolicy");

Endpoints.UserEndpoints.Signup(app);
Endpoints.UserEndpoints.Login(app);
Endpoints.UserEndpoints.Get(app);

Endpoints.BookEndpoints.All(app);
Endpoints.BookEndpoints.Get(app);

Endpoints.BookEndpoints.Create(app);
Endpoints.BookEndpoints.Update(app);
Endpoints.BookEndpoints.Delete(app);


app.Run();

