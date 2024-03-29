﻿using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public class TopicExchangeConsumer
    {
        public static void Consume(IModel channel)
        {

            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic);
            channel.QueueDeclare("demo-topic-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue: "demo-topic-queue", exchange: "demo-topic-exchange", routingKey: "account.*");
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

            channel.BasicConsume("demo-topic-queue", true, consumer);
            Console.WriteLine("Consumer starts");

            Console.ReadLine();

        }
    }
}
