using BusinessLayer.Interfaces;
using Core.Result;
using DataAccessLayer.Interfaces;
using Entities;
using Entities.DTO;

namespace BusinessLayer.Implementation
{
    public class TicketBusiness : ITicketBusiness
    {
        private readonly IEventRepository _eventRepo;
        private readonly ITicketRepository _ticketRepo;

        public TicketBusiness(IEventRepository eventRepository, ITicketRepository ticketRepo)
        {
            _eventRepo = eventRepository;
            _ticketRepo = ticketRepo;
        }

        public async Task<ResultWrapper> AddTicket(TicketDTO dto)
        {
            Ticket ticket = new()
            {
                Price = dto.Price,
                EventId = dto.EventId,
            };

            var result = await _ticketRepo.Add(ticket);

            if (result)
            {
                return new ResultWrapper
                {
                    Message = "Uspesno dodat",
                    Success = true
                };
            }
            else
            {
                return new ResultWrapper
                {
                    Message = "Nesupesno",
                    Success = false
                };
            }
        }

        public async Task<IEnumerable<EventsWithTicketsDTO>> GetEventsWithTickets()
        {
            var events = await _eventRepo.GetAll();
            var tickets = await _ticketRepo.GetAll();

            var result = from e in events
                         join t in tickets
                         on e.EventId equals t.EventId
                         select new EventsWithTicketsDTO
                         {
                             EventId = e.EventId,
                             EventName = e.Name,
                             EventDate = e.EventDate,
                             Location = e.Location,
                             Price = t.Price
                         };
            return result;
        }
    }
}
