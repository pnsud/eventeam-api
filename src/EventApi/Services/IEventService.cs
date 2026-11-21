using EventApi.Models;

namespace EventApi.Services;

public interface IEventService
{
    // IEnumerable<Event> GetAll(string? title, DateTime? from, DateTime? to);
    PaginatedResult<Event> GetAll(string? title, DateTime? from, DateTime? to, int page = 1, int pageSize = 10);
    Event? GetById(Guid id);
    Event Create(Event newEvent);
    bool Update(Guid id, Event updated);
    bool Delete(Guid id);
}