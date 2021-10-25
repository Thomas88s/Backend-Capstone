using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public void Add(Stonk stonk)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Stonk (Name, Price, Date, OneYear, FiveYear, TenYear)
                        OUTPUT INSERTED.ID
                        VALUES (@name, @price, @date, @oneYear, @fiveYear, @tenYear);
                        ";

                    cmd.Parameters.AddWithValue("@name", stonk.Name);
                    cmd.Parameters.AddWithValue("@price", stonk.Price);
                    cmd.Parameters.AddWithValue("@date", stonk.Date);
                    cmd.Parameters.AddWithValue("@oneYear", stonk.OneYear);
                    cmd.Parameters.AddWithValue("@fiveYear", stonk.FiveYear);
                    cmd.Parameters.AddWithValue("@tenYear", stonk.TenYear);
                    ;

                    int id = (int)cmd.ExecuteScalar();
                    stonk.Id = id;
                }
            }
        }

        public void Update(Stonk stonk)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Stonk
                            SET 
                                [Name] = @name
                                Price = @price
                                Date = @date
                                OneYear = @oneYear
                                FiveYear = @fiveYear
                                TenYear = @tenYear
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", stonk.Name);
                    cmd.Parameters.AddWithValue("@id", stonk.Id);
                    cmd.Parameters.AddWithValue("@price", stonk.Price);
                    cmd.Parameters.AddWithValue("@date", stonk.Date);
                    cmd.Parameters.AddWithValue("@oneYear", stonk.OneYear);
                    cmd.Parameters.AddWithValue("@fiveYear", stonk.FiveYear);
                    cmd.Parameters.AddWithValue("@tenYear", stonk.TenYear);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int stonkId)
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
