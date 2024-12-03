namespace Ambev.DeveloperEvaluation.Application.Messaging;

public interface IEventPublisher
{
    Task PublishEventAsync(IEvent eventToPublish);
}
