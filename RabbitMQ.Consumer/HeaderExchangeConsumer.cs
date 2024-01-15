using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {

            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers);
            channel.QueueDeclare("demo-header-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var headers = new Dictionary<string, object> { { "account", "new" } };

            channel.QueueBind(queue: "demo-header-queue", exchange: "demo-header-exchange", routingKey: string.Empty, arguments: headers);
            // declearing a prefetch count
            // this allows the consumer fetch 10 messages at the same time
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("demo-header-queue", true, consumer);
            Console.WriteLine("Consumer starts");

            Console.ReadLine();

        }
    }
}
