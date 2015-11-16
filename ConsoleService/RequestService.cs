using System;
using System.Diagnostics;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Topshelf;

namespace ConsoleService
{
    public class RequestService : ServiceControl
    {
        private IBusControl _busControl;

        public bool Start(HostControl hostControl)
        {
            Debug.WriteLine("Creating bus...");

            _busControl = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                IRabbitMqHost host = x.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                x.ReceiveEndpoint(host, "request_service", e => { e.Consumer<RequestConsumer>(); });
            });

            Debug.WriteLine("Starting bus...");

            _busControl.Start();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Debug.WriteLine("Stopping bus...");

            _busControl?.Stop();

            return true;
        }
    }
}