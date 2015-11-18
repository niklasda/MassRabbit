using System;
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
    public class RequestService : ServiceControl
    {
        private IBusControl _busControl;
        readonly ILog _log = Logger.Get<RequestService>();

        public bool Start(HostControl hostControl)
        {
            NLogLogger.Use();

            var container = new Container(cfg =>
            {
                // register each consumer
                cfg.ForConcreteType<SimpleRequestConsumer>();
                cfg.ForConcreteType<ComplexRequestConsumer>();
                cfg.ForConcreteType<MessageRequestConsumer>();

                //or use StructureMap's excellent scanning capabilities
            });


            Console.WriteLine("Creating bus...");
            _log.Info("Creating bus");
            _busControl = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                IRabbitMqHost host = x.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                x.UseNLog();
                  x.AutoDelete = true;
                x.ReceiveEndpoint(host, "request_service", ec => { ec.LoadFrom(container); });

            });

            //container.Configure(cfg =>
            //{
            //    cfg.For<IBusControl>().Use(_busControl);
            //    cfg.Forward<IBus, IBusControl>();
            //});

            Console.WriteLine("Starting bus...");

            _busControl.Start();


            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Console.WriteLine("Stopping bus...");

            _busControl?.Stop();

            return true;
        }
    }
}