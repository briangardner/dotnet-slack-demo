using MediatR;
using SlackNet;
using SlackNet.WebApi;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace SlackBotDemo.UseCases.Hello;

public record HelloRequest (string Channel, string Message) : IRequest;

public class HelloRequestHandler : IRequestHandler<HelloRequest>
{
    private readonly ISlackApiClient _slackApiClient;
    private readonly ILogger _logger;

    public HelloRequestHandler(ISlackApiClient slackApiClient, ILogger<HelloRequestHandler> logger)
    {
        _slackApiClient = slackApiClient;
        _logger = logger;
    }
    public async Task<Unit> Handle(HelloRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userInfo = await _slackApiClient.Users.LookupByEmail("bgardner@nuvalence.io");
            await _slackApiClient.Chat.PostMessage(new Message
            {
                Channel = userInfo.Id,
                Text = request.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error doing a thing");
        }
        
        return Unit.Value;
    }
}