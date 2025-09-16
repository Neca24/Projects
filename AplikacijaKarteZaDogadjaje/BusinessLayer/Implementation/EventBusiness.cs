using BusinessLayer.Interfaces;
using Core.Result;
using DataAccessLayer.Interfaces;
using Entities;
using Entities.DTO;

namespace BusinessLayer.Implementation
{
    public class EventBusiness : IEventBusiness
    {
        private readonly IEventRepository _eventRepository;

        public EventBusiness(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<ResultWrapper> AddEvent(EventDTO eventDTO)
        {
            Event e = new()
            {
                Name = eventDTO.Name,
                EventDate = eventDTO.EventDate,
                Location = eventDTO.Location
            };

            var result = await _eventRepository.Add(e);
            if (result)
            {
                return new ResultWrapper
                {
                    Success = true,
                    Message = "Dogadjaj usepsno dodat"
                };
            }
            else
            {
                return new ResultWrapper
                {
                    Success = false,
                    Message = "Dogadjaj nije dodat"
                };
            }
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _eventRepository.GetAll();
        }
    }
}
