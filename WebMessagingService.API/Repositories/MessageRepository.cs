using Npgsql;
using WebMessagingService.API.Models;

namespace WebMessagingService.API.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly string _connectionString;

    public MessageRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public void AddMessage(Message message)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var command = new NpgsqlCommand(
            "INSERT INTO Messages (Text, Timestamp, SequenceNumber) VALUES (@text, @timestamp, @sequenceNumber)", connection);
        command.Parameters.AddWithValue("text", message.Text);
        command.Parameters.AddWithValue("timestamp", message.Timestamp);
        command.Parameters.AddWithValue("sequenceNumber", message.SequenceNumber);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public IEnumerable<Message> GetMessages(DateTime from, DateTime to)
    {
        var messages = new List<Message>();
        using var connection = new NpgsqlConnection(_connectionString);
        var command = new NpgsqlCommand("SELECT * FROM Messages WHERE Timestamp BETWEEN @from AND @to", connection);
        command.Parameters.AddWithValue("from", from);
        command.Parameters.AddWithValue("to", to);
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            messages.Add(new Message
            {
                Id = reader.GetInt32(0),
                Text = reader.GetString(1),
                Timestamp = reader.GetDateTime(2),
                SequenceNumber = reader.GetInt32(3)
            });
        }
        return messages;
    }
}