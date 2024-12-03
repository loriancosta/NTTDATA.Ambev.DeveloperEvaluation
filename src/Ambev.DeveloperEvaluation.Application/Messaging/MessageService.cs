using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Messaging;

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;

    public MessageService(ILogger<MessageService> logger)
    {
        _logger = logger;
    }

    public async Task SendMessageAsync(string message)
    {
        _logger.LogInformation("Message sent: {Message}", message);
        await Task.CompletedTask;
    }
}
