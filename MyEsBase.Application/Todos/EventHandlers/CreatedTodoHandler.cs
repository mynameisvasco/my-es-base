using MediatR;
using MyEsBase.Application.Common.Models;
using MyEsBase.Domain.Todos;

namespace MyEsBase.Application.Todos.EventHandlers;

public class CreatedTodoHandler : INotificationHandler<EventNotification<CreatedTodo>>
{
    public async Task Handle(EventNotification<CreatedTodo> notification, CancellationToken cancellationToken)
    {
        Console.WriteLine(notification.Event);
        await Task.CompletedTask;
    }
}