namespace Restaurants.Services
{
    public class Messages
    {
        public void ShowMessage(string message)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000 * 5);

                Console.WriteLine(message);
            });
        }
    }
}
