using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public class DirectExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            //  configuring time to live
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct, arguments: ttl);

            int count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello Count {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("demo-direct-exchange", "account.init", null, body);
                count++;
                Thread.Sleep(1000);

            }
        }
    }
}
