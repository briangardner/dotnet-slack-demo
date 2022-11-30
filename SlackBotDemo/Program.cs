using MediatR;
using SlackBotDemo;
using SlackBotDemo.Models;
using SlackBotDemo.SlackEventHandlers;
using SlackBotDemo.UseCases.Hello;
using SlackNet.AspNetCore;
using SlackNet.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var slackSettings = builder.Configuration.GetSection("Slack").Get<SlackSettings>();

builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(HelloRequestHandler));

builder.Services.AddSlackNet(x => x.UseApiToken(slackSettings?.AccessToken)
    .RegisterEventHandler<MessageEvent, MessageEventHandler>());
builder.Logging.ClearProviders().AddConsole();
builder.Services.AddSingleton<SlackEndpointConfiguration>(x =>
    new SlackEndpointConfiguration().UseSigningSecret(slackSettings?.SigningSecret));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.AddSlackEndpoints();

app.Run();