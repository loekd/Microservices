﻿using PubSub;

namespace EventTypes
{
    public class OrderCreatedEvent : IEvent
    {
        public int Id { get; set; }

        public string Product { get; set; }

        public int Quantity { get; set; }

        public int CustomerId { get; set; }

    }
}