using System.ComponentModel.DataAnnotations;

namespace EventApi.Models;

public class EventRequest : IValidatableObject
{
    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public DateTime? StartAt { get; set; }

    [Required]
    public DateTime? EndAt { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            yield return new ValidationResult(
                "Title is required.",
                new[] { nameof(Title) });
        }

        if (StartAt is { } start && EndAt is { } end && end <= start)
        {
            yield return new ValidationResult(
                "EndAt must be later than StartAt.",
                new[] { nameof(EndAt) });
        }
    }
}