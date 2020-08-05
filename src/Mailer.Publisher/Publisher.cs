using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Mailer.Publisher
{
    public class Publisher
    {
        public void PublishEmail(EmailMessage emailMessage)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "mailToSendQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(emailMessage.ToMessage());

            channel.BasicPublish(exchange: "",
                                 routingKey: "mailToSendQueue",
                                 basicProperties: null,
                                 body: body);
        }
    }

    public class EmailMessage
    {
        public EmailMessage(string subject, string to, string body)
        {
            Subject = subject;
            To = to;
            Body = body;
        }

        public string Subject { get; private set; }
        public string To { get; private set; }
        public string Body { get; private set; }
        public string ToMessage()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
