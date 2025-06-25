using LoteriaMexicanaServer.Hubs;
using LoteriaMexicanaServer.Managers;
using LoteriaMexicanaServer.Services;
using LoteriaMexicanaTypes.Models;
using Microsoft.AspNetCore.Http.Connections;
using TypedSignalR.Client.DevTools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(options => { options.EnableDetailedErrors = true; }).AddJsonProtocol();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<GameRoomManager>();
builder.Services.AddSingleton<PlayerManager>();
builder.Services.AddSingleton<GameActionsService>();
builder.Services.AddSingleton<PlayerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSignalRHubSpecification();
    app.UseSignalRHubDevelopmentUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.MapHub<GameHub>("/gamehub", options => { options.Transports = HttpTransportType.WebSockets; });

app.Run();
