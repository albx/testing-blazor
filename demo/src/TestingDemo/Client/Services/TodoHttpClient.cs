using System.Net.Http.Json;
using TestingDemo.Shared.Todo;

namespace TestingDemo.Client.Services;

public class TodoHttpClient : ITodoClient
{
    public TodoHttpClient(HttpClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public HttpClient Client { get; }

    public string ApiResource { get; set; } = "/api/todos";

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        var items = await Client.GetFromJsonAsync<IEnumerable<TodoItem>>(ApiResource);
        return items ?? Array.Empty<TodoItem>();
    }

    public async Task<TodoItem?> GetTodoItemAsync(Guid todoId)
    {
        var item = await Client.GetFromJsonAsync<TodoItem?>($"{ApiResource}/{todoId}");
        if (item is null)
        {
            return null;
        }

        return item;
    }

    public async Task CreateNewTodoItemAsync(TodoItem item)
    {
        var response = await Client.PostAsJsonAsync(ApiResource, item);
        ThrowIfInvalidResponse(response, $"Error creating todo item {item.Title}");
    }

    public async Task UpdateTodoItemAsync(Guid todoId, TodoItem item)
    {
        var response = await Client.PutAsJsonAsync($"{ApiResource}/{todoId}", item);
        ThrowIfInvalidResponse(response, $"Error updating todo item {item.Title}");
    }

    public async Task DeleteTodoItemAsync(Guid todoId)
    {
        var response = await Client.DeleteAsync($"{ApiResource}/{todoId}");
        ThrowIfInvalidResponse(response, "Error deleting item");
    }

    private void ThrowIfInvalidResponse(HttpResponseMessage response, string errorMessage)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(errorMessage);
        }
    }
}
