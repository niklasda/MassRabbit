using System;
using BusMessages.Interfaces;

namespace BusMessages.Requests
{
    public class MessageRequest : IMessageRequest
    {
        readonly string _customerId;
        readonly DateTime _timestamp;

        public MessageRequest(string customerId)
        {
            _customerId = customerId;
            _timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp
        {
            get { return _timestamp; }
        }

        public string CustomerId
        {
            get { return _customerId; }
        }
    }
}