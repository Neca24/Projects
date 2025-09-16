using DataAccessLayer.Constant;
using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public async Task<bool> Add(Ticket item)
        {
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();
                await using SqlCommand cmd = new("INSERT INTO TICKETS (Price,EventId) VALUES (@Price,@EventId)",sqlConnection);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@EventId", item.EventId);

                int res = await cmd.ExecuteNonQueryAsync();

                return res > 0;
            }
        }

        public async Task<bool> Delete(Ticket item)
        {
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using SqlCommand cmd = new("DELETE FROM TICKETS WHERE TicketId=@TicketId",sqlConnection);
                cmd.Parameters.AddWithValue("@TicketId", item.TicketId);

                int res = await cmd.ExecuteNonQueryAsync();

                return res > 0;
            }
        }

        public async Task<List<Ticket>> GetAll()
        {
            var tickets = new List<Ticket>();

            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();
                await using SqlCommand cmd = new("SELECT * FROM TICKETS", sqlConnection);
                await using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    tickets.Add(new Ticket
                    {
                        TicketId = reader.GetInt32(0),
                        Price = reader.GetDecimal(1),
                        EventId = reader.GetInt32(2)
                    });
                }
            }

            return tickets;
        }

        public Task<bool> Update(Ticket item)
        {
            throw new NotImplementedException();
        }
    }
}
