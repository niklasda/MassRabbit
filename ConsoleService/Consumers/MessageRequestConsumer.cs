using System;
using System.Threading.Tasks;
using BusMessages.Interfaces;
using MassTransit;
using MassTransit.Logging;

namespace ConsoleService.Consumers
{
    public class MessageRequestConsumer : IConsumer<IMessageRequest>
    {
        readonly ILog _log = Logger.Get<MessageRequestConsumer>();

        public async Task Consume(ConsumeContext<IMessageRequest> context)
        {
            Console.WriteLine("Consumed id {0}", context.Message.CustomerId);
            Console.WriteLine("Returning name for {0}", context.ConversationId);

            _log.Info("Consumed 2...");
        }
    }   
}