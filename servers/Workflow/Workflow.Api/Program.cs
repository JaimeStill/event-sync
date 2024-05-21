using DataStack.Extensions;
using EventSync.Server;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCorsService();
builder.ConfigureApiServices();
builder.ConfigureSignalRServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors();

app.MapControllers();
app.MapHub<EventHub>("/events");

app.Run();