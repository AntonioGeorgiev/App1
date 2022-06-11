using App1.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App1.BL.Services
{
    public class KafkaConsumer : IHostedService
    {
        private IConsumer<byte[], Person> _consumer;

        public KafkaConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost",
                AutoCommitIntervalMs = 5000,
                FetchWaitMaxMs = 50,
                GroupId = Guid.NewGuid().ToString(),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
                ClientId = "2"
            };

            _consumer = new ConsumerBuilder<byte[], Person>(config)
                            .SetValueDeserializer(new MsgPackDeserializer<Person>())
                            .Build();
        }

        void Print(string s)
        {
            Console.WriteLine(s);
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe("Persons");
            Task.Factory.StartNew(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = _consumer.Consume(cancellationToken);
                        PersonList.person.Add(result.Message.Value);
                        Console.WriteLine("Incoming message:");
                        Console.WriteLine($"Person with name: {result.Message.Value.Name}, with age of: {result.Message.Value.Age} \n");
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"Error: {ex.Error.Reason}");
                    }
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}