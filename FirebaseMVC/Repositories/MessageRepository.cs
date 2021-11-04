using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using StonkMarket.Models;

namespace StonkMarket.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {

        public MessageRepository(IConfiguration config) : base(config)
        {

        }

        public List<Message> GetAllMessages()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT m.id, m.Content, m.Date, m.SenderId, m.ReceiverId, m.CreatedDate 
                                        FROM Message AS m
                                        LEFT JOIN UserProfile AS up ON m.SenderId = up.id
                                       
                                        ";
                    var reader = cmd.ExecuteReader();

                    var messages = new List<Message>();

                    while (reader.Read())
                    {
                        messages.Add(new Message()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            SenderId = reader.GetInt32(reader.GetOrdinal("SenderId")),
                            ReceiverId = reader.GetInt32(reader.GetOrdinal("ReceiverId")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                        });
                    }

                    reader.Close();

                    return messages;
                }
            }
        }
        public List<Message> GetAllMessagesByUserId(int userProfileId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT m.id, m.Content, m.Date, m.SenderId, m.ReceiverId, m.CreatedDate 
                                        FROM Message AS m
                                        LEFT JOIN UserProfile AS up ON SenderId = up.id
                                     
                                        WHERE m.SenderId = @UserProfileId";

                    cmd.Parameters.AddWithValue("@UserProfileId", userProfileId);
                    var reader = cmd.ExecuteReader();

                    var messages = new List<Message>();

                    while (reader.Read())
                    {
                        messages.Add(new Message()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            SenderId = reader.GetInt32(reader.GetOrdinal("SenderId")),
                            ReceiverId = reader.GetInt32(reader.GetOrdinal("ReceiverId")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("Date")),

                            UserProfile = new UserProfile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                            }
                        });

                    }
                        reader.Close();
                        return messages;
                    }
                
               
            }
        }



        public void AddMessage(Message message)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Message (Content, Date, SenderId, ReceiverId, CreatedDate)
                    OUTPUT INSERTED.ID
                    VALUES (@content, @date, 1, 1, @createDate);
                ";

                    cmd.Parameters.AddWithValue("@content", message.Content);
                    cmd.Parameters.AddWithValue("@date", message.Date);
                    cmd.Parameters.AddWithValue("@senderId", message.SenderId);
                    cmd.Parameters.AddWithValue("@receiverId", message.ReceiverId);
                    cmd.Parameters.AddWithValue("@createdDate", message.CreatedDate);


                    int id = (int)cmd.ExecuteScalar();

                    message.Id = id;
                }
            }
        }

        public void UpdateMessage(Message message)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Owner
                            SET 
                                Content = @content, 
                                SenderId = @senderId, 
                                ReceiverId = @receiverId, 
                                Date = @date,                               
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@content", message.Content);
                    cmd.Parameters.AddWithValue("@date", message.Date);
                    cmd.Parameters.AddWithValue("@senderId", message.SenderId);
                    cmd.Parameters.AddWithValue("@receiverId", message.ReceiverId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMessage(int messageId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Message
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", messageId);

                    cmd.ExecuteNonQuery();
                }
            }
        }       
    }
}
