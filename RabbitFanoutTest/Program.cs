using RabbitMQ.Client;
using System.Text;

namespace RabbitPublisher
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
            channel.QueueDeclare("",true,false,false);
            while (true)
            {
                Console.ReadLine();
                var message = "Hello RabbitMQ Fanout";
                var body=Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("logs", "", null, body);
                Console.WriteLine("Message sent successfully");
            }
        }
    }
}
