using DotNet.EventSourcing.Core.Interfaces.MessageBrokers;
using DotNet.EventSourcing.MessageBrokers;
using DotNet.EventSourcing.MessageBrokers.AzureEventHub;
using DotNet.EventSourcing.MessageBrokers.AzureQueue;
using DotNet.EventSourcing.MessageBrokers.AzureServiceBus;
using DotNet.EventSourcing.MessageBrokers.Fake;
using DotNet.EventSourcing.MessageBrokers.Kafka;
using DotNet.EventSourcing.MessageBrokers.RabbitMQ;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MessageBrokersCollectionExtensions
    {
        private static IServiceCollection AddAzureEventHubSender<TKey, TValue>(this IServiceCollection services, AzureEventHubOptions options)
        {
            services.AddSingleton<IMessageSender<TKey, TValue>>(new AzureEventHubSender<TKey, TValue>(
                                options.ConnectionString,
                                options.HubName));
            return services;
        }

        private static IServiceCollection AddAzureEventHubReceiver<TKey, TValue>(this IServiceCollection services, AzureEventHubOptions options)
        {
            services.AddTransient<IMessageReceiver<TKey, TValue>>(x => new AzureEventHubReceiver<TKey, TValue>(
                                options.ConnectionString,
                                options.HubName,
                                options.ConsumerGroup));
            return services;
        }

        private static IServiceCollection AddAzureQueueSender<TKey, TValue>(this IServiceCollection services, AzureQueueOptions options)
        {
            services.AddSingleton<IMessageSender<TKey, TValue>>(new AzureQueueSender<TKey, TValue>(
                                options.ConnectionString,
                                options.QueueName));
            return services;
        }

        private static IServiceCollection AddAzureQueueReceiver<TKey, TValue>(this IServiceCollection services, AzureQueueOptions options)
        {
            services.AddTransient<IMessageReceiver<TKey, TValue>>(x => new AzureQueueReceiver<TKey, TValue>(
                                options.ConnectionString,
                                options.QueueName));
            return services;
        }

        private static IServiceCollection AddAzureServiceBusSender<TKey, TValue>(this IServiceCollection services, AzureServiceBusOptions options)
        {
            services.AddSingleton<IMessageSender<TKey, TValue>>(new AzureServiceBusSender<TKey, TValue>(
                                options.ConnectionString,
                                options.QueueName));
            return services;
        }

        private static IServiceCollection AddAzureServiceBusReceiver<TKey, TValue>(this IServiceCollection services, AzureServiceBusOptions options)
        {
            services.AddTransient<IMessageReceiver<TKey, TValue>>(x => new AzureServiceBusReceiver<TKey, TValue>(
                                options.ConnectionString,
                                options.QueueName));
            return services;
        }

        private static IServiceCollection AddFakeSender<TKey, TValue>(this IServiceCollection services)
        {
            services.AddSingleton<IMessageSender<TKey, TValue>>(new FakeSender<TKey, TValue>());
            return services;
        }

        private static IServiceCollection AddFakeReceiver<TKey, TValue>(this IServiceCollection services)
        {
            services.AddTransient<IMessageReceiver<TKey, TValue>>(x => new FakeReceiver<TKey, TValue>());
            return services;
        }

        private static IServiceCollection AddKafkaSender<TKey, TValue>(this IServiceCollection services, KafkaOptions options)
        {
            services.AddSingleton<IMessageSender<TKey, TValue>>(new KafkaSender<TKey, TValue>(options.BootstrapServers, options.TopicName));
            return services;
        }

        private static IServiceCollection AddKafkaReceiver<TKey, TValue>(this IServiceCollection services, KafkaOptions options)
        {
            services.AddTransient<IMessageReceiver<TKey, TValue>>(x => new KafkaReceiver<TKey, TValue>(options.BootstrapServers,
                options.TopicName,
                options.GroupId));
            return services;
        }

        private static IServiceCollection AddRabbitMQSender<TKey, TValue>(this IServiceCollection services, RabbitMQOptions options)
        {
            services.AddSingleton<IMessageSender<TKey, TValue>>(new RabbitMQSender<TKey, TValue>(new RabbitMQSenderOptions
            {
                HostName = options.HostName,
                Port = options.Port,
                UserName = options.UserName,
                Password = options.Password,
                ExchangeName = options.ExchangeName,
                RoutingKey = options.RoutingKey,
            }));
            return services;
        }

        private static IServiceCollection AddRabbitMQReceiver<TKey, TValue>(this IServiceCollection services, RabbitMQOptions options)
        {
            services.AddTransient<IMessageReceiver<TKey, TValue>>(x => new RabbitMQReceiver<TKey, TValue>(new RabbitMQReceiverOptions
            {
                HostName = options.HostName,
                Port = options.Port,
                UserName = options.UserName,
                Password = options.Password,
                ExchangeName = options.ExchangeName,
                RoutingKey = options.RoutingKey,
                QueueName = options.QueueName,
                AutomaticCreateEnabled = true,
            }));
            return services;
        }

        public static IServiceCollection AddMessageBusReceiver<TKey, TValue>(this IServiceCollection services, MessageBrokerOptions options)
        {
            if (options.UsedRabbitMQ())
            {
                services.AddRabbitMQReceiver<TKey, TValue>(options.RabbitMQ);
            }
            else if (options.UsedKafka())
            {
                services.AddKafkaReceiver<TKey, TValue>(options.Kafka);
            }
            else if (options.UsedAzureQueue())
            {
                services.AddAzureQueueReceiver<TKey, TValue>(options.AzureQueue);
            }
            else if (options.UsedAzureServiceBus())
            {
                services.AddAzureServiceBusReceiver<TKey, TValue>(options.AzureServiceBus);
            }
            else if (options.UsedAzureEventHub())
            {
                services.AddAzureEventHubReceiver<TKey, TValue>(options.AzureEventHub);
            }
            else if (options.UsedFake())
            {
                services.AddFakeReceiver<TKey, TValue>();
            }

            return services;
        }

        public static IServiceCollection AddMessageBusSender<TKey, TValue>(this IServiceCollection services, MessageBrokerOptions options)
        {
            if (options.UsedRabbitMQ())
            {
                services.AddRabbitMQSender<TKey, TValue>(options.RabbitMQ);
            }
            else if (options.UsedKafka())
            {
                services.AddKafkaSender<TKey, TValue>(options.Kafka);
            }
            else if (options.UsedAzureQueue())
            {
                services.AddAzureQueueSender<TKey, TValue>(options.AzureQueue);
            }
            else if (options.UsedAzureServiceBus())
            {
                services.AddAzureServiceBusSender<TKey, TValue>(options.AzureServiceBus);
            }
            else if (options.UsedAzureEventHub())
            {
                services.AddAzureEventHubSender<TKey, TValue>(options.AzureEventHub);
            }
            else if (options.UsedFake())
            {
                services.AddFakeSender<TKey, TValue>();
            }

            return services;
        }
    }
}
