using DataAccessLayer.Constant;
using DataAccessLayer.Interfaces;
using Entities;
using Entities.Enums;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        public Task<bool> Add(Reservation item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Reservation item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reservation>> GetAll()
        {
            var reservations = new List<Reservation>();

            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using SqlCommand cmd = new("SELECT * FROM RESERVATIONS");
                await using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while(await reader.ReadAsync())
                {
                    reservations.Add(new Reservation
                    {
                        ReservationId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        TicketId = reader.GetInt32(2),
                        ReservationDate = reader.GetDateTime(3),
                        ReservationStatus = Enum.TryParse<Status>(reader.GetString(4), out var status) ? status : Status.PENDING
                    });
                }
            }

            return reservations;
        }

        public async Task<bool> Update(Reservation item)
        {
            await using (SqlConnection sqlConnection = new(ConnectionBase.ConnectionString))
            {
                await sqlConnection.OpenAsync();

                await using SqlCommand cmd = new("UPDATE RESERVATIONS SET ReservationId=@ReservationId,UserId=@UserId,TicketId=@TicketId,ReservationDate=@ReservationDate,ReservationStatus=@ReservationStatus");
                cmd.Parameters.AddWithValue("@ReservationId", item.ReservationId);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                cmd.Parameters.AddWithValue("@TicketId", item.TicketId);
                cmd.Parameters.AddWithValue("@ReservationDate", item.ReservationDate);
                cmd.Parameters.AddWithValue("@ReservationStatus", item.ReservationStatus);

                int res = await cmd.ExecuteNonQueryAsync();

                return res > 0;
            }
        }
    }
}
