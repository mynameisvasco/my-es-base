namespace MyEsBase.Domain.Common.Models;

public abstract class AggregateRoot
{
    public Guid Id { get; }
    private readonly IList<Event> _events = new List<Event>();

    protected AggregateRoot()
    {
        Id = Guid.NewGuid();
    }

    public void ApplyAndAdd(dynamic @event)
    {
        (this as dynamic).Apply(@event);
        _events.Add(@event);
    }

    public void Rehydrate(IEnumerable<dynamic> events)
    {
        foreach (var @event in events)
        {
            (this as dynamic).Apply(@event);
        }
    }

    public IEnumerable<Event> GetUncommittedEvents()
    {
        return _events;
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}