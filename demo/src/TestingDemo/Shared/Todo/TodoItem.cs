using System.ComponentModel.DataAnnotations;

namespace TestingDemo.Shared.Todo;

public record TodoItem
{
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public bool IsDone { get; set; } = false;
}
