using MediatR;
using Microsoft.AspNetCore.Mvc;
using SlackBotDemo.UseCases.Hello;

namespace SlackBotDemo;

public static class SlackEndpoints
{
    public static IEndpointRouteBuilder AddSlackEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/slack/hello", async ([FromBody] HelloRequest request,  ISender sender) => await sender.Send(request))
            .WithName("Hello");
        return builder;
    }
}