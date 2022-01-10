using MediatR;
using MyEsBase.Domain.Common.Models;

namespace MyEsBase.Application.Common.Models;

public class EventNotification<T> : INotification where T : Event
{
    public T Event { get; }

    public EventNotification(T @event)
    {
        Event = @event;
    }
}