using System;
using System.Threading.Tasks;
using BusMessages.Interfaces;
using BusMessages.Responses;
using MassTransit;
using MassTransit.Logging;

namespace ConsoleService.Consumers
{
    public class ComplexRequestConsumer : IConsumer<IComplexRequest>
    {
        readonly ILog _log = Logger.Get<ComplexRequestConsumer>();

        public async Task Consume(ConsumeContext<IComplexRequest> context)
        {
            Console.WriteLine("Returning name for {0}", context.Message.CustomerId);
            Console.WriteLine("Returning name for {0}", context.ConversationId);

            _log.Info("Consuming 1...");

            context.Respond(new SimpleResponse
            {
                CustomerName = string.Format("Complex Customer Number {0}", context.Message.CustomerId)
            });
        }
    }   
}