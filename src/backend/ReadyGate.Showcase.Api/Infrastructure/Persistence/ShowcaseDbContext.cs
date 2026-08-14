using Microsoft.EntityFrameworkCore;
using ReadyGate.Showcase.Api.Domain;

namespace ReadyGate.Showcase.Api.Infrastructure.Persistence;

public sealed class ShowcaseDbContext(DbContextOptions<ShowcaseDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<SupportTicket> Tickets => Set<SupportTicket>();
    public DbSet<AccessRequest> AccessRequests => Set<AccessRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");
            entity.HasKey(customer => customer.Id);
            entity.Property(customer => customer.Name).HasMaxLength(120).IsRequired();
            entity.Property(customer => customer.Email).HasMaxLength(254).IsRequired();
            entity.Property(customer => customer.Phone).HasMaxLength(32).IsRequired();
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.ToTable("Tickets");
            entity.HasKey(ticket => ticket.Id);
            entity.HasIndex(ticket => ticket.Reference).IsUnique();
            entity.Property(ticket => ticket.Reference).HasMaxLength(24).IsRequired();
            entity.Property(ticket => ticket.Subject).HasMaxLength(200).IsRequired();
            entity.Property(ticket => ticket.ResolutionNotes).HasMaxLength(2_000).IsRequired();
            entity.Property(ticket => ticket.Status).HasConversion<string>().HasMaxLength(24);
            entity.Property(ticket => ticket.Priority).HasConversion<string>().HasMaxLength(24);
            entity.Property(ticket => ticket.UpdatedAtUtc)
                .HasConversion(
                    value => value.UtcTicks,
                    value => new DateTimeOffset(value, TimeSpan.Zero));
            entity.HasOne(ticket => ticket.Customer)
                .WithMany(customer => customer.Tickets)
                .HasForeignKey(ticket => ticket.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AccessRequest>(entity =>
        {
            entity.ToTable("AccessRequests");
            entity.HasKey(request => request.Id);
            entity.Property(request => request.RequesterName).HasMaxLength(120).IsRequired();
            entity.Property(request => request.RequestedRole).HasMaxLength(80).IsRequired();
            entity.Property(request => request.BusinessReason).HasMaxLength(500).IsRequired();
            entity.Property(request => request.Status).HasConversion<string>().HasMaxLength(24);
            entity.Property(request => request.RequestedAtUtc)
                .HasConversion(
                    value => value.UtcTicks,
                    value => new DateTimeOffset(value, TimeSpan.Zero));
        });
    }
}
