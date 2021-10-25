using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StonkMarket.Models;

namespace StonkMarket.Repositories
{
    public class TopStonkRepository : BaseRepository, ITopStonkRepository
    {

        public TopStonkRepository(IConfiguration config) : base(config)
        {

        }



        public List<TopStonk> GetAllTopStonks()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.id, t.StonkId, t.UserId, up.FirstName, t.PercentageIncrease, t.TopPerformer, s.Name, s.Price, s.Date, s.OneYear, s.FiveYear, s.TenYear
                                        FROM TopStonk AS t 
                                        LEFT JOIN Stonk AS s ON t.StonkId = s.Id
                                        LEFT JOIN UserProfile AS up ON t.UserId = up.id
                                        ORDER BY s.name";
                    var reader = cmd.ExecuteReader();

                    var stonks = new List<TopStonk>();

                    while (reader.Read())
                    {
                        stonks.Add(new TopStonk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StonkId = reader.GetInt32(reader.GetOrdinal("StonkId")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            TopPerformer = reader.GetBoolean(reader.GetOrdinal("TopPerformer")),
                            PercentageIncrease = reader.GetInt32(reader.GetOrdinal("PercentageIncrease"))
                        });
                    }

                    reader.Close();

                    return stonks;
                }
            }
        }

        public TopStonk GetTopStonkById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT t.id, t.StonkId, t.UserId, up.FirstName, t.TopPerformer, s.Name, s.Price, s.Date, s.OneYear, s.FiveYear, s.TenYear
                       FROM TopStonk AS t
                       LEFT JOIN Stonk AS s ON t.StonkId = s.Id
                       LEFT JOIN UserProfile AS up ON t.UserId = up.id
                       WHERE TopStonk.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        TopStonk topStonk = new TopStonk
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StonkId = reader.GetInt32(reader.GetOrdinal("StonkId")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            TopPerformer = reader.GetBoolean(reader.GetOrdinal("TopPerformer")),
                            PercentageIncrease = reader.GetInt32(reader.GetOrdinal("PercentageIncrease"))
                        };


                        reader.Close();
                        return topStonk;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }

                }
            }
        }

        public void Add(TopStonk topStonk)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO TopStonk (StockId, UserId, TopPerformer, PercentageIncrease)
                        OUTPUT INSERTED.ID
                        VALUES (@stockId, @userId, @TopPerformer, @PercentageIncrease);
                        ";

                    cmd.Parameters.AddWithValue("@stonkId", topStonk.StonkId);
                    cmd.Parameters.AddWithValue("@userId", topStonk.UserId);
                    cmd.Parameters.AddWithValue("@topPerformer", topStonk.TopPerformer);
                    cmd.Parameters.AddWithValue("@percentageIncrease", topStonk.PercentageIncrease);
                    ;

                    int id = (int)cmd.ExecuteScalar();
                    topStonk.Id = id;
                }
            }
        }

        public void Update(TopStonk topStonk)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE TopStonk
                            SET 
                                StonkId = @stonkId
                                UserID = @userId
                                TopPerformer = @topPerformer
                                PercentageIncrease = @percentageIncrease
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", topStonk.Id);
                    cmd.Parameters.AddWithValue("@stonkId", topStonk.StonkId);
                    cmd.Parameters.AddWithValue("@userId", topStonk.UserId);
                    cmd.Parameters.AddWithValue("@topPerformer", topStonk.TopPerformer);
                    cmd.Parameters.AddWithValue("@percentageIncrease", topStonk.PercentageIncrease);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int topStonkId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM TopStonk
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", topStonkId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
