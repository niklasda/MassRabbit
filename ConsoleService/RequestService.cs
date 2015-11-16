using System;
using MassTransit;
using MassTransit.Logging;
using MassTransit.NLogIntegration;
using MassTransit.RabbitMqTransport;
using MassTransit.NLogIntegration.Logging;
using Topshelf;

namespace ConsoleService
{
    public class RequestService : ServiceControl
    {
        private IBusControl _busControl;
        readonly ILog _log = Logger.Get<RequestService>();

        public bool Start(HostControl hostControl)
        {
            NLogLogger.Use();

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
                x.ReceiveEndpoint(host, "request_service", e => { e.Consumer<RequestConsumer>(); });
            });
            
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