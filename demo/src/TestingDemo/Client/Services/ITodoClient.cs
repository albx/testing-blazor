using TestingDemo.Shared.Todo;

namespace TestingDemo.Client.Services;

public interface ITodoClient
{
    Task<IEnumerable<TodoItem>> GetAllAsync();

    Task<TodoItem?> GetTodoItemAsync(Guid todoId);

    Task CreateNewTodoItemAsync(TodoItem item);

    Task UpdateTodoItemAsync(Guid todoId, TodoItem item);

    Task DeleteTodoItemAsync(Guid todoId);
}
