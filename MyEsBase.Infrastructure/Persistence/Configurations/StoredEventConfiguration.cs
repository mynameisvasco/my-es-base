using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyEsBase.Infrastructure.EventStore;

namespace MyEsBase.Infrastructure.Persistence.Configurations;

public class StoredEventConfiguration : IEntityTypeConfiguration<StoredEvent>
{
    public void Configure(EntityTypeBuilder<StoredEvent> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.AggregateId).IsRequired();
        builder.HasIndex(b => b.AggregateId);
        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.Data).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}