using System.Text;
using System.Text.Json;

namespace client1
{
    class Program 
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите сообщение (или 'exit' для выхода): ");
                string text = Console.ReadLine();

                if (text.ToLower() == "exit")
                    break;

                var message = new MessageDto
                {
                    Text = text,
                    SequenceNumber = GetSequenceNumber()
                };

                var jsonContent = JsonSerializer.Serialize(message);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:5135/api/Messages/send", content);

                if (response.IsSuccessStatusCode)
                    Console.WriteLine("Сообщение отправлено!");
                else
                    Console.WriteLine($"Ошибка отправки сообщения: {response.StatusCode}");
            }
        }

        private static int GetSequenceNumber()
        {
            return new Random().Next(1, 1000);
        }
    }
}

