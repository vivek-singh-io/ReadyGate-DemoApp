namespace ReadyGate.Showcase.Api.Domain;

public sealed class AccessRequest
{
    public int Id { get; set; }
    public required string RequesterName { get; set; }
    public required string RequestedRole { get; set; }
    public required string BusinessReason { get; set; }
    public AccessRequestStatus Status { get; set; }
    public DateTimeOffset RequestedAtUtc { get; set; }
}

public enum AccessRequestStatus
{
    Pending,
    Approved,
    Rejected
}
