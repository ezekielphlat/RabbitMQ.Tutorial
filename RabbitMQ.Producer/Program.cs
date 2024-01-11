using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory {
                VirtualHost= "IndiaVH",
                Uri = new Uri("amqp://administrator:admin@localhost:5672")
            };
           using var connection = factory.CreateConnection();
           using var channel = connection.CreateModel();
            DirectExchangeProducer.Publish(channel);
            Console.WriteLine("Producer Started");

            Console.ReadLine();
           
        }
    }
}