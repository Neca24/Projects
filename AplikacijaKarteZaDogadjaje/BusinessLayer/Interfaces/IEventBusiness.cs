using Core.Result;
using Entities;
using Entities.DTO;

namespace BusinessLayer.Interfaces
{
    public interface IEventBusiness
    {
        Task<ResultWrapper> AddEvent(EventDTO eventDTO);
        Task<List<Event>> GetAllEvents();
    }
}
