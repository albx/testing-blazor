﻿namespace TestingDemo.Shared.Todo;

public record TodoItem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool IsDone { get; set; } = false;
}
