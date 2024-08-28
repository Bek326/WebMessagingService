using Microsoft.AspNetCore.SignalR.Client;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5135/messageHub")
    .Build();

connection.On<string, string>("ReceiveMessage", (user, message) =>
{
    Console.WriteLine($"Received message: {message}");
});

await connection.StartAsync();

app.MapGet("/", () => "Client 2 listening to messages...");

app.Run();