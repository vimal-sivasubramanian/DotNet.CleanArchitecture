namespace DotNet.CleanArchitecture.MessageBrokers.RabbitMQ
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ExchangeName { get; set; }

        public string RoutingKey { get; set; }

        public string QueueName { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"amqp://{UserName}:{Password}@{HostName}:{Port}/%2f";
            }
        }
    }
}
