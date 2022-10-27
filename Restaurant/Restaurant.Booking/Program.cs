using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Restaurant.Booking.Consumers;
using System.Diagnostics;

namespace Restaurant.Booking
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var rest = new Restaurant();

            TimerCallback tm = new TimerCallback(rest.AutoCleanBookTableAsync);
            // создаем таймер
            Timer timer = new Timer(tm, (int)1, 0, 20000);

            while (true)
            {
                Console.WriteLine("Привет! Желаете забронировать столик?\n1 - мы уведомим Вас по смс (асинхронно)" +
                    "\n2 - Желаете снять бронь со столика?");
                if (!int.TryParse(Console.ReadLine(), out var choice) && choice is not (1 or 2 or 3 or 4))
                {
                    Console.WriteLine("Введите, пожалуйста, 1, 2.");
                    continue;
                }

                var stopWatch = new Stopwatch();
                stopWatch.Start();
                switch (choice)
                {
                    case 1:
                        rest.BookFreeTableAsync(1);
                        break;
                    case 2:
                        rest.FreeBookTableAsync(1);
                        break;
                    default:
                        Console.WriteLine("Введите, пожалуйста, 1, 2.");
                        break;
                }
                Console.WriteLine("Спасибо за Ваше обращение!");
                stopWatch.Stop();

                var ts = stopWatch.Elapsed;
                Console.WriteLine($"{ts.Seconds:00}:{ts.Milliseconds:00}");
            }

        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<RestaurantBookingRequestConsumer>()
                            .Endpoint(e =>
                            {
                                e.Temporary = true;
                            });
                        
                        x.AddConsumer<BookingRequestFaultConsumer>()
                            .Endpoint(e =>
                            {
                                e.Temporary = true;
                            });

                        x.AddSagaStateMachine<RestaurantBookingSaga, RestaurantBooking>()
                            .Endpoint(e => e.Temporary = true)
                            .InMemoryRepository();

                        x.AddDelayedMessageScheduler();

                        //x.UsingRabbitMq((context, cfg) =>
                        //{
                        //    cfg.UseDelayedMessageScheduler();
                        //    cfg.UseInMemoryOutbox();
                        //    cfg.ConfigureEndpoints(context);
                        //});


                        //_hostName = ; //hostName;
                        //HostName = _hostName,
                        //Port = 5672,
                        //UserName = "lnyrytka",
                        //Password = "HAageR1YPdExR2jKJvl8tM132tCrtBIV",
                        //VirtualHost = "lnyrytka"
        

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("chimpanzee.rmq.cloudamqp.com", "lnyrytka", h =>
                            {
                                h.Username("lnyrytka");
                                h.Password("HAageR1YPdExR2jKJvl8tM132tCrtBIV");
                            });
                        });


                    });
                    
                    services.AddMassTransitHostedService();

                    services.AddTransient<RestaurantBooking>();
                    services.AddTransient<RestaurantBookingSaga>();
                    services.AddTransient<Restaurant>();

                    services.AddHostedService<Worker>();
                });
    }
}