using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    internal class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly Uri _uri;
        private const string exchange = @"tech-com-bd-test";

        private static List<Type> _eventTypes;
        private static Dictionary<string, Type> _handlers;

        public EventBusRabbitMQ(ILogger<EventBusRabbitMQ> logger, Uri uri, IServiceProvider provider)
        {
            _provider = provider;
            _logger = logger;
            _uri = uri;

            _eventTypes = new List<Type>();
            _handlers = new Dictionary<string, Type>();
        }

        public async Task PublishAsync(IntegrationEvent @event)
        {
            var eventName = @event.GetType().Name;
            _logger.LogInformation($"Publishing new event: {eventName}", @event);
            var factory = new ConnectionFactory() {Uri = _uri};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct);
                string message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                await Task.Run(() =>
                {
                    channel.BasicPublish(exchange: exchange,
                    routingKey: eventName,
                    basicProperties: null,
                    body: body);
                });
            }
            _logger.LogInformation($"Published event: {eventName}", @event);
        }

        public async Task SubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var factory = new ConnectionFactory() { Uri = _uri };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange, "direct");
            var queueName = channel.QueueDeclare().QueueName;
            await Task.Run(() =>
            {
                channel.QueueBind(queue: queueName,
                              exchange: exchange,
                              routingKey: eventName);
            });
            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

            if (!_handlers.ContainsKey(typeof(T).Name))
            {
                _eventTypes.Add(typeof(T));
                _handlers.Add(typeof(T).Name, typeof(TH));
            }

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
                }

                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

            var eventType = _eventTypes.FirstOrDefault(x => x.Name == eventName);
            var handlerType = _handlers[eventName];

            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
            var handlerObj = ActivatorUtilities.CreateInstance(_provider, handlerType);
            var handlerMethod = handlerType.GetMethod("HandleAsync");
            await (Task)handlerMethod.Invoke(handlerObj, new object[] { integrationEvent });
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
