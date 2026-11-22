using EventApi.Models;
using EventApi.Services;

namespace EventApi.Tests;

public class EventServiceTests
{
    private readonly EventService _service = new();

    private static Event Event(string title, DateTime start, DateTime end) => new()
    {
        Title = title,
        StartAt = start,
        EndAt = end
    };

    [Fact]
    public void Create_AssignsId_AndStoresEvent()
    {
        var created = _service.Create(Event("Meetup", new DateTime(2026, 3, 1, 10, 0, 0), new DateTime(2026, 3, 1, 12, 0, 0)));

        Assert.NotEqual(Guid.Empty, created.Id);
        Assert.Equal(created.Id, _service.GetById(created.Id)!.Id);
    }

    [Fact]
    public void GetAll_ReturnsCreatedEvents()
    {
        _service.Create(Event("Meetup", new DateTime(2026, 3, 1, 10, 0, 0), new DateTime(2026, 3, 1, 12, 0, 0)));
        _service.Create(Event("Standup", new DateTime(2026, 4, 1, 10, 0, 0), new DateTime(2026, 4, 1, 11, 0, 0)));

        var result = _service.GetAll(null, null, null);

        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.Items.Count);
    }

    [Fact]
    public void GetById_ReturnsEvent()
    {
        var created = _service.Create(Event("Meetup", new DateTime(2026, 3, 1, 10, 0, 0), new DateTime(2026, 3, 1, 12, 0, 0)));

        var found = _service.GetById(created.Id);

        Assert.NotNull(found);
        Assert.Equal("Meetup", found.Title);
    }

    [Fact]
    public void Update_ChangesExistingEvent()
    {
        var created = _service.Create(Event("Meetup", new DateTime(2026, 3, 1, 10, 0, 0), new DateTime(2026, 3, 1, 12, 0, 0)));

        var updated = _service.Update(created.Id, Event("Meetup 2", new DateTime(2026, 3, 2, 10, 0, 0), new DateTime(2026, 3, 2, 12, 0, 0)));

        Assert.True(updated);
        Assert.Equal("Meetup 2", _service.GetById(created.Id)!.Title);
    }

    [Fact]
    public void Delete_RemovesExistingEvent()
    {
        var created = _service.Create(Event("Meetup", new DateTime(2026, 3, 1, 10, 0, 0), new DateTime(2026, 3, 1, 12, 0, 0)));

        var deleted = _service.Delete(created.Id);

        Assert.True(deleted);
        Assert.Null(_service.GetById(created.Id));
    }

    [Fact]
    public void GetAll_FiltersByTitle()
    {
        _service.Create(Event("Meetup", new DateTime(2026, 3, 1, 10, 0, 0), new DateTime(2026, 3, 1, 12, 0, 0)));
        _service.Create(Event("Standup", new DateTime(2026, 4, 1, 10, 0, 0), new DateTime(2026, 4, 1, 11, 0, 0)));

        var result = _service.GetAll("meet", null, null);

        Assert.Equal(new[] { "Meetup" }, result.Items.Select(e => e.Title));
    }

    [Fact]
    public void GetAll_FiltersByDates()
    {
        _service.Create(Event("Meetup", new DateTime(2026, 3, 1, 10, 0, 0), new DateTime(2026, 3, 1, 12, 0, 0)));
        _service.Create(Event("Standup", new DateTime(2026, 4, 1, 10, 0, 0), new DateTime(2026, 4, 1, 11, 0, 0)));

        var result = _service.GetAll(null, new DateTime(2026, 3, 15), new DateTime(2026, 4, 30));

        Assert.Equal(new[] { "Standup" }, result.Items.Select(e => e.Title));
    }

    [Fact]
    public void GetAll_Paginates()
    {
        _service.Create(Event("A", new DateTime(2026, 1, 1, 10, 0, 0), new DateTime(2026, 1, 1, 11, 0, 0)));
        _service.Create(Event("B", new DateTime(2026, 1, 2, 10, 0, 0), new DateTime(2026, 1, 2, 11, 0, 0)));
        _service.Create(Event("C", new DateTime(2026, 1, 3, 10, 0, 0), new DateTime(2026, 1, 3, 11, 0, 0)));

        var result = _service.GetAll(null, null, null, page: 2, pageSize: 2);

        Assert.Equal(3, result.TotalCount);
        Assert.Equal(2, result.Page);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(new[] { "C" }, result.Items.Select(e => e.Title));
    }

    [Fact]
    public void GetAll_CombinesFilters()
    {
        _service.Create(Event("Meetup", new DateTime(2026, 3, 1, 10, 0, 0), new DateTime(2026, 3, 1, 12, 0, 0)));
        _service.Create(Event("Meetup later", new DateTime(2026, 5, 1, 10, 0, 0), new DateTime(2026, 5, 1, 12, 0, 0)));

        var result = _service.GetAll("meet", new DateTime(2026, 4, 1), null);

        Assert.Equal(new[] { "Meetup later" }, result.Items.Select(e => e.Title));
    }

    [Fact]
    public void GetById_UnknownId_ReturnsNull()
    {
        Assert.Null(_service.GetById(Guid.NewGuid()));
    }

    [Fact]
    public void Update_UnknownId_ReturnsFalse()
    {
        var updated = _service.Update(
            Guid.NewGuid(),
            Event("Meetup", new DateTime(2026, 3, 1, 10, 0, 0), new DateTime(2026, 3, 1, 12, 0, 0)));

        Assert.False(updated);
    }
}