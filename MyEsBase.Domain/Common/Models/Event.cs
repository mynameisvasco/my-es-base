namespace MyEsBase.Domain.Common.Models;

public abstract record Event
{
    public DateTime CreatedAt { get; } = DateTime.Now;
}