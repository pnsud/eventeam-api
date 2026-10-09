using EventApi.Models;

namespace EventApi.Services;

public interface IEventService
{
    IEnumerable<Event> GetAll();
    Event? GetById(Guid id);
    Event Create(Event newEvent);
    bool Update(Guid id, Event updated);
    bool Delete(Guid id);
}