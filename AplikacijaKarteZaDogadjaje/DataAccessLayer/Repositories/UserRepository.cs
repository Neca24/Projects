using DataAccessLayer.Constant;
using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<bool> Add(User item)
        {
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using (SqlCommand cmd = new("INSERT INTO USERS (FirstName,LastName,Email,Phone,PasswordHash,PasswordSalt,RoleId) VALUES (@FirstName,@LastName,@Email,@Phone,@PasswordHash,@PasswordSalt,@RoleId)", sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", item.LastName);
                    cmd.Parameters.AddWithValue("@Email", item.Email);
                    cmd.Parameters.AddWithValue("@Phone", item.Phone);
                    cmd.Parameters.AddWithValue("@PasswordHash", item.PasswordHash);
                    cmd.Parameters.AddWithValue("@PasswordSalt", item.PasswordSalt);
                    cmd.Parameters.AddWithValue("@RoleId", item.RoleId);

                    int res = await cmd.ExecuteNonQueryAsync();

                    return res > 0;
                }
            }
        }

        public async Task<bool> Delete(User item)
        {
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using (SqlCommand cmd = new("DELETE FROM USERS WHERE UserId=@UserId", sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@UserId", item.UserId);

                    int res = await cmd.ExecuteNonQueryAsync();

                    return res > 0;
                }
            }
        }

        public async Task<List<User>> GetAll()
        {
            var users = new List<User>();

            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using (SqlCommand cmd = new("SELECT * FROM Users", sqlConnection))
                {
                    await using SqlDataReader sqlDataReader = await cmd.ExecuteReaderAsync();

                    while (await sqlDataReader.ReadAsync())
                    {
                        users.Add(new User
                        {
                            UserId = sqlDataReader.GetInt32(0),
                            FirstName = sqlDataReader.GetString(1),
                            LastName = sqlDataReader.GetString(2),
                            Email = sqlDataReader.GetString(3),
                            Phone = sqlDataReader.GetString(4),
                            PasswordHash = sqlDataReader.GetFieldValue<byte[]>(5),
                            PasswordSalt = sqlDataReader.GetFieldValue<byte[]>(6),
                            RoleId = sqlDataReader.GetInt32(7)
                        });
                    }
                }
            }
            return users;
        }

        public async Task<User?> GetByEmail(string email)
        {
            var user = new User();
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using (SqlCommand cmd = new("SELECT * FROM USERS WHERE Email=@Email", sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    await using SqlDataReader sqlDataReader = await cmd.ExecuteReaderAsync();

                    if (await sqlDataReader.ReadAsync())
                    {
                        user.UserId = sqlDataReader.GetInt32(0);
                        user.FirstName = sqlDataReader.GetString(1);
                        user.LastName = sqlDataReader.GetString(2);
                        user.Email = sqlDataReader.GetString(3);
                        user.Phone = sqlDataReader.GetString(4);
                        user.PasswordHash = sqlDataReader.GetFieldValue<byte[]>(5);
                        user.PasswordSalt = sqlDataReader.GetFieldValue<byte[]>(6);
                        user.RoleId = sqlDataReader.GetInt32(7);
                    }
                }
            }
            return user;
        }

        public async Task<User> GetById(int id)
        {
            User user = new();
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using SqlCommand cmd = new("SELECT * FROM Users WHERE UserId=@UserId", sqlConnection);
                cmd.Parameters.AddWithValue("@UserId", id);

                await using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if(await reader.ReadAsync())
                {
                    user.UserId=reader.GetInt32(0);
                    user.FirstName=reader.GetString(1);
                    user.LastName=reader.GetString(2);
                    user.Email=reader.GetString(3);
                    user.Phone = reader.GetString(4);
                    user.PasswordHash = reader.GetFieldValue<byte[]>(5);
                    user.PasswordSalt = reader.GetFieldValue<byte[]>(6);
                    user.RoleId = reader.GetInt32(7);
                }
            }

            return user;
        }

        public async Task<bool> Update(User item)
        {
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using (SqlCommand cmd = new("UPDATE USERS SET FirstName=@FirstName,LastName=@LastName,Email=@Email,Phone=@Phone,PasswordHash=@PasswordHash,PasswordSalt=@PasswordSalt,RoleId=@RoleId", sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", item.LastName);
                    cmd.Parameters.AddWithValue("@Email", item.Email);
                    cmd.Parameters.AddWithValue("@Phone", item.Phone);
                    cmd.Parameters.AddWithValue("@PasswordHash", item.PasswordHash);
                    cmd.Parameters.AddWithValue("@PasswordSalt", item.PasswordSalt);
                    cmd.Parameters.AddWithValue("@RoleId", item.RoleId);

                    int res = await cmd.ExecuteNonQueryAsync();

                    return res > 0;
                }
            }

        }
    }
}
