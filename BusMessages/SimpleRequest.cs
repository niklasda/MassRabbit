using System;

namespace BusMessages
{
    public interface ISimpleRequest
    {
        /// <summary>
        /// When the request was created
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// The customer id for the request (or whatever data you want here)
        /// </summary>
        string CustomerId { get; }
    }

    public class SimpleRequest : ISimpleRequest
    {
        readonly string _customerId;
        readonly DateTime _timestamp;

        public SimpleRequest(string customerId)
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