using LoteriaMexicanaServer.Hubs;
using LoteriaMexicanaServer.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalRCore();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddSingleton<GameManager>();
builder.Services.AddSingleton<PlayerManager>();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.MapHub<GameHub>("/gameHub");

app.Run();
