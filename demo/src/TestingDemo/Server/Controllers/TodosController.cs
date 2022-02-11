using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingDemo.Server.Services;
using TestingDemo.Shared.Todo;

namespace TestingDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        public TodoService Service { get; }

        public TodosController(TodoService service)
        {
            Service = service;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var items = Service.GetAllTodoItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodoDetail(Guid id)
        {
            var todo = Service.GetTodoItemDetail(id);
            if (todo is null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTodo([FromBody] TodoItem model)
        {
            await Service.CreateNewTodoItemAsync(model);
            return CreatedAtAction(nameof(GetTodoDetail), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(Guid id, [FromBody] TodoItem model)
        {
            try
            {
                await Service.UpdateTodoItemAsync(id, model);
                return Ok();
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            try
            {
                await Service.DeleteTodoItemAsync(id);
                return Ok();
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }
    }
}
