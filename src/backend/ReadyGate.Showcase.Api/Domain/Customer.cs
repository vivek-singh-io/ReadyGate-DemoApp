namespace ReadyGate.Showcase.Api.Domain;

public sealed class Customer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public ICollection<SupportTicket> Tickets { get; set; } = [];
}
