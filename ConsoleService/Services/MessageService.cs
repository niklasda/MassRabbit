using System;
using BusMessages.Requests;
using ConsoleService.Consumers;
using MassTransit;
using MassTransit.Logging;
using MassTransit.NLogIntegration;
using MassTransit.RabbitMqTransport;
using MassTransit.NLogIntegration.Logging;
using StructureMap;
using Topshelf;

namespace ConsoleService.Services
{
    public class MessageService : ServiceControl
    {
        private IBusControl _busControl;
        readonly ILog _log = Logger.Get<MessageService>();

        public bool Start(HostControl hostControl)
        {
            NLogLogger.Use();

            var container = new Container(cfg =>
            {
                // register each consumer
                cfg.ForConcreteType<SimpleRequestConsumer>();
                cfg.ForConcreteType<ComplexRequestConsumer>();

                //or use StructureMap's excellent scanning capabilities
            });


            Console.WriteLine("Creating bus2...");
            _log.Info("Creating bus2");
            _busControl = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                IRabbitMqHost host = x.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                x.UseNLog();
                //x.ReceiveEndpoint(host, "request_service", ec => { ec.LoadFrom(container); });

                x.ReceiveEndpoint(host, "my_queue", endpoint =>
                {
                    endpoint.Handler<SimpleRequest>(async context =>
                    {
                        await Console.Out.WriteLineAsync($"Received: {context.Message.CustomerId}");
                    });
                });
            });

            //container.Configure(cfg =>
            //{
            //    cfg.For<IBusControl>().Use(_busControl);
            //    cfg.Forward<IBus, IBusControl>();
            //});

            Console.WriteLine("Starting bus2...");

            _busControl.Start();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Console.WriteLine("Stopping bus2...");

            _busControl?.Stop();

            return true;
        }
    }
}