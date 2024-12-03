namespace Ambev.DeveloperEvaluation.Application.Messaging;

public interface IEvent
{
    string EventName { get; }
    DateTime EventDate { get; }
}
