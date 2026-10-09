namespace EventApi.Models;

public class Event
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}

// Хорошие практики (необязательно для зачёта):
// DTO для запроса/ответа:
// то, что приходит от клиента
// public class CreateEventRequest
// {
//     public string Title { get; set; } = string.Empty;
//     public string? Description { get; set; }
//     public DateTime StartAt { get; set; }
//     public DateTime EndAt { get; set; }
// }

// // то, что уходит клиенту
// public class EventResponse
// {
//     public Guid Id { get; set; }
//     public string Title { get; set; } = string.Empty;
//     public string? Description { get; set; }
//     public DateTime StartAt { get; set; }
//     public DateTime EndAt { get; set; }
// }
