using StonkMarket.Models;
using System.Collections.Generic;

namespace StonkMarket.Repositories
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(int messageId);
        List<Message> GetAllMessages();
        List<Message> GetAllMessagesByUserId(int userProfileId);
        void UpdateMessage(Message message);
    }
}