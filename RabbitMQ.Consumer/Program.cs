using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {

           var factory = new ConnectionFactory
            {
               VirtualHost = "IndiaVH",
                Uri = new Uri("amqp://administrator:admin@localhost:5672")
            };
           using var connection = factory.CreateConnection();           
            using var channel = connection.CreateModel();         
                    FanoutExchangeConsumer.Consume(channel);
            Console.WriteLine("Consumer started");
           

            Console.ReadLine();
           
        }
    }
}