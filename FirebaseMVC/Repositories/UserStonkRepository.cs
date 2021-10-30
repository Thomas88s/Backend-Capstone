using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StonkMarket.Models;

namespace StonkMarket.Repositories
{
    public class UserStonkRepository : BaseRepository, IUserStonkRepository
    {

        public UserStonkRepository(IConfiguration config) : base(config)
        {

        }



        public List<UserStonk> GetAllUserStonks()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.id, u.StonkId, u.UserId, u.NumberOfStonks, up.FirstName, up.id, up.DisplayName, u.TopPerformer, s.id, s.Name, s.Price, s.Date, s.OneYear, s.FiveYear, s.TenYear
                                        FROM UserStonk AS u 
                                        LEFT JOIN Stonk AS s ON u.StonkId = s.Id
                                        LEFT JOIN UserProfile AS up ON u.UserId = up.id
                                        ORDER BY S.name";
                    var reader = cmd.ExecuteReader();

                    var stonks = new List<UserStonk>();

                    while (reader.Read())
                    {
                        stonks.Add(new UserStonk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StonkId = reader.GetInt32(reader.GetOrdinal("StonkId")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            NumberOfStonks = reader.GetInt32(reader.GetOrdinal("NumberOfStonks")),
                            TopPerformer = reader.GetBoolean(reader.GetOrdinal("TopPerformer")),
                            Stonk = new Stonk()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                OneYear = reader.GetDecimal(reader.GetOrdinal("OneYear")),
                                FiveYear = reader.GetDecimal(reader.GetOrdinal("FiveYear")),
                                TenYear = reader.GetDecimal(reader.GetOrdinal("TenYear")),
                            },
                            UserProfile = new UserProfile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                            }
                        }) ;
                    }

                    reader.Close();

                    return stonks;
                }
            }
        }

        public List<UserStonk> GetAllUserStonksById(int userProfileId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.id, u.StonkId, u.UserId, u.NumberOfStonks, up.FirstName, up.id, up.DisplayName, u.TopPerformer, s.id, s.Name, s.Price, s.Date, s.OneYear, s.FiveYear, s.TenYear
                                        FROM UserStonk AS u 
                                        LEFT JOIN Stonk AS s ON u.StonkId = s.Id
                                        LEFT JOIN UserProfile AS up ON u.UserId = up.id
                                        WHERE u.UserId = @UserProfileId";

                    cmd.Parameters.AddWithValue("@UserProfileId", userProfileId);
                    var reader = cmd.ExecuteReader();

                    var stonks = new List<UserStonk>();

                    while (reader.Read())
                    {
                        stonks.Add(new UserStonk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StonkId = reader.GetInt32(reader.GetOrdinal("StonkId")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            NumberOfStonks = reader.GetInt32(reader.GetOrdinal("NumberOfStonks")),
                            TopPerformer = reader.GetBoolean(reader.GetOrdinal("TopPerformer")),
                            Stonk = new Stonk()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                OneYear = reader.GetDecimal(reader.GetOrdinal("OneYear")),
                                FiveYear = reader.GetDecimal(reader.GetOrdinal("FiveYear")),
                                TenYear = reader.GetDecimal(reader.GetOrdinal("TenYear")),
                            },
                            UserProfile = new UserProfile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                            }
                        });
                    }

                    reader.Close();

                    return stonks;
                }
            }
        }

        public UserStonk GetUserStonkById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                      SELECT u.id, u.StonkId, u.UserId, u.NumberOfStonks, up.FirstName, up.id, up.DisplayName, u.TopPerformer, s.id, s.Name, s.Price, s.Date, s.OneYear, s.FiveYear, s.TenYear
                                        FROM UserStonk AS u 
                                        LEFT JOIN Stonk AS s ON u.StonkId = s.id
                                        LEFT JOIN UserProfile AS up ON u.UserId = up.id
                       WHERE u.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        UserStonk userStonk = new UserStonk
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StonkId = reader.GetInt32(reader.GetOrdinal("StonkId")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            NumberOfStonks = reader.GetInt32(reader.GetOrdinal("NumberOfStonks")),
                            TopPerformer = reader.GetBoolean(reader.GetOrdinal("TopPerformer")),
                            Stonk = new Stonk()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                OneYear = reader.GetDecimal(reader.GetOrdinal("OneYear")),
                                FiveYear = reader.GetDecimal(reader.GetOrdinal("FiveYear")),
                                TenYear = reader.GetDecimal(reader.GetOrdinal("TenYear")),
                            },
                            UserProfile = new UserProfile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                            }
                        };


                        reader.Close();
                        return userStonk;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }

                }
            }
        }

        public List<UserStonk> GetTopStonks()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.id, u.StonkId, u.UserId, u.NumberOfStonks, up.FirstName, up.id, up.DisplayName, u.TopPerformer, s.id, s.Name, s.Price, s.Date, s.OneYear, s.FiveYear, s.TenYear
                                        FROM UserStonk AS u 
                                        LEFT JOIN Stonk AS s ON u.StonkId = s.Id
                                        LEFT JOIN UserProfile AS up ON u.UserId = up.id
                                        Where u.TopPerformer = 'true'";
                    var reader = cmd.ExecuteReader();

                    var stonks = new List<UserStonk>();

                    while (reader.Read())
                    {
                        stonks.Add(new UserStonk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StonkId = reader.GetInt32(reader.GetOrdinal("StonkId")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            NumberOfStonks = reader.GetInt32(reader.GetOrdinal("NumberOfStonks")),
                            TopPerformer = reader.GetBoolean(reader.GetOrdinal("TopPerformer")),
                            Stonk = new Stonk()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                OneYear = reader.GetDecimal(reader.GetOrdinal("OneYear")),
                                FiveYear = reader.GetDecimal(reader.GetOrdinal("FiveYear")),
                                TenYear = reader.GetDecimal(reader.GetOrdinal("TenYear")),
                            },
                            UserProfile = new UserProfile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                            }
                        });
                    }

                    reader.Close();

                    return stonks;
                }
            }
        }

        public void Add(UserStonk userStonk)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserStonk (StockId, UserId, NumberOfStonks, TopPerformer)
                        OUTPUT INSERTED.ID
                        VALUES (@stockId, @userId, @PercentageIncrease, @TopPerformer);
                        ";

                    cmd.Parameters.AddWithValue("@stonkId", userStonk.StonkId);
                    cmd.Parameters.AddWithValue("@userId", userStonk.UserId);
                    cmd.Parameters.AddWithValue("@numberOfStonks", userStonk.UserId);
                    cmd.Parameters.AddWithValue("@date", userStonk.TopPerformer);
                    ;

                    int id = (int)cmd.ExecuteScalar();
                    userStonk.Id = id;
                }
            }
        }

        public void Update(UserStonk userStonk)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE UserStonk
                            SET 
                                StonkId = @stonkId,
                                UserId = @userId,
                                NumberOfStonks = @numberOfStonks,
                                TopPerformer = @topPerformer
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", userStonk.Id);
                    cmd.Parameters.AddWithValue("@stonkId", userStonk.StonkId);
                    cmd.Parameters.AddWithValue("@userId", userStonk.UserId);
                    cmd.Parameters.AddWithValue("@numberOfStonks", userStonk.NumberOfStonks);
                    cmd.Parameters.AddWithValue("@topPerformer", userStonk.TopPerformer);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int userStonkId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM UserStonk
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", userStonkId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
