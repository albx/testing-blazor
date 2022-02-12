using TestingDemo.Server.Data;
using TestingDemo.Shared.Todo;

namespace TestingDemo.Server.Services;

public class TodoService
{
    public ApplicationDbContext Context { get; }

    public TodoService(ApplicationDbContext context)
    {
        Context = context;
    }

    public IEnumerable<TodoItem> GetAllTodoItems()
    {
        var items = Context.Todos.ToArray();
        return items;
    }

    public TodoItem? GetTodoItemDetail(Guid todoId)
    {
        var item = Context.Todos.FirstOrDefault(x => x.Id == todoId);
        return item;
    }

    public async Task CreateNewTodoItemAsync(TodoItem model)
    {
        Context.Todos.Add(model);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateTodoItemAsync(Guid todoId, TodoItem model)
    {
        var item = Context.Todos.SingleOrDefault(t => t.Id == todoId);
        if (item == null)
        {
            throw new ArgumentOutOfRangeException(nameof(item));
        }

        item.Title = model.Title;
        await Context.SaveChangesAsync();
    }

    public async Task DeleteTodoItemAsync(Guid todoId)
    {
        var item = Context.Todos.SingleOrDefault(t => t.Id == todoId);
        if (item == null)
        {
            throw new ArgumentOutOfRangeException(nameof(item));
        }

        Context.Todos.Remove(item);
        await Context.SaveChangesAsync();
    }

    public async Task MarkTodoItemAsDone(Guid todoId)
    {
        var item = Context.Todos.SingleOrDefault(t => t.Id == todoId);
        if (item == null)
        {
            throw new ArgumentOutOfRangeException(nameof(item));
        }

        item.IsDone = !item.IsDone;
        await Context.SaveChangesAsync();
    }
}
