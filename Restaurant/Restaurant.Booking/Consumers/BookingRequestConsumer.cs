using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using Restaurant.Messages;

namespace Restaurant.Booking.Consumers
{
    public class RestaurantBookingRequestConsumer : IConsumer<Fault<IBookingRequest>>
    {
        private readonly Restaurant _restaurant;

        public RestaurantBookingRequestConsumer(Restaurant restaurant)
        {
            _restaurant = restaurant;
        }

        public async Task Consume(ConsumeContext<Fault<IBookingRequest>> context)
        {
            Console.WriteLine($"[OrderId: {context.Message.Message.OrderId}]");
            var result = await _restaurant.BookFreeTableAsync(1);
            
            await context.Publish<ITableBooked>(new TableBooked(context.Message.Message.OrderId, result ?? false));
        }
    }
}