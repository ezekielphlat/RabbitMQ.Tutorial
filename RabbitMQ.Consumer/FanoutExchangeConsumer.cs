﻿using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public class FanoutExchangeConsumer
    {
        public static void Consume(IModel channel)
        {

            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout);
            channel.QueueDeclare("demo-fanout-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            channel.QueueBind(queue: "demo-fanout-queue", exchange: "demo-fanout-exchange", routingKey: string.Empty);
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

            channel.BasicConsume("demo-fanout-queue", true, consumer);
            Console.WriteLine("Consumer starts");

            Console.ReadLine();

        }
    }
}