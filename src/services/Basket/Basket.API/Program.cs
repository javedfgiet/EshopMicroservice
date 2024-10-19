


var builder = WebApplication.CreateBuilder(args);
// Add Services to container

var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("DataBase")!);
    config.Schema.For<ShoppingCart>().Identity(x => x.UserName);

}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure Processing Pipeline
app.UseExceptionHandler(opt =>
{

});

app.MapCarter();


app.Run();
