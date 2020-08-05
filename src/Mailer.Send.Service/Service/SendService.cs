using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Send.Service.Service
{
    public class SendService
    {
        public void Send()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmqMailer", Port = 5672, UserName = "admin", Password = "admin" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "mailToSendQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "mailToSendQueue",
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
