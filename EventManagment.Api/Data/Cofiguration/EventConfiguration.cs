using EventManagment.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagment.Api.Data.Cofiguration;

public sealed class EventConfiguration
    : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");

        builder.HasKey(e => e.EventId);

        builder.Property(e => e.EventName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.EventDescription)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(e => e.EventStartDate)
            .IsRequired();

        builder.Property(e => e.EventEndDate)
            .IsRequired();

        builder
            .HasMany(e => e.Tags)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "EventTags",

                right => right
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("TagId")
                    .OnDelete(DeleteBehavior.Cascade),

                left => left
                    .HasOne<Event>()
                    .WithMany()
                    .HasForeignKey("EventId")
                    .OnDelete(DeleteBehavior.Cascade),

                join =>
                {
                    join.ToTable("EventTags");

                    join.HasKey(
                        "EventId",
                        "TagId");

                    join.HasIndex("TagId");
                });
    }
}