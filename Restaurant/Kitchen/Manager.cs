﻿using System;
using MassTransit;
using Messaging;

namespace Kitchen
{
    internal class Manager
    {
        private readonly IBus _bus;

        public Manager(IBus bus)
        {
            _bus = bus;
        }

        public void CheckKitchenReady(Guid orderId, Dish? dish)
        {
            _bus.Publish<IKitchenReady>(new KitchenReady(orderId, true));
        }
    }
}