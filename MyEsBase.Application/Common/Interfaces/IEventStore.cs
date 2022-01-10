using MyEsBase.Domain.Common.Models;

namespace MyEsBase.Application.Common.Interfaces;

public interface IEventStore
{
    public Task Save(AggregateRoot aggregate);
    public Task<TAggregate> GetById<TAggregate>(Guid id) where TAggregate : AggregateRoot, new();
}