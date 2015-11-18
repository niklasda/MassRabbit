using System;
using System.Threading.Tasks;
using BusMessages.Interfaces;
using BusMessages.Responses;
using MassTransit;
using MassTransit.Logging;

namespace ConsoleService.Consumers
{
    public class SimpleRequestConsumer : IConsumer<ISimpleRequest>
    {
        readonly ILog _log = Logger.Get<SimpleRequestConsumer>();

        public async Task Consume(ConsumeContext<ISimpleRequest> context)
        {
            Console.WriteLine("Returning name for {0}", context.Message.CustomerId);
            Console.WriteLine("Returning name for {0}", context.ConversationId);

            _log.Info("Consuming 3...");
            
            context.Respond(new SimpleResponse
            {
                CustomerName = string.Format("Customer Number {0}", context.Message.CustomerId)
            });
        }
    }   
}