using DataAccessLayer.Constant;
using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Task<bool> Add(Role item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Role item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Role>> GetAll()
        {
            var roles = new List<Role>();

            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using SqlCommand cmd = new("SELECT * FROM Roles", sqlConnection);
                await using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    roles.Add(new Role
                    {
                        RoleId = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }

            return roles;
        }
        
        public async Task<Role> GetById(int id)
        {
            var role = new Role();
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();
                await using SqlCommand cmd = new("SELECT * FROM Roles WHERE RoleId=@RoleId", sqlConnection);
                cmd.Parameters.AddWithValue("@RoleId", id);

                await using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    role.RoleId = reader.GetInt32(0);
                    role.Name = reader.GetString(1);
                }
            }
            return role;
        }

        public Task<bool> Update(Role item)
        {
            throw new NotImplementedException();
        }
    }
}
