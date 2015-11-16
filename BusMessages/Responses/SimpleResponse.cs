using BusMessages.Interfaces;

namespace BusMessages.Responses
{
    public class SimpleResponse : ISimpleResponse
    {
        public string CustomerName { get; set; }
    }
}