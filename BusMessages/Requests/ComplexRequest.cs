using System;
using BusMessages.Interfaces;

namespace BusMessages.Requests
{
    public class ComplexRequest : IComplexRequest
    {
        readonly string _customerId;
        readonly DateTime _timestamp;

        public ComplexRequest(string customerId)
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