using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TVM.Models;
using TVM.Repository;

namespace TVM.MessageListener;

public static class EventListener 
{

    public static void ListenForIntegrationEvents()
    {
        // TOOO: Reuse and close connections and channel, etc, 
        ConnectionFactory factory = new ConnectionFactory(){ HostName = "lm-server01.westeurope.cloudapp.azure.com" };
        IConnection connection = factory.CreateConnection();

        ITicketRepository repo = new TicketRepository(new TvmDataContext());

        var channel = connection.CreateModel();
        
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {           
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var type = ea.RoutingKey;

            if (type == "ticket.add")
            {
                Ticket? tk = JsonSerializer.Deserialize<Ticket>(message);
                if(tk!= null)
                {
                    await repo.AddTicketAsync(tk);
                    await repo.SaveAsync();
                }

            }
            else if (type == "ticket.patch")
            {
                Ticket? tk = JsonSerializer.Deserialize<Ticket>(message);
                if(tk != null)
                {
                    Ticket tkOld = await repo.GetTicketByIdAsync(tk.TicketId);
                    if(tkOld != null)
                        tkOld.Price = tk.Price;
                    await repo.SaveAsync();                    

                }
            }

        };

        channel.BasicConsume(queue: "ticket.add",
                        autoAck: true,
                        consumer: consumer);

        channel.BasicConsume(queue: "ticket.patch",
                                    autoAck: true,
                                    consumer: consumer);


    }
}