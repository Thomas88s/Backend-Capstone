
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using StonkMarket.Models;



namespace StonkMarket.Repositories
{
    public class StonkRepository : BaseRepository, IStonkRepository
    {

        public StonkRepository(IConfiguration config) : base(config)
        {

        }
        
      

        public List<Stonk> GetAllStonks()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, Name, Price, Date, OneYear, FiveYear, TenYear FROM Stonk ORDER BY name";
                    var reader = cmd.ExecuteReader();

                    var stonks = new List<Stonk>();

                    while (reader.Read())
                    {
                        stonks.Add(new Stonk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            OneYear = reader.GetDecimal(reader.GetOrdinal("OneYear")),
                            FiveYear = reader.GetDecimal(reader.GetOrdinal("FiveYear")),
                            TenYear = reader.GetDecimal(reader.GetOrdinal("TenYear")),
                        });
                    }

                    reader.Close();

                    return stonks;
                }
            }
        }

        public Stonk GetStonkById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Id, Name, Price, Date, OneYear, FiveYear, TenYear
                       FROM Stonk
                       WHERE Stonk.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Stonk stonk = new Stonk
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Price = reader.GetInt32(reader.GetOrdinal("Price")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            OneYear = reader.GetInt32(reader.GetOrdinal("OneYear")),
                            FiveYear = reader.GetInt32(reader.GetOrdinal("FiveYear")),
                            TenYear = reader.GetInt32(reader.GetOrdinal("TenYear")),
                        };


                        reader.Close();
                        return stonk;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }

                }
            }
        }

        public List<Stonk> GetStonksByUserStonkId(int userStonkId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT s.Id, s.Price, s.Date, s.OneYear, s.FiveYear, s.TenYear
                FROM Stonk AS s
                JOIN UserStonk AS us ON s.Id = us.StonkId
                WHERE UserStonk.Id = @userStonkId
            ";

                    cmd.Parameters.AddWithValue("@userStonkId", userStonkId);

                    var reader = cmd.ExecuteReader();

                    List<Stonk> stonks = new List<Stonk>();

                    while (reader.Read())
                    {
                        Stonk stonk = new Stonk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Price = reader.GetInt32(reader.GetOrdinal("Price")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            OneYear = reader.GetInt32(reader.GetOrdinal("OneYear")),
                            FiveYear = reader.GetInt32(reader.GetOrdinal("FiveYear")),
                            TenYear = reader.GetInt32(reader.GetOrdinal("TenYear")),
                        };

                     

                        stonks.Add(stonk);
                    }
                    reader.Close();
                    return stonks;
                }
            }
        }




        public void AddStonkToUserStonk(int stonkId, int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO UserStonk (UserId, StonkId, NumberOfStonks, TopPerformer)
                                        OUTPUT INSERTED.ID
                                        VALUES (@userid, @stonkId, 1, 0)
                                        ";
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@stonkid", stonkId);
                    

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void DeleteStonk(int stonkId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Stonk
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", stonkId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
