using Core.Result;
using Entities.DTO;

namespace BusinessLayer.Interfaces
{
    public interface ITicketBusiness
    {
        Task<IEnumerable<EventsWithTicketsDTO>> GetEventsWithTickets();
        Task<ResultWrapper> AddTicket(TicketDTO dto);
    }
}
