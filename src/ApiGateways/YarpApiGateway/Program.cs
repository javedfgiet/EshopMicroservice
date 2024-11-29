using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
//Add Service To Container
builder.Services.AddReverseProxy().LoadFromConfig(
    builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(rateLimiterOption =>
{
    rateLimiterOption.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});

var app = builder.Build();

//Configure Http Request Pipeline

//app.MapGet("/", () => "Hello World!");
app.UseRateLimiter();
app.MapReverseProxy();

app.Run();
