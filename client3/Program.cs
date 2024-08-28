using System.Text.Json;

namespace client3
{
    class Program 
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Показать историю сообщений за последние 10 минут? (y/n): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "n")
                    break;

                if (input.ToLower() == "y")
                {
                    var to = DateTime.UtcNow;
                    var from = to.AddMinutes(-10);
                    
                    Console.WriteLine($"Запрос отправлен: from={from:O}, to={to:O}");

                    var response = await client.GetAsync($"http://localhost:5135/api/Messages/history?from={from:O}&to={to:O}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var messages = JsonSerializer.Deserialize<MessageDto[]>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        Console.WriteLine("Сообщения за последние 10 минут:");
                        foreach (var message in messages)
                        {
                            Console.WriteLine($"{message.Timestamp}: {message.SequenceNumber} - {message.Text}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка получения сообщений: {response.StatusCode}");
                    }
                }
            }
        }
    }
}