using System;
using System.Threading.Tasks;
using MassTransit;
using Restaurant.Messages;

namespace Restaurant.Kitchen.Consumers
{
    internal class KitchenBookingRequestedConsumer : IConsumer<IBookingRequest>
    {
        private readonly Manager _manager;

        public KitchenBookingRequestedConsumer(Manager manager)
        {
            _manager = manager;
        }
        
        public async Task Consume(ConsumeContext<IBookingRequest> context)
        {
            var rnd = new Random().Next(1000, 10000);

            if (rnd > 7000)
            {
                throw new Exception("Случилась беда!");
            }

            Console.WriteLine($"[OrderId: {context.Message.OrderId} CreationDate: {context.Message.CreationDate}]");
            Console.WriteLine("Trying time: " + DateTime.Now);

            await Task.Delay(rnd);
            
            if(_manager.CheckKitchenReady(context.Message.OrderId, context.Message.PreOrder))
                await context.Publish<IKitchenReady>(new KitchenReady(context.Message.OrderId, true));
        }
    }
}