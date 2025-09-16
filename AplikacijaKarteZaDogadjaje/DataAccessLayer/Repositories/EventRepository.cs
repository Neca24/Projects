using DataAccessLayer.Constant;
using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Repositories
{
    public class EventRepository : IEventRepository
    {
        public async Task<bool> Add(Event item)
        {
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using SqlCommand cmd = new("INSERT INTO EVENTS (Name,EventDate,Location) VALUES (@Name,@EventDate,@Location)", sqlConnection);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@EventDate", item.EventDate);
                cmd.Parameters.AddWithValue("@Location", item.Location);

                int res = await cmd.ExecuteNonQueryAsync();

                return res > 0;
            }
        }

        public Task<bool> Delete(Event item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetAll()
        {
            var events = new List<Event>();

            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using SqlCommand cmd = new("SELECT * FROM EVENTS", sqlConnection);
                await using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    events.Add(new Event
                    {
                        EventId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        EventDate = reader.GetDateTime(2),
                        Location = reader.GetString(3)
                    });
                }
            }

            return events;
        }

        public async Task<bool> Update(Event item)
        {
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using SqlCommand cmd = new("UPDATE EVENTS SET EventId=@EventId,Name=@Name,EventDate=@EventDate,Location=@Location",sqlConnection);
                cmd.Parameters.AddWithValue("@EventId", item.EventId);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@EventDate", item.EventDate);
                cmd.Parameters.AddWithValue("@Location", item.Location);

                int res = await cmd.ExecuteNonQueryAsync();

                return res > 0;
            }
        }
    }
}
