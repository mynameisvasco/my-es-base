using MediatR;
using MyEsBase.Application.Common.Interfaces;
using MyEsBase.Domain.Todos;

namespace MyEsBase.Application.Todos.Commands;

public record CreateTodoItemCommand(string Title, string Body) : IRequest<Guid>;

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Guid>
{
    private readonly IEventStore _eventStore;

    public CreateTodoItemCommandHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo();
        todo.Create(request.Title, request.Body);
        await _eventStore.Save(todo);
        return todo.Id;
    }
}