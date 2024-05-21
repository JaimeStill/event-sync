using DataStack.Extensions;
using Proposal.Api.Events;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCorsService();
builder.ConfigureApiServices();
builder.ConfigureSignalRServices();

builder.Services.RegisterEvents();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.MapControllers();

app.Run();