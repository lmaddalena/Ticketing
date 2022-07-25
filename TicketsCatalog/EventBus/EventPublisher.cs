using System.Text;
using RabbitMQ.Client;

namespace TicketsCatalog.EventBus;


class EventPublisher : IEventPublisher
{
    ConnectionFactory factory;
    IConnection connection;

    public EventPublisher()
    {
        factory = new ConnectionFactory(){ HostName = "lm-server01.westeurope.cloudapp.azure.com" };
        connection = factory.CreateConnection();
    }
    public void PublishToMessageQueue(string integrationEvent, string eventData)
    {
        // TOOO: Reuse and close connections and channel, etc, 
        var channel = connection.CreateModel();
        var body = Encoding.UTF8.GetBytes(eventData);
        channel.BasicPublish(exchange: "ticket",
                            routingKey: integrationEvent,
                            basicProperties: null,
                            body: body);
        channel.Close();
    }

}
