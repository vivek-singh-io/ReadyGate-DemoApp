using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ReadyGate.Showcase.Api.Domain;
using ReadyGate.Showcase.Api.Infrastructure.Persistence;

#nullable disable

namespace ReadyGate.Showcase.Api.Infrastructure.Persistence.Migrations;

[DbContext(typeof(ShowcaseDbContext))]
partial class ShowcaseDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder) => BuildSnapshotModel(modelBuilder);

    internal static void BuildSnapshotModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "10.0.6");

        modelBuilder.Entity<AccessRequest>(entity =>
        {
            entity.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
            entity.Property<string>("BusinessReason").IsRequired().HasMaxLength(500).HasColumnType("TEXT");
            entity.Property<DateTimeOffset>("RequestedAtUtc").HasConversion<long>().HasColumnType("INTEGER");
            entity.Property<string>("RequestedRole").IsRequired().HasMaxLength(80).HasColumnType("TEXT");
            entity.Property<string>("RequesterName").IsRequired().HasMaxLength(120).HasColumnType("TEXT");
            entity.Property<AccessRequestStatus>("Status").HasConversion<string>().HasMaxLength(24).HasColumnType("TEXT");
            entity.HasKey("Id");
            entity.ToTable("AccessRequests");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
            entity.Property<string>("Email").IsRequired().HasMaxLength(254).HasColumnType("TEXT");
            entity.Property<string>("Name").IsRequired().HasMaxLength(120).HasColumnType("TEXT");
            entity.Property<string>("Phone").IsRequired().HasMaxLength(32).HasColumnType("TEXT");
            entity.HasKey("Id");
            entity.ToTable("Customers");
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
            entity.Property<int>("CustomerId").HasColumnType("INTEGER");
            entity.Property<TicketPriority>("Priority").HasConversion<string>().HasMaxLength(24).HasColumnType("TEXT");
            entity.Property<string>("Reference").IsRequired().HasMaxLength(24).HasColumnType("TEXT");
            entity.Property<string>("ResolutionNotes").IsRequired().HasMaxLength(2000).HasColumnType("TEXT");
            entity.Property<TicketStatus>("Status").HasConversion<string>().HasMaxLength(24).HasColumnType("TEXT");
            entity.Property<string>("Subject").IsRequired().HasMaxLength(200).HasColumnType("TEXT");
            entity.Property<DateTimeOffset>("UpdatedAtUtc").HasConversion<long>().HasColumnType("INTEGER");
            entity.HasKey("Id");
            entity.HasIndex("CustomerId");
            entity.HasIndex("Reference").IsUnique();
            entity.ToTable("Tickets");
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.HasOne("ReadyGate.Showcase.Api.Domain.Customer", "Customer")
                .WithMany("Tickets")
                .HasForeignKey("CustomerId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            entity.Navigation("Customer");
        });

        modelBuilder.Entity<Customer>().Navigation("Tickets");
    }
}
