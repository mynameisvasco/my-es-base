using System.Text;
using System.Text.Json;
using MediatR;
using MyEsBase.Application.Common.Interfaces;
using MyEsBase.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using MyEsBase.Application.Common.Models;
using MyEsBase.Infrastructure.Persistence;

namespace MyEsBase.Infrastructure.EventStore;

public class EventStore : IEventStore
{
    private readonly AppDbContext _appDbContext;
    private readonly IPublisher _mediator;

    public EventStore(AppDbContext appDbContext, IPublisher mediator)
    {
        _appDbContext = appDbContext;
        _mediator = mediator;
    }

    public async Task Save(AggregateRoot aggregate)
    {
        var events = aggregate.GetUncommittedEvents();
        var enumerable = events.ToList();

        var storedEvents = from @event in enumerable
            let payload = JsonSerializer.Serialize<dynamic>(@event)
            let data = Encoding.UTF8.GetBytes(payload)
            select new StoredEvent
            {
                AggregateId = aggregate.Id,
                Name = @event.GetType().FullName,
                Id = Guid.NewGuid(),
                Data = data,
                CreatedAt = DateTime.Now
            };


        await _appDbContext.StoredEvents.AddRangeAsync(storedEvents);
        await _appDbContext.SaveChangesAsync();

        foreach (var @event in enumerable)
        {
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(@event));
        }

        aggregate.ClearEvents();
    }

    public async Task<TAggregate> GetById<TAggregate>(Guid id) where TAggregate : AggregateRoot, new()
    {
        var storedEvents = await _appDbContext.StoredEvents.Where(se => se.AggregateId == id)
            .OrderBy(se => se.CreatedAt)
            .ToListAsync();

        var events = from storedEvent in storedEvents
            let decoded = Encoding.UTF8.GetString(storedEvent.Data)
            let type = Type.GetType(storedEvent.Name) ??
                       AppDomain.CurrentDomain.GetAssemblies()
                           .Select(a => a.GetType(storedEvent.Name))
                           .FirstOrDefault(t => t != null)
            select JsonSerializer.Deserialize(decoded, type);

        var aggregate = new TAggregate();
        aggregate.Rehydrate(events);
        return aggregate;
    }

    private static INotification GetNotificationCorrespondingToDomainEvent(Event @event)
    {
        return (INotification) Activator.CreateInstance(
            typeof(EventNotification<>).MakeGenericType(@event.GetType()), @event)!;
    }
}