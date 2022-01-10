using Microsoft.AspNetCore.Mvc;
using MyEsBase.Application.Todos.Commands;
using MyEsBase.Application.Todos.Query;
using MyEsBase.Domain.Todos;

namespace MyEsBase.Api.Controllers;

public class TodosController : BaseController
{
    [HttpPost]
    public async Task<Guid> Create(CreateTodoItemCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet("{id:guid}")]
    public async Task<Todo> Delete(Guid id)
    {
        return await Mediator.Send(new GetTodoByIdQuery(id));
    }
}