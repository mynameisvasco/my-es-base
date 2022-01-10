using MediatR;
using MyEsBase.Application.Common.Interfaces;
using MyEsBase.Domain.Todos;

namespace MyEsBase.Application.Todos.Query;

public record GetTodoByIdQuery(Guid Id) : IRequest<Todo>;

public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, Todo>
{
    private readonly IEventStore _eventStore;

    public GetTodoByIdQueryHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<Todo> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
    {
        var todo = await _eventStore.GetById<Todo>(query.Id);
        return todo;
    }
}