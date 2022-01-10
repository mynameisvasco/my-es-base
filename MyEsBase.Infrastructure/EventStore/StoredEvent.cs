namespace MyEsBase.Infrastructure.EventStore;

public class StoredEvent
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; set; }
    public string Name { get; set; }
    public byte[] Data { get; set; }
    public DateTime CreatedAt { get; set; }
}