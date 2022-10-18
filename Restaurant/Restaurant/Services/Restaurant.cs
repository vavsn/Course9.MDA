
using Restaurants.Services;

public class Restaurant
{
    private readonly List<Table> _tables = new();
    private Messages _message = new();

    public Restaurant()
    { 
        for (ushort i =1; i <= 10;i++)
        {
            _tables.Add(new Table(i));
        }
    }

    public void BookFreeTable(int countOfPerson)
    {
        _message.ShowMessage("Добрый день! Подождите секунду, я подберу Вам столик и подтвержу Вашу бронь. Оставайтесь на линии.");

        var table = _tables.FirstOrDefault(t => t.SeatsCount >= countOfPerson 
                        && t.State == State.Free);

        table?.SetState(State.Booked);

        _message.ShowMessage(table is null
            ? $"К сожалению, все столики сейчас заняты"
            : $"Готово! Ваш столик номер {table.Id}");
    }

    public void BookFreeTableAsync(int countOfPerson)
    {
        _message.ShowMessage("Добрый день! Подождите секунду, я подберу Вам столик и подтвержу Вашу бронь. Вам придет уведомление.");

        Task.Run(async () =>
        {
            var table = _tables.FirstOrDefault(t => t.SeatsCount >= countOfPerson
                            && t.State == State.Free);

            await Task.Delay(1);
            table?.SetState(State.Booked);

            _message.ShowMessage(table is null
                ? $"УВЕДОМЛЕНИЕ: К сожалению, все столики сейчас заняты"
                : $"УВЕДОМЛЕНИЕ: Готово! Ваш столик номер {table.Id}");
        });


    }

    public void FreeBookTable(int idTable)
    {
        _message.ShowMessage("Подождите секунду, я бронь с указанного Вами столика.");

        var table = _tables.FirstOrDefault(t => t.SeatsCount >= idTable);

        table?.SetState(State.Free);

        _message.ShowMessage($"Готово! Со столика номер {table.Id} бронь снята");
    }

    public void FreeBookTableAsync(int idTable)
    {
        _message.ShowMessage("Подождите секунду, я бронь с указанного Вами столика. Вам придет уведомление.");

        Task.Run(async () =>
        {
            var table = _tables.FirstOrDefault(t => t.SeatsCount >= idTable);

            await Task.Delay(1);
            table?.SetState(State.Free);

            _message.ShowMessage($"УВЕДОМЛЕНИЕ: Со столика номер {table.Id} бронь снята");
        });
    }

    public void AutoCleanBookTableAsync(object obj)
    {
        Task.Run(async () =>
        {
            foreach(var t in _tables)
            {
                t.SetState(State.Free);
            }

            await Task.Delay(1);

            _message.ShowMessage("УВЕДОМЛЕНИЕ: Проведено автоматическое снятие бронирования столов");
        });
    }


}
