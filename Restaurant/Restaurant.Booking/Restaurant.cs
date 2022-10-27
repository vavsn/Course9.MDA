using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Booking
{
    public class Restaurant
    {
        private readonly List<Table> _tables = new ();
        //private Messages _message = new();
        //private readonly Producer _producer =
        //    new("BookingNotification", "localhost");

        public Restaurant()
        {
            for (ushort i = 1; i <= 10; i++)
            {
                _tables.Add(new Table(i));
            }
        }

        public async Task<bool?> BookFreeTableAsync(int countOfPersons)
        {
            Console.WriteLine($"Спасибо за Ваше обращение, я подберу столик и подтвержу вашу бронь," +
                              "Вам придет уведомление");

            var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfPersons
                                                        && t.State == TableState.Free);
            return table?.SetState(TableState.Booked);
        }

        public async Task<bool?> FreeBookTableAsync(int countOfPersons)
        {
            Console.WriteLine($"Спасибо за Ваше обращение, я снимаю бронь с указанного Вами столика." +
                              "Вам придет уведомление");

            var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfPersons
                                                        && t.State == TableState.Booked);
            return table?.SetState(TableState.Free);
        }

        public void AutoCleanBookTableAsync(object obj)
        {
            Task.Run(async () =>
            {
                foreach (var t in _tables)
                {
                    t?.SetState(TableState.Free);
                }

                await Task.Delay(1);
            });
        }

    }
}