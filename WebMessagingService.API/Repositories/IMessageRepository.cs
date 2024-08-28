using WebMessagingService.API.Models;

namespace WebMessagingService.API.Repositories;

public interface IMessageRepository
{
    void AddMessage(Message message);
    IEnumerable<Message> GetMessages(DateTime from, DateTime to);
}