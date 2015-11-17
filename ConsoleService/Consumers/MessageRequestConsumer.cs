using System;
using System.Threading.Tasks;
using BusMessages.Interfaces;
using BusMessages.Responses;
using ConsoleService.Services;
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

            _log.Info("Consumed...");
            
            //context.Respond(new SimpleResponse
            //{
            //    CustomerName = string.Format("Customer Number {0}", context.Message.CustomerId)
            //});
        }
    }   
}