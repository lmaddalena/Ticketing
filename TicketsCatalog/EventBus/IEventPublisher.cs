namespace TicketsCatalog.EventBus;

public interface IEventPublisher
{
    void PublishToMessageQueue(string integrationEvent, string eventData);
}