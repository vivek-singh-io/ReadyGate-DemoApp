namespace ReadyGate.Showcase.Api.Domain;

public sealed class SupportTicket
{
    public int Id { get; set; }
    public required string Reference { get; set; }
    public required string Subject { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public required string ResolutionNotes { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}

public enum TicketStatus
{
    Open,
    InProgress,
    Resolved,
    Flagged
}

public enum TicketPriority
{
    Low,
    Medium,
    High,
    Critical
}
