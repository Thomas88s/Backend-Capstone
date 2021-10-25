using Microsoft.Data.SqlClient;
using StonkMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace StonkMarket.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {

        private readonly IConfiguration _config;

        public UserProfileRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public UserProfile GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, Email, FirebaseUserId, DisplayName, FirstName, LastName, CreatedDate
                                    FROM UserProfile
                                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", id);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))

                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, Email, FirebaseUserId, FirstName, LastName
                                    FROM UserProfile
                                    WHERE FirebaseUserId = @FirebaseUserId";

                    cmd.Parameters.AddWithValue("@FirebaseUserId", firebaseUserId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public void Add(UserProfile userProfile)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO
                                        UserProfile (Email, FirebaseUserId, DisplayName, FirstName, LastName, CreatedDate) 
                                        OUTPUT INSERTED.ID
                                        VALUES(@email, @firebaseUserId, @displayName, @firstName, @lastName, @createdDate)";

                    cmd.Parameters.AddWithValue("@email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@firebaseUserId", userProfile.FirebaseUserId);
                    cmd.Parameters.AddWithValue("@displayName", userProfile.DisplayName);
                    cmd.Parameters.AddWithValue("@firstName", userProfile.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", userProfile.LastName);
                    cmd.Parameters.AddWithValue("@createdDate", userProfile.CreatedDate);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE UserProfile
                            SET 
                                Email = @Email, 
                                FirstName = @firstName, 
                                LastName = @lastName, 
                                DisplayName = @displayName,
                            WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", userProfile.Id);
                    cmd.Parameters.AddWithValue("@email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@firebaseUserId", userProfile.FirebaseUserId);
                    cmd.Parameters.AddWithValue("@firstName", userProfile.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", userProfile.LastName);
                    cmd.Parameters.AddWithValue("@displayName", userProfile.DisplayName);
                    cmd.Parameters.AddWithValue("@createdDate", userProfile.CreatedDate);




                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int userProfileId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM UserProfile
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", userProfileId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
