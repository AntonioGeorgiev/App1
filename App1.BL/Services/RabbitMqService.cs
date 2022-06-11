using App1.BL.Interfaces;
using App1.Models;
using MessagePack;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace App1.BL.Services
{
    public class RabbitMqService : IRabbitMqService , IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;


        public RabbitMqService()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("Test", ExchangeType.Fanout);

            _channel.QueueDeclare("person_queue", true, false);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        public async Task PublishPersonAsync(Person person)
        {
            await Task.Factory.StartNew(() =>
            {
                var body = MessagePackSerializer.Serialize(person);

                _channel.BasicPublish("", "person_queue", body: body);
            }
            );
        }
    }
}