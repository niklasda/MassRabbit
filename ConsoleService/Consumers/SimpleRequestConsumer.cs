using System;
using System.Threading.Tasks;
using BusMessages.Interfaces;
using BusMessages.Responses;
using ConsoleService.Services;
using MassTransit;
using MassTransit.Logging;

namespace ConsoleService.Consumers
{
    public class SimpleRequestConsumer : IConsumer<ISimpleRequest>
    {
        readonly ILog _log = Logger.Get<RequestService>();

        public async Task Consume(ConsumeContext<ISimpleRequest> context)
        {
            Console.WriteLine("Returning name for {0}", context.Message.CustomerId);

            _log.Info("Consuming...");
            
            context.Respond(new SimpleResponse
            {
                CustomerName = string.Format("Customer Number {0}", context.Message.CustomerId)
            });
        }
    }   
}