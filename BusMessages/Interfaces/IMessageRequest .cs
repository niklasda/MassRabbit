﻿using System;

namespace BusMessages.Interfaces
{
    public interface IMessageRequest
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
}