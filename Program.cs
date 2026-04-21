using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ExternalApiSettings>(
    builder.Configuration.GetSection("ExternalApi")
);

builder.Services.AddHttpClient<AuthService>((sp, c) =>
{
    var settings = sp.GetRequiredService<IOptions<ExternalApiSettings>>().Value;
    c.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddHttpClient<As400Service>((sp, c) =>
{
    var settings = sp.GetRequiredService<IOptions<ExternalApiSettings>>().Value;
    c.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddControllers();
var app = builder.Build();

if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", () => "API OK");

app.MapControllers();

//app.Run("http://0.0.0.0:80");
app.Run("http://localhost:5000");