using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
                                        LEFT JOIN UserProfile AS up ON SenderId = up.id
                                        Left UserProfile AS up ON ReceiverId = up.id
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
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("Date")),
                        });
                    }

                    reader.Close();

                    return messages;
                }
            }
        }
        public Message GetMessageById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT m.id, m.Content, m.Date, m.SenderId, m.ReceiverId, m.CreatedDate 
                                        FROM Message AS m
                                        LEFT JOIN UserProfile AS up ON SenderId = up.id
                                        Left UserProfile AS up ON ReceiverId = up.id                                 
                                        WHERE Message.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Message message = new Message
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            SenderId = reader.GetInt32(reader.GetOrdinal("SenderId")),
                            ReceiverId = reader.GetInt32(reader.GetOrdinal("ReceiverId")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("Date")),
                        };


                        reader.Close();
                        return message;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }

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
                    VALUES (@content, @date, @senderID, @receiverId);
                ";

                    cmd.Parameters.AddWithValue("@content", message.Content);
                    cmd.Parameters.AddWithValue("@date", message.Date);
                    cmd.Parameters.AddWithValue("@senderId", message.SenderId);
                    cmd.Parameters.AddWithValue("@receiverId", message.ReceiverId);


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
