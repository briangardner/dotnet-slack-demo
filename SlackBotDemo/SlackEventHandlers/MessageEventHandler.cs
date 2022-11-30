using SlackNet;
using SlackNet.Events;
using SlackNet.WebApi;

namespace SlackBotDemo.SlackEventHandlers;

public class MessageEventHandler : IEventHandler<MessageEvent>
{
    private readonly ISlackApiClient _slackApiClient;

    public MessageEventHandler(ISlackApiClient slackApiClient)
    {
        _slackApiClient = slackApiClient;
    }

    public async Task Handle(MessageEvent slackEvent)
    {
        await _slackApiClient.Chat.PostMessage(new Message()
        {
            Text = "Well hello there!",
            Channel = slackEvent.Channel
        });
    }
}