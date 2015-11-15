using System;
using System.Threading.Tasks;
using BusMessages;
using MassTransit;

namespace ConsoleService
{
    public class RequestConsumer : IConsumer<ISimpleRequest>
    {
        public async Task Consume(ConsumeContext<ISimpleRequest> context)
        {
            Console.WriteLine("Returning name for {0}", context.Message.CustomerId);

            context.Respond(new SimpleResponse
            {
                CusomerName = string.Format("Customer Number {0}", context.Message.CustomerId)
            });
        }
    }   
}