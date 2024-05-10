using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitConsumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri=new Uri("amqps://wkrhegxa:6UytImCBsgpRvzEKMSy4Ji-T68l7kOxl@roedeer.rmq.cloudamqp.com/wkrhegxa");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue:queueName,exchange:"logs",routingKey:string.Empty); 

            var consumer=new EventingBasicConsumer(channel);
            channel.BasicConsume(queueName,true, consumer);
            consumer.Received+=Consumer_Received;
            Console.ReadLine();
        }

        private static void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var message=Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine($"Recieved message : {message}");
        }
    }
}
